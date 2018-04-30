using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Mercator.GIS.Coordinate;
using Mercator.GIS.Projection;
using Mercator.Mathematics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Mercator.GIS.Map.Browser
{
    public partial class MapDocument : DockContent
    {
        public OperateFlagType OperateFlag = OperateFlagType.None;
        private string m_mapProvider = string.Empty;
        public string MapProvider
        {
            get
            {
                return m_mapProvider;
            }
            set
            {
                m_mapProvider = value;

                switch(m_mapProvider)
                {
                    case "BingSatelliteMap":
                        mapControl.MapProvider = GMapProviders.BingSatelliteMap;
                        break;
                    case "BingMap":
                        mapControl.MapProvider = GMapProviders.BingMap;
                        break;
                    case "BingHybridMap":
                        mapControl.MapProvider = GMapProviders.BingHybridMap;
                        break;
                    case "GoogleChinaHybridMap":
                        mapControl.MapProvider = GMapProviders.GoogleChinaHybridMap;
                        break;
                    case "GoogleChinaSatelliteMap":
                        mapControl.MapProvider = GMapProviders.GoogleChinaSatelliteMap;
                        break;
                    case "GoogleChinaMap":
                        mapControl.MapProvider = GMapProviders.GoogleChinaMap;
                        break;
                    case "GoogleChinaTerrainMap":
                        mapControl.MapProvider = GMapProviders.GoogleChinaTerrainMap;
                        break;
                }
            }
        }

        /// <summary>
        /// 像控点
        /// </summary>
        private GMapOverlay m_pixelOverlay;
        /// <summary>
        /// 参考点
        /// </summary>
        private GMapOverlay m_referenceOverlay;
        /// <summary>
        /// 点 - 标记
        /// </summary>
        private GMapOverlay m_pointOverlay;
        /// <summary>
        /// 线 - 路径
        /// </summary>
        private GMapOverlay m_polylineOverlay;
        /// <summary>
        /// 面 - 多边形
        /// </summary>
        private GMapOverlay m_polygonOverlay;

        private bool m_isStartDrawPolyline = true;
        private bool m_isStartDrawPolygon = true;
        private PointLatLng m_startPoint;
        private PointLatLng m_endPoint;

        private List<PointLatLng> m_tempPoints;
        private GMapRoute m_tempRoute;
        private GMapOverlay m_tempRouteOverlay;

        public MapDocument()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            mapControl.CacheLocation = System.Windows.Forms.Application.StartupPath;
            mapControl.Manager.Mode = AccessMode.ServerAndCache;
            mapControl.MaxZoom = 20;
            mapControl.MinZoom = 1;
            mapControl.ShowCenter = false;
            mapControl.DragButton = MouseButtons.Middle;
            mapControl.Zoom = 8;
            mapControl.Position = new PointLatLng(27.33, 103.72);

            m_polygonOverlay = new GMapOverlay("SketchPolygon");
            m_polylineOverlay = new GMapOverlay("SketchPolyline");
            m_pointOverlay = new GMapOverlay("SketchPoint");
            m_tempRouteOverlay = new GMapOverlay("TempRoute");

            m_pixelOverlay = new GMapOverlay("PixelPoint");
            m_referenceOverlay = new GMapOverlay("ReferencePoint");

            m_startPoint = new PointLatLng();
            m_endPoint = new PointLatLng();

            m_tempPoints = new List<PointLatLng>();

            this.mapControl.Overlays.Add(m_polygonOverlay);
            this.mapControl.Overlays.Add(m_polylineOverlay);
            this.mapControl.Overlays.Add(m_tempRouteOverlay);
            this.mapControl.Overlays.Add(m_pointOverlay);
            this.mapControl.Overlays.Add(m_pixelOverlay);
            this.mapControl.Overlays.Add(m_referenceOverlay);
        }

        protected override string GetPersistString()
        {
            // Add extra information into the persist string for this document
            // so that it is available when deserialized.
            return GetType().ToString() + "," + MapProvider + "," + Text;
        }

        public void AddOverlayer(GMapOverlay overlay)
        {
            mapControl.Overlays.Add(overlay);
        }

        public void ShowOrHidderOverlay(string layerName, bool isVisibile)
        {
            foreach(var overlay in mapControl.Overlays)
            {
                if(overlay.Id.Equals(layerName))
                {
                    overlay.IsVisibile = isVisibile;
                    mapControl.Refresh();
                    break;
                }
            }
        }

        public void Zoom(string markersOverlayId)
        {
            mapControl.ZoomAndCenterMarkers(markersOverlayId);
        }

        public void Zoom(RectLatLng rect)
        {
            mapControl.SetZoomToFitRect(rect);
        }

        private void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            var form = (MainForm)this.ParentForm;

            if (e.Button == MouseButtons.Left)
            {
                var point = mapControl.FromLocalToLatLng(e.X, e.Y);
                switch (OperateFlag)
                {
                    case OperateFlagType.DrawMarker:
                        AddSketchPoint(point);
                        break;
                    case OperateFlagType.DrawPolyline:
                        if (!m_isStartDrawPolyline)
                        {
                            m_endPoint = point;
                            AddTempSketchPolyline(m_startPoint, m_endPoint);
                            m_startPoint = point;
                        }
                        else
                        {
                            ClearTempData();
                            m_startPoint = point;
                            m_isStartDrawPolyline = false;
                            m_isStartDrawPolygon = true;
                        }
                        m_tempPoints.Add(point);
                        break;
                    case OperateFlagType.DrawPolygon:
                        if (!m_isStartDrawPolygon)
                        {
                            m_endPoint = point;
                            AddTempSketchPolyline(m_startPoint, m_endPoint);
                            m_startPoint = point;
                        }
                        else
                        {
                            ClearTempData();
                            m_startPoint = point;
                            m_isStartDrawPolygon = false;
                            m_isStartDrawPolyline = true;
                        }
                        m_tempPoints.Add(point);
                        break;
                    case OperateFlagType.AddPixelPoint:
                        AddPixelPoint(point);
                        form.ShowPixelPoint(point);
                        break;
                    case OperateFlagType.AddReferencePoint:
                        AddReferencePoint(point);
                        form.ShowReferencePoint(point);
                        break;
                }
            }
            else
            {
                switch (OperateFlag)
                {
                    case OperateFlagType.DrawPolyline:
                        AddSketchPolyline();
                        ClearTempData();
                        m_isStartDrawPolyline = true;
                        break;
                    case OperateFlagType.DrawPolygon:
                        AddSketchPolygon();
                        ClearTempData();
                        m_isStartDrawPolygon = true;
                        break;
                    default:
                        OperateFlag = OperateFlagType.None;
                        break;
                }
            }
        }

        private void ClearTempData()
        {
            m_tempPoints.Clear();
            if (m_tempRoute != null) { m_tempRoute.Clear(); }
            m_tempRouteOverlay.Routes.Clear();
        }

        private void AddSketchPoint(PointLatLng point)
        {
            var marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
            marker.ToolTipText = string.Format("{0}#草图点", m_pointOverlay.Markers.Count + 1);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            m_pointOverlay.Markers.Add(marker);
        }

        private void AddTempSketchPolyline(PointLatLng start,PointLatLng end)
        {
            var tempPoints = new List<PointLatLng>();
            tempPoints.Add(start);
            tempPoints.Add(end);
            m_tempRoute = new GMapRoute(tempPoints, "TempRoute");
            m_tempRoute.Stroke = new Pen(Color.Green, 3);
            m_tempRouteOverlay.Routes.Add(m_tempRoute);
        }

        private void AddSketchPolyline()
        {
            var index = m_polylineOverlay.Routes.Count + 1;
            var route = new GMapRoute(m_tempPoints, "Polyline" + index);
            route.Stroke = new Pen(Color.Green, 3);
            route.IsHitTestVisible = true;
            m_polylineOverlay.Routes.Add(route);
        }

        private void AddSketchPolygon()
        {
            if (m_tempPoints.Count < 3) { return; }
            var index = m_polygonOverlay.Polygons.Count + 1;
            m_tempPoints.Add(m_tempPoints[0]);
            var polygon = new GMapPolygon(m_tempPoints, "Polygon" + index);
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.LightPink));
            var pen = new Pen(Color.Green, 3);
            polygon.Stroke = pen;
            polygon.IsHitTestVisible = true;
            m_polygonOverlay.Polygons.Add(polygon);
        }

        private void AddPixelPoint(PointLatLng point)
        {
            var marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
            marker.ToolTipText = string.Format("{0}#像控点", m_pixelOverlay.Markers.Count + 1);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            m_pixelOverlay.Markers.Add(marker);
        }

        private void AddReferencePoint(PointLatLng point)
        {
            var marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
            marker.ToolTipText = string.Format("{0}#参考点", m_referenceOverlay.Markers.Count + 1);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            m_referenceOverlay.Markers.Add(marker);
        }

        public void ClearLayer(OverlayerType type)
        {
            switch(type)
            {
                case OverlayerType.SketchPoint:
                    foreach(var overlay in mapControl.Overlays)
                    {
                        if(overlay.Id.Equals("SketchPoint"))
                        {
                            overlay.Clear();
                            break;
                        }
                    }
                    break;
                case OverlayerType.SketchPolyline:
                    foreach (var overlay in mapControl.Overlays)
                    {
                        if (overlay.Id.Equals("SketchPolyline"))
                        {
                            overlay.Clear();
                            break;
                        }
                    }
                    break;
                case OverlayerType.SketchPolygon:
                    foreach (var overlay in mapControl.Overlays)
                    {
                        if (overlay.Id.Equals("SketchPolygon"))
                        {
                            overlay.Clear();
                            break;
                        }
                    }
                    break;
                case OverlayerType.PixelPoint:
                    foreach (var overlay in mapControl.Overlays)
                    {
                        if (overlay.Id.Equals("PixelPoint"))
                        {
                            overlay.Clear();
                            break;
                        }
                    }
                    break;
                case OverlayerType.ReferencePoint:
                    foreach (var overlay in mapControl.Overlays)
                    {
                        if (overlay.Id.Equals("ReferencePoint"))
                        {
                            overlay.Clear();
                            break;
                        }
                    }
                    break;
                case OverlayerType.ShpFile:
                    var layers = new List<GMapOverlay>();
                    foreach (var overlay in mapControl.Overlays)
                    {
                        if (overlay.Id.Equals("TempRoute") || overlay.Id.Equals("ReferencePoint") || overlay.Id.Equals("PixelPoint") || overlay.Id.Equals("SketchPolygon") || overlay.Id.Equals("SketchPolyline") || overlay.Id.Equals("SketchPoint"))
                        {
                            continue;
                        }
                        layers.Add(overlay);
                    }
                    foreach(var layer in layers)
                    {
                        mapControl.Overlays.Remove(layer);
                    }
                    break;
            }

            mapControl.Refresh();
        }

        public int GetSketchPointCount()
        {
            return m_pointOverlay.Markers.Count;
        }

        public int GetSketchPolylineCount()
        {
            return m_polylineOverlay.Routes.Count;
        }

        public int GetSketchPolygonCount()
        {
            return m_polygonOverlay.Polygons.Count;
        }

        public void GetSketchPointInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ)
        {
            var marker = m_pointOverlay.Markers[index];
            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;
            projection.Forward(marker.Position.Lat, marker.Position.Lng, out adfX[0], out adfY[0]);
        }

        public void GetSketchPointInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ, Matrix param4)
        {
            var marker = m_pointOverlay.Markers[index];
            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;
            projection.Forward(marker.Position.Lat, marker.Position.Lng, out adfX[0], out adfY[0]);

            var sourceCoordinate = new HorizontalCoordinate(adfX[0], adfY[0]);
            var targetCoordinate = LinearTransformation.Transform(sourceCoordinate, param4);

            adfX[0] = targetCoordinate.Y;
            adfY[0] = targetCoordinate.X;
        }

        public int GetSketchPolylinePointCount(int index)
        {
            return m_polylineOverlay.Routes[index].Points.Count;
        }

        public int GetSketchPolygonPointCount(int index)
        {
            return m_polygonOverlay.Polygons[index].Points.Count;
        }

        public void GetSketchPolylineInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ)
        {
            var route = m_polylineOverlay.Routes[index];

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            for (int i=0;i<route.Points.Count;i++)
            {
                projection.Forward(route.Points[i].Lat, route.Points[i].Lng, out adfX[i], out adfY[i]);
            }
        }

        public void GetSketchPolylineInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ, Matrix param4)
        {
            var route = m_polylineOverlay.Routes[index];

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            for (int i = 0; i < route.Points.Count; i++)
            {
                projection.Forward(route.Points[i].Lat, route.Points[i].Lng, out adfX[i], out adfY[i]);

                var sourceCoordinate = new HorizontalCoordinate(adfX[i], adfY[i]);
                var targetCoordinate = LinearTransformation.Transform(sourceCoordinate, param4);

                adfX[i] = targetCoordinate.Y;
                adfY[i] = targetCoordinate.X;
            }
        }

        public void GetSketchPolygonInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ)
        {
            var polygon = m_polygonOverlay.Polygons[index];

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            for (int i = 0; i < polygon.Points.Count; i++)
            {
                projection.Forward(polygon.Points[i].Lat, polygon.Points[i].Lng, out adfX[i], out adfY[i]);
            }

        }

        public void GetSketchPolygonInfo(int index, ref double[] adfX, ref double[] adfY, ref double[] adfZ, Matrix param4)
        {
            var polygon = m_polygonOverlay.Polygons[index];

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            for (int i = 0; i < polygon.Points.Count; i++)
            {
                projection.Forward(polygon.Points[i].Lat, polygon.Points[i].Lng, out adfX[i], out adfY[i]);

                var sourceCoordinate = new HorizontalCoordinate(adfX[i], adfY[i]);
                var targetCoordinate = LinearTransformation.Transform(sourceCoordinate, param4);

                adfX[i] = targetCoordinate.Y;
                adfY[i] = targetCoordinate.X;
            }

        }

        /// <summary>
        /// 获取转换后的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="param4"></param>
        /// <returns></returns>
        private HorizontalCoordinate GetTransformedPoint(double x, double y, Matrix param4)
        {
            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            // 根据x,y获取经纬度
            double lat, lng;
            projection.Reverse(x, y,out lat,out lng);
            // 计算偏移量（大数）
            var offset = (int)(y / 1000000) * 1000000;
            // 坐标转换（不要加大数）
            var sourceCoordinate = new HorizontalCoordinate(x, y - offset);
            var targetCoordinate = LinearTransformation.Transform(sourceCoordinate, param4);
            // 带大数的坐标
            var xyWithZone = new HorizontalCoordinate(targetCoordinate.X, targetCoordinate.Y + offset);

            return xyWithZone;
        }

        private void mapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if(OperateFlag== OperateFlagType.QueryAttribute)
                MessageBox.Show(item.ToolTipText);
        }

        private void mapControl_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            if (OperateFlag == OperateFlagType.QueryAttribute)
                MessageBox.Show(item.Name);
        }

        private void mapControl_OnRouteClick(GMapRoute item, MouseEventArgs e)
        {
            if (OperateFlag == OperateFlagType.QueryAttribute)
            {
                MessageBox.Show(item.Name + "长度: " + Math.Round(item.Distance * 1000, 2) + " 米");
            }
        }

        public void Prefetch(RectLatLng area,int minZoon,int maxZoom)
        {
            if (!area.IsEmpty)
            {
                for (int i = minZoon; i <= maxZoom; i++)
                {
                    using (TilePrefetcher obj = new TilePrefetcher())
                    {
                        obj.Shuffle = mapControl.Manager.Mode != AccessMode.CacheOnly;
                        obj.Owner = this;
                        obj.ShowCompleteMessage = true;
                        obj.Start(area, i, mapControl.MapProvider, mapControl.Manager.Mode == AccessMode.CacheOnly ? 0 : 100, mapControl.Manager.Mode == AccessMode.CacheOnly ? 0 : 1);
                    }
                }
            }
        }

        
    }

    public enum OverlayerType
    {
        ShpFile,
        SketchPoint,
        SketchPolyline,
        SketchPolygon,
        PixelPoint,
        ReferencePoint
    }

    public enum OperateFlagType
    {
        DrawMarker,
        DrawPolyline,
        DrawPolygon,
        QueryAttribute,
        AddPixelPoint,
        AddReferencePoint,
        None
    }
}
