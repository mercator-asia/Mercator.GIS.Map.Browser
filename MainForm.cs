using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using Mercator.GIS.Coordinate;
using Mercator.GIS.Map.Tools;
using Mercator.GIS.Coordinate.KVS;
using Mercator.GIS.Projection;
using Mercator.Mathematics.LinearAlgebra;

namespace Mercator.GIS.Map.Browser
{
    public partial class MainForm : Form
    {
        private LayerWindow m_layerWindow;
        private PropertyWindow m_propertyWindow;
        private MapDocument m_mapDocument;
        private OutputWindow m_outputWindow;

        private VS2015LightTheme m_theme;
        private VisualStudioToolStripExtender m_toolStripExtender;
        private DeserializeDockContent m_deserializeDockContent;

        private List<ToolStripMenuItem> m_menuItems;
        private List<ToolStripButton> m_toolButtons;
        private List<ToolStripMenuItem> m_mapSourceMenuItems;

        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            SetStyle();
            CreateToolWindows();
        }

        public void MainForm_Load(object sender, EventArgs e)
        {
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(configFile)) { dockPanel.LoadFromXml(configFile, m_deserializeDockContent); }
            menuItemPropertyWindow.Checked = !m_propertyWindow.IsHidden;
            menuItemOutputWindow.Checked = !m_outputWindow.IsHidden;
            menuItemLayerWindow.Checked = !m_layerWindow.IsHidden;

            if (m_mapDocument == null) { OpenMapWindow(); }

            AddMenuItemAndToolButtonToList();

            foreach (var item in m_mapSourceMenuItems)
            {
                item.Checked = item.Text.Equals(m_mapDocument.MapProvider) ? true : false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel.SaveAsXml(configFile);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAllContents();
        }

        private void AddMenuItemAndToolButtonToList()
        {
            m_menuItems = new List<ToolStripMenuItem>();
            m_toolButtons = new List<ToolStripButton>();

            m_menuItems.Add(menuItemAddPixelPoint);
            m_menuItems.Add(menuItemAddReferencePoint);
            m_menuItems.Add(menuItemDrawMarker);
            m_menuItems.Add(menuItemDrawRoute);
            m_menuItems.Add(menuItemDrawPolygon);
            m_menuItems.Add(menuItemQueryAttribute);

            m_toolButtons.Add(toolStripButtonAddPixelPoint);
            m_toolButtons.Add(toolStripButtonAddReferencePoint);
            m_toolButtons.Add(toolStripButtonDrawMarker);
            m_toolButtons.Add(toolStripButtonDrawRoute);
            m_toolButtons.Add(toolStripButtonDrawPolygon);
            m_toolButtons.Add(toolStripButtonQueryAttribute);

            m_mapSourceMenuItems = new List<ToolStripMenuItem>();
            m_mapSourceMenuItems.Add(googleChinaHybridMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(googleChinaMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(googleChinaSatelliteMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(googleChinaTerrainMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(bingHybridMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(bingMapToolStripMenuItem);
            m_mapSourceMenuItems.Add(bingSatelliteMapToolStripMenuItem);

        }

        private void SetStyle()
        {
            m_theme = new VS2015LightTheme();
            m_toolStripExtender = new VisualStudioToolStripExtender();
            m_toolStripExtender.DefaultRenderer = new ToolStripProfessionalRenderer();

            this.dockPanel.Theme = this.m_theme;
            var version = VisualStudioToolStripExtender.VsVersion.Vs2015;
            m_toolStripExtender.SetStyle(menuStrip, version, m_theme);
            m_toolStripExtender.SetStyle(toolStrip, version, m_theme);
            m_toolStripExtender.SetStyle(statusStrip, version, m_theme);

            if (dockPanel.Theme.ColorPalette != null)
            {
                statusStrip.BackColor = dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
            }
        }

        private void CreateToolWindows()
        {
            m_layerWindow = new LayerWindow();
            m_propertyWindow = new PropertyWindow();
            m_outputWindow = new OutputWindow();
        }

        private void OpenMapWindow(string mapProvider = "GoogleChinaHybridMap")
        {
            m_mapDocument = new MapDocument();
            m_mapDocument.MapProvider = mapProvider;

            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                m_mapDocument.MdiParent = this;
                m_mapDocument.Show();
            }
            else
            {
                m_mapDocument.Show(dockPanel);
            }
        }

        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    // IMPORANT: dispose all panes.
                    document.DockHandler.DockPanel = null;
                    document.DockHandler.Close();
                }
            }
        }

        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_layerWindow.DockPanel = null;
            m_propertyWindow.DockPanel = null;
            m_outputWindow.DockPanel = null;

            // Close all other document windows
            CloseAllDocuments();

            // IMPORTANT: dispose all float windows.
            foreach (var window in dockPanel.FloatWindows.ToList())
                window.Dispose();

            System.Diagnostics.Debug.Assert(dockPanel.Panes.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.Contents.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.FloatWindows.Count == 0);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(LayerWindow).ToString())
                return m_layerWindow;
            else if (persistString == typeof(PropertyWindow).ToString())
                return m_propertyWindow;
            else if (persistString == typeof(OutputWindow).ToString())
                return m_outputWindow;
            else
            {
                // MapDocument overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(MapDocument).ToString())
                    return null;
                m_mapDocument = new MapDocument();
                if (parsedStrings[1] != string.Empty)
                    m_mapDocument.MapProvider = parsedStrings[1];
                if (parsedStrings[2] != string.Empty)
                    m_mapDocument.Text = parsedStrings[2];

                return m_mapDocument;
            }
        }

        private void menuItemPropertyWindow_Click(object sender, System.EventArgs e)
        {
            if (!menuItemPropertyWindow.Checked)
            {
                m_propertyWindow.Show(dockPanel);
            }
            else
            {
                m_propertyWindow.Hide();
            }
            menuItemPropertyWindow.Checked = !menuItemPropertyWindow.Checked;

        }

        private void menuItemLayerWindow_Click(object sender, System.EventArgs e)
        {
            if (!menuItemLayerWindow.Checked)
            {
                m_layerWindow.Show(dockPanel);
            }
            else
            {
                m_layerWindow.Hide();
            }
            menuItemLayerWindow.Checked = !menuItemLayerWindow.Checked;
        }

        private void menuItemOutput_Click(object sender, EventArgs e)
        {
            if (!menuItemOutputWindow.Checked)
            {
                m_outputWindow.Show(dockPanel);
            }
            else
            {
                m_outputWindow.Hide();
            }
            menuItemOutputWindow.Checked = !menuItemOutputWindow.Checked;
        }

        private void OpenShpFile(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "shp文件(*.shp)|*.shp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (m_layerWindow.IsLayerExist(dialog.FileName))
                {
                    m_outputWindow.OutputText(string.Format("操作失败：图层 {0} 已经加载。", dialog.FileName));
                    return;
                }

                OpenLayer(dialog.FileName);
            }
        }

        /// <summary>
        /// 取图形范围
        /// </summary>
        /// <param name="adfMinBound"></param>
        /// <param name="adfMaxBound"></param>
        /// <param name="param4"></param>
        /// <returns></returns>
        private RectLatLng GetShapeRect(double[] adfMinBound, double[] adfMaxBound, Matrix param4)
        {
            double minX, minY, maxX, maxY;

            minX = adfMinBound[0];
            minY = adfMinBound[1];

            var minZone = (int)minX / 1000000;
            minX = minX - minZone * 1000000;

            maxX = adfMaxBound[0];
            maxY = adfMaxBound[1];

            var maxZone = (int)maxX / 1000000;
            maxX = maxX - maxZone * 1000000;

            // 坐标转换（不要加大数）
            var minXY = LinearTransformation.Transform(new HorizontalCoordinate(minX, minY), param4);
            var maxXY = LinearTransformation.Transform(new HorizontalCoordinate(maxX, maxY), param4);

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            // 再转成经纬度（加上大数，因为需要计算投影带号）
            double lat, lng;

            projection.LongitudeOfOrigin = minZone * 3;
            projection.Reverse(minXY.X, minXY.Y, out lat, out lng);
            var minLB = new GeocentricCoordinate(lat, lng);

            projection.LongitudeOfOrigin = maxZone * 3;
            projection.Reverse(maxXY.X, maxXY.Y, out lat, out lng);
            var maxLB = new GeocentricCoordinate(lat, lng);

            // 取得图形范围用于显示
            var rect = new RectLatLng(maxLB.Latitude.Digital, minLB.Longitude.Digital, maxLB.Longitude.Digital - minLB.Longitude.Digital, maxLB.Latitude.Digital - minLB.Latitude.Digital);

            return rect;
        }

        private RectLatLng GetShapeRect(double[] adfMinBound, double[] adfMaxBound)
        {
            double minX, minY, maxX, maxY;

            minX = adfMinBound[0];
            minY = adfMinBound[1];

            var minZone = (int)minX / 1000000;
            minX = minX - minZone * 1000000;

            maxX = adfMaxBound[0];
            maxY = adfMaxBound[1];

            var maxZone = (int)maxX / 1000000;
            maxX = maxX - maxZone * 1000000;

            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            double lat, lng;

            projection.LongitudeOfOrigin = minZone * 3;
            projection.Reverse(minX, minY, out lat, out lng);
            var minLB = new GeocentricCoordinate(lat, lng);

            projection.LongitudeOfOrigin = maxZone * 3;
            projection.Reverse(maxX, maxY, out lat, out lng);
            var maxLB = new GeocentricCoordinate(lat, lng);

            // 取得图形范围用于显示
            var rect = new RectLatLng(maxLB.Latitude.Digital, minLB.Longitude.Digital, maxLB.Longitude.Digital - minLB.Longitude.Digital, maxLB.Latitude.Digital - minLB.Latitude.Digital);
            return rect;
        }

        /// <summary>
        /// 获取转换后的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="param4"></param>
        /// <returns></returns>
        private PointLatLng GetTransformedPoint(double x, double y, Matrix param4)
        {
            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            var zone = (int)x / 1000000;
            x = x - zone * 1000000;

            // 坐标转换（不要加大数）
            var sourceCoordinate = new HorizontalCoordinate(x, y);
            var targetCoordinate = LinearTransformation.Transform(sourceCoordinate, param4);

            // 再转成经纬度（加上大数，因为需要计算投影带号）
            double lat, lng;
            projection.LongitudeOfOrigin = zone * 3;
            projection.Reverse(targetCoordinate.X, targetCoordinate.Y, out lat, out lng);

            return new PointLatLng(lat, lng);
        }

        private PointLatLng GetPoint(double x, double y)
        {
            var projection = new GaussKrugerProjection();
            projection.Ellipsoid = ReferenceEllipsoid.International1975;

            var zone = (int)x / 1000000;
            x = x - zone * 1000000;

            // 根据x,y获取经纬度
            double lat, lng;

            projection.LongitudeOfOrigin = zone * 3;
            projection.Reverse(x, y, out lat, out lng);
            return new PointLatLng(lat, lng);
        }

        /// <summary>
        /// 更新覆盖层
        /// </summary>
        /// <param name="overlay">覆盖层对象</param>
        /// <param name="pshpType">图形类型</param>
        /// <param name="points">点集</param>
        /// <param name="entityName">实体名（属性值）</param>
        private void UpdateOverlay(ref GMapOverlay overlay, Shapelib.ShapeType pshpType, List<PointLatLng> points, string entityName)
        {
            var pen = new Pen(Color.White);

            switch (pshpType)
            {
                case Shapelib.ShapeType.Point:
                    var marker = new GMarkerGoogle(points[0], GMarkerGoogleType.orange_dot);
                    overlay.Markers.Add(marker);
                    break;
                case Shapelib.ShapeType.PolyLine:
                    var route = new GMapRoute(points, entityName);
                    pen = new Pen(Color.Red, 3);
                    route.Stroke = pen;
                    route.IsHitTestVisible = true;
                    overlay.Routes.Add(route);
                    break;
                case Shapelib.ShapeType.Polygon:
                    var polygon = new GMapPolygon(points, entityName);
                    polygon.Fill = new SolidBrush(Color.FromArgb(20, Color.LightPink));
                    pen = new Pen(Color.Pink, 3);
                    pen.DashPattern = new float[] { 2, 3 };
                    polygon.Stroke = pen;
                    polygon.IsHitTestVisible = true;
                    overlay.Polygons.Add(polygon);
                    break;
            }
        }

        private void ChangeMapSource(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            m_mapDocument.MapProvider = menuItem.Text;

            foreach(var item in m_mapSourceMenuItems)
            {
                item.Checked = item.Equals(menuItem) ? true : false;
            }
        }

        private void OpenShpFileAndTransform(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "shp文件(*.shp)|*.shp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (m_layerWindow.IsLayerExist(dialog.FileName))
                {
                    m_outputWindow.OutputText(string.Format("操作失败：文件 {0} 已经存在。", dialog.FileName));
                    return;
                }
                var xmlDialog = new OpenFileDialog();
                xmlDialog.Filter = "控制点坐标(*.xml)|*.xml";
                if (xmlDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenLayer(dialog.FileName, xmlDialog.FileName);
                }
            }
        }

        private void OpenLayer(string shpFileName, string xmlFileName = "")
        {
            var pathName = Path.GetDirectoryName(shpFileName);
            var fileName = Path.GetFileNameWithoutExtension(shpFileName);

            var shp = string.Format(@"{0}\{1}.shp", pathName, fileName);
            var hSHP = Shapelib.SHPOpen(shp, "rb");
            var dbf = string.Format(@"{0}\{1}.dbf", pathName, fileName);
            var hDBF = Shapelib.DBFOpen(dbf, "rb");

            // 实体数
            int pnEntities = 0;
            // 形状类型
            Shapelib.ShapeType pshpType = Shapelib.ShapeType.NullShape;
            // 界限坐标数组
            double[] adfMinBound = new double[4], adfMaxBound = new double[4];

            // 获取实体数、形状类型、界限坐标等信息
            Shapelib.SHPGetInfo(hSHP, ref pnEntities, ref pshpType, adfMinBound, adfMaxBound);

            var sourceCoordinates = new List<HorizontalCoordinate>();
            var targetCoordinates = new List<HorizontalCoordinate>();

            if (!string.IsNullOrEmpty(xmlFileName))
                KVS.Open(xmlFileName, out sourceCoordinates, out targetCoordinates);

            var param4 = string.IsNullOrEmpty(xmlFileName) ? null : LinearTransformation.GetTransformationParameter(sourceCoordinates, targetCoordinates);
            var rect = string.IsNullOrEmpty(xmlFileName) ? GetShapeRect(adfMinBound, adfMaxBound) : GetShapeRect(adfMinBound, adfMaxBound, param4);

            var node = new TreeNode(shp);
            node.Tag = rect;
            node.Checked = true;
            node.ExpandAll();

            GMapOverlay overlay = new GMapOverlay(shp);

            for (int iShape = 0; iShape < pnEntities; iShape++)
            {
                // SHPObject对象
                Shapelib.SHPObject shpObject = new Shapelib.SHPObject();
                // 读取SHPObject对象指针
                var shpObjectPtr = Shapelib.SHPReadObject(hSHP, iShape);
                // 忽略可能存在问题的实体
                if (shpObjectPtr == IntPtr.Zero) { continue; }
                // 指针转换为SHPObject对象
                Marshal.PtrToStructure(shpObjectPtr, shpObject);

                // 顶点数
                int nVertices = shpObject.nVertices;

                // 顶点的X坐标数组
                var padfX = new double[nVertices];
                // 将顶点的X坐标数组指针转换为数组
                Marshal.Copy(shpObject.padfX, padfX, 0, nVertices);
                // 顶点的Y坐标数组
                var padfY = new double[nVertices];
                // 将顶点的Y坐标数组指针转换为数组
                Marshal.Copy(shpObject.padfY, padfY, 0, nVertices);

                int iField = Shapelib.DBFGetFieldIndex(hDBF, "名称");
                string entityName = Shapelib.DBFReadStringAttribute(hDBF, iShape, iField);
                // var entityName = Enum.GetName(pshpType.GetType(), pshpType) + iShape;
                var points = new List<PointLatLng>();
                var entityNode = new TreeNode();
                entityNode.Text = entityName;
                switch (pshpType)
                {
                    case Shapelib.ShapeType.Point:
                        var pointMarker = string.IsNullOrEmpty(xmlFileName) ? GetPoint(padfX[0], padfY[0]) : GetTransformedPoint(padfX[0], padfY[0], param4);
                        points.Add(pointMarker);
                        UpdateOverlay(ref overlay, pshpType, points, entityName);
                        entityNode.Tag = overlay.Id;
                        break;
                    default:
                        var minPoint = new PointLatLng();
                        var maxPoint = new PointLatLng();
                        for (int i = 0; i < nVertices; i++)
                        {
                            var point = string.IsNullOrEmpty(xmlFileName) ? GetPoint(padfX[i], padfY[i]) : GetTransformedPoint(padfX[i], padfY[i], param4);
                            points.Add(point);

                            if (i == 0)
                            {
                                minPoint.Lat = maxPoint.Lat = point.Lat;
                                minPoint.Lng = maxPoint.Lng = point.Lng;
                            }
                            else
                            {
                                if (point.Lng > maxPoint.Lng) { maxPoint.Lng = point.Lng; }
                                if (point.Lat > maxPoint.Lat) { maxPoint.Lat = point.Lat; }
                                if (point.Lng < minPoint.Lng) { minPoint.Lng = point.Lng; }
                                if (point.Lat < minPoint.Lat) { minPoint.Lat = point.Lat; }
                            }
                        }

                        UpdateOverlay(ref overlay, pshpType, points, entityName);
                        var entityRect = new RectLatLng(maxPoint.Lat, minPoint.Lng, maxPoint.Lng - minPoint.Lng, maxPoint.Lat - minPoint.Lat);
                        entityNode.Tag = entityRect;
                        break;
                }
                node.Nodes.Add(entityNode);
            }
            Shapelib.DBFClose(hDBF);
            Shapelib.SHPClose(hSHP);

            m_layerWindow.AddNode(node);

            m_mapDocument.AddOverlayer(overlay);
            if (pshpType == Shapelib.ShapeType.Point)
                m_mapDocument.Zoom(overlay.Id);
            else
                m_mapDocument.Zoom(rect);
        }

        public void Zoom(object rect)
        {
            if (rect.GetType() == typeof(RectLatLng))
                m_mapDocument.Zoom((RectLatLng)rect);
            else
                m_mapDocument.Zoom(rect.ToString());
        }

        public void ShowReferencePoint(PointLatLng point)
        {
            m_outputWindow.ShowReferencePoint(point);
        }

        public void ShowPixelPoint(PointLatLng point)
        {
            m_outputWindow.ShowPixelPoint(point);
        }

        private void menuItemAddPixelPoint_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag= menuItemAddPixelPoint.Checked ? OperateFlagType.None : OperateFlagType.AddPixelPoint;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void menuItemAddReferencePoint_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag = menuItemAddReferencePoint.Checked ? OperateFlagType.None : OperateFlagType.AddReferencePoint;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void menuItemClearPixelPoints_Click(object sender, EventArgs e)
        {
            m_mapDocument.ClearLayer(OverlayerType.PixelPoint);
            m_outputWindow.ClearPixelPoints();
        }

        private void menuItemClearReferencePoints_Click(object sender, EventArgs e)
        {
            m_mapDocument.ClearLayer(OverlayerType.ReferencePoint);
            m_outputWindow.ClearReferencePoints();
        }

        private void menuItemSaveCoordinates_Click(object sender, EventArgs e)
        {
            m_outputWindow.ExportCoordinates();
        }

        private void menuItemDrawPolygon_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag = menuItemDrawPolygon.Checked ? OperateFlagType.None : OperateFlagType.DrawPolygon;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void menuItemClearLayers_Click(object sender, EventArgs e)
        {
            m_layerWindow.ClearLayers();
            m_mapDocument.ClearLayer(OverlayerType.ShpFile);
        }

        public void ShowOrHidderOverlay(string layerName, bool isVisibile)
        {
            m_mapDocument.ShowOrHidderOverlay(layerName, isVisibile);
        }

        private void menuItemDrawMarker_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag = menuItemDrawMarker.Checked ? OperateFlagType.None : OperateFlagType.DrawMarker;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void menuItemDrawRoute_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag = menuItemDrawRoute.Checked ? OperateFlagType.None : OperateFlagType.DrawPolyline;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void UpdateMenuItemAndToolButtonCheckedState()
        {
            var menuItem = new ToolStripMenuItem();
            var toolButton = new ToolStripButton();
            switch (m_mapDocument.OperateFlag)
            {
                case OperateFlagType.DrawPolyline:
                    menuItem = menuItemDrawRoute;
                    toolButton = toolStripButtonDrawRoute;
                    break;
                case OperateFlagType.DrawMarker:
                    menuItem = menuItemDrawMarker;
                    toolButton = toolStripButtonDrawMarker;
                    break;
                case OperateFlagType.DrawPolygon:
                    menuItem = menuItemDrawPolygon;
                    toolButton = toolStripButtonDrawPolygon;
                    break;
                case OperateFlagType.AddPixelPoint:
                    menuItem = menuItemAddPixelPoint;
                    toolButton = toolStripButtonAddPixelPoint;
                    break;
                case OperateFlagType.AddReferencePoint:
                    menuItem = menuItemAddReferencePoint;
                    toolButton = toolStripButtonAddReferencePoint;
                    break;
                case OperateFlagType.QueryAttribute:
                    menuItem = menuItemQueryAttribute;
                    toolButton = toolStripButtonQueryAttribute;
                    break;
                case OperateFlagType.None:
                    foreach (var item in m_menuItems) { item.Checked = false; }
                    foreach (var item in m_toolButtons) { item.Checked = false; }
                    return;
            }

            foreach(var item in m_menuItems)
            {
                item.Checked = item.Equals(menuItem) ? true : false;
            }

            foreach (var item in m_toolButtons)
            {
                item.Checked = item.Equals(toolButton) ? true : false;
            }
        }

        private void menuItemClearSketchPoints_Click(object sender, EventArgs e)
        {
            m_mapDocument.ClearLayer(OverlayerType.SketchPoint);
        }

        private void menuItemClearSketchPolylines_Click(object sender, EventArgs e)
        {
            m_mapDocument.ClearLayer(OverlayerType.SketchPolyline);
        }

        private void menuItemClearSketchPolygons_Click(object sender, EventArgs e)
        {
            m_mapDocument.ClearLayer(OverlayerType.SketchPolygon);
        }

        private void menuItemClearOutputText_Click(object sender, EventArgs e)
        {
            m_outputWindow.Clear();
        }

        private void menuItemExportSketch_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Shp文件(*.shp)|*.shp";
            dialog.AddExtension = true;
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                var pathName = Path.GetDirectoryName(dialog.FileName);
                var fileName = Path.GetFileNameWithoutExtension(dialog.FileName);

                var nVertices = m_mapDocument.GetSketchPointCount();
                double[] adfX = new double[1];
                double[] adfY = new double[1];
                double[] adfZ = new double[1];

                var shpFileName = string.Format(@"{0}\{1}_point.shp", pathName, fileName);
                var hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.Point);
                var dbfFileName = string.Format(@"{0}\{1}_point.dbf", pathName, fileName);
                var hDBF = Shapelib.DBFCreate(dbfFileName);
                int iField = Shapelib.DBFAddField(hDBF, "Id", Shapelib.DBFFieldType.FTString, 50, 0);

                for (int i=0;i< nVertices;i++)
                {
                    m_mapDocument.GetSketchPointInfo(i,ref adfX, ref adfY, ref adfZ);
                    var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.Point, nVertices, adfX, adfY, adfZ);
                    Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    Shapelib.DBFWriteStringAttribute(hDBF, i, iField, string.Format("{0}#草图点",i+1));
                }
                Shapelib.DBFClose(hDBF);
                Shapelib.SHPClose(hSHP);
                m_outputWindow.OutputText("点文件已存为：" + shpFileName);

                nVertices = m_mapDocument.GetSketchPolylineCount();
                shpFileName = string.Format(@"{0}\{1}_polyline.shp", pathName, fileName);
                hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.PolyLine);
                dbfFileName = string.Format(@"{0}\{1}_polyline.dbf", pathName, fileName);
                hDBF = Shapelib.DBFCreate(dbfFileName);
                iField = Shapelib.DBFAddField(hDBF, "Id", Shapelib.DBFFieldType.FTInteger, 14, 0);

                for (int i = 0; i < nVertices; i++)
                {
                    var count = m_mapDocument.GetSketchPolylinePointCount(i);
                    adfX = new double[count];
                    adfY = new double[count];
                    adfZ = new double[count];

                    m_mapDocument.GetSketchPolylineInfo(i, ref adfX, ref adfY, ref adfZ);

                    var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.PolyLine, count, adfX, adfY, adfZ);
                    var iLine = Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    Shapelib.DBFWriteIntegerAttribute(hDBF, i, iField, i);
                }
                Shapelib.DBFClose(hDBF);
                Shapelib.SHPClose(hSHP);
                
                m_outputWindow.OutputText("线文件已存为：" + shpFileName);

                nVertices = m_mapDocument.GetSketchPolygonCount();
                shpFileName = string.Format(@"{0}\{1}_polygon.shp", pathName, fileName);
                hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.Polygon);
                dbfFileName = string.Format(@"{0}\{1}_polygon.dbf", pathName, fileName);
                hDBF = Shapelib.DBFCreate(dbfFileName);
                iField = Shapelib.DBFAddField(hDBF, "Id", Shapelib.DBFFieldType.FTInteger, 14, 0);

                for (int i = 0; i < nVertices; i++)
                {
                    var count = m_mapDocument.GetSketchPolygonPointCount(i);
                    adfX = new double[count];
                    adfY = new double[count];
                    adfZ = new double[count];

                    m_mapDocument.GetSketchPolygonInfo(i, ref adfX, ref adfY, ref adfZ);

                    var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.Polygon, count, adfX, adfY, adfZ);
                    var iPolygon = Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    Shapelib.DBFWriteIntegerAttribute(hDBF, i, iField, i);
                }
                Shapelib.DBFClose(hDBF);
                Shapelib.SHPClose(hSHP);
                m_outputWindow.OutputText("面文件已存为：" + shpFileName);
            }  
        }

        private void menuItemExportTransformedSketch_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Shp文件(*.shp)|*.shp";
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var xmlDialog = new OpenFileDialog();
                xmlDialog.Filter = "控制点坐标(*.xml)|*.xml";
                if (xmlDialog.ShowDialog() == DialogResult.OK)
                {
                    List<HorizontalCoordinate> sourceCoordinates, targetCoordinates;
                    KVS.Open(dialog.FileName, out targetCoordinates, out sourceCoordinates);
                    var param4 = LinearTransformation.GetTransformationParameter(sourceCoordinates, targetCoordinates);

                    var pathName = Path.GetDirectoryName(dialog.FileName);
                    var fileName = Path.GetFileNameWithoutExtension(dialog.FileName);

                    var nVertices = m_mapDocument.GetSketchPointCount();
                    double[] adfX = new double[1];
                    double[] adfY = new double[1];
                    double[] adfZ = new double[1];

                    var shpFileName = string.Format(@"{0}\{1}_point.shp", pathName, fileName);
                    var hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.Point);

                    for (int i = 0; i < nVertices; i++)
                    {
                        m_mapDocument.GetSketchPointInfo(i, ref adfX, ref adfY, ref adfZ, param4);
                        var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.Point, nVertices, adfX, adfY, adfZ);
                        Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    }
                    Shapelib.SHPClose(hSHP);
                    m_outputWindow.OutputText("点文件已存为：" + shpFileName);

                    nVertices = m_mapDocument.GetSketchPolylineCount();
                    shpFileName = string.Format(@"{0}\{1}_polyline.shp", pathName, fileName);
                    hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.PolyLine);

                    for (int i = 0; i < nVertices; i++)
                    {
                        var count = m_mapDocument.GetSketchPolylinePointCount(i);
                        adfX = new double[count];
                        adfY = new double[count];
                        adfZ = new double[count];

                        m_mapDocument.GetSketchPolylineInfo(i, ref adfX, ref adfY, ref adfZ, param4);

                        var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.PolyLine, count, adfX, adfY, adfZ);
                        var iLine = Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    }
                    Shapelib.SHPClose(hSHP);
                    m_outputWindow.OutputText("线文件已存为：" + shpFileName);

                    nVertices = m_mapDocument.GetSketchPolygonCount();
                    shpFileName = string.Format(@"{0}\{1}_polygon.shp", pathName, fileName);
                    hSHP = Shapelib.SHPCreate(shpFileName, Shapelib.ShapeType.Polygon);

                    for (int i = 0; i < nVertices; i++)
                    {
                        var count = m_mapDocument.GetSketchPolygonPointCount(i);
                        adfX = new double[count];
                        adfY = new double[count];
                        adfZ = new double[count];

                        m_mapDocument.GetSketchPolygonInfo(i, ref adfX, ref adfY, ref adfZ, param4);

                        var psObject = Shapelib.SHPCreateSimpleObject(Shapelib.ShapeType.Polygon, count, adfX, adfY, adfZ);
                        var iPolygon = Shapelib.SHPWriteObject(hSHP, -1, psObject);
                    }
                    Shapelib.SHPClose(hSHP);
                    m_outputWindow.OutputText("面文件已存为：" + shpFileName);
                }
            }
        }

        private void menuItemQueryAttribute_Click(object sender, EventArgs e)
        {
            m_mapDocument.OperateFlag = menuItemQueryAttribute.Checked ? OperateFlagType.None : OperateFlagType.QueryAttribute;
            UpdateMenuItemAndToolButtonCheckedState();
        }

        private void menuItemDownload_Click(object sender, EventArgs e)
        {
            
        }

        private void menuItemExport_Click(object sender, EventArgs e)
        {
            using (FileDialog dlg = new SaveFileDialog())
            {
                dlg.CheckPathExists = true;
                dlg.CheckFileExists = false;
                dlg.AddExtension = true;
                dlg.DefaultExt = "gmdb";
                dlg.ValidateNames = true;
                dlg.Title = "导出离线数据库";
                dlg.FileName = "";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dlg.Filter = "离线数据库(*.gmdb)|*.gmdb";
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool ok = GMaps.Instance.ExportToGMDB(dlg.FileName);
                    if (ok)
                    {
                        MessageBox.Show("Complete!", "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed!", "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
