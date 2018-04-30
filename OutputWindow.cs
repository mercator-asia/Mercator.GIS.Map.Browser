using GMap.NET;
using Mercator.GIS.Coordinate;
using Mercator.GIS.Coordinate.KVS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace Mercator.GIS.Map.Browser
{
    public partial class OutputWindow : ToolWindow
    {
        private List<PointLatLng> m_pixelPoints;
        private List<PointLatLng> m_referencePoints;

        public OutputWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            m_pixelPoints = new List<PointLatLng>();
            m_referencePoints = new List<PointLatLng>();
        }

        public void ShowPixelPoint(PointLatLng point)
        {
            m_pixelPoints.Add(point);
            this.textBox.AppendText(string.Format("{0}#像控点: {1},{2}{3}", m_pixelPoints.Count, point.Lat, point.Lng, Environment.NewLine));
        }

        public void ShowReferencePoint(PointLatLng point)
        {
            m_referencePoints.Add(point);
            this.textBox.AppendText(string.Format("{0}#参考点: {1},{2}{3}", m_referencePoints.Count, point.Lat, point.Lng, Environment.NewLine));
        }

        public void OutputText(string text)
        {
            this.textBox.AppendText(text + Environment.NewLine);
        }

        public void Clear()
        {
            this.textBox.Clear();
        }

        public void ExportCoordinates()
        {
            if (m_pixelPoints.Count <= 0)
            {
                this.textBox.AppendText("导出坐标失败：未设置像控点。" + Environment.NewLine);
                return;
            }
            if (m_referencePoints.Count <= 0)
            {
                this.textBox.AppendText("导出坐标失败：未设置参考点。" + Environment.NewLine);
                return;
            }
            if(m_referencePoints.Count!= m_pixelPoints.Count)
            {
                this.textBox.AppendText("导出坐标失败：像控点和参考点的数量不一致。"+ Environment.NewLine);
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.Filter = "控制点坐标(*.xml)|*.xml";
            dialog.AddExtension = true;
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                var sourceCoordinates = new List<GeocentricCoordinate>();
                var targetCoordinates = new List<GeocentricCoordinate>();

                for (int i = 0; i < m_pixelPoints.Count; i++)
                {
                    sourceCoordinates.Add(new GeocentricCoordinate(m_pixelPoints[i].Lat, m_pixelPoints[i].Lng));
                    targetCoordinates.Add(new GeocentricCoordinate(m_referencePoints[i].Lat, m_referencePoints[i].Lng));
                }

                KVS.Create(dialog.FileName, sourceCoordinates, targetCoordinates);
            }           
        }

        public void ClearReferencePoints()
        {
            m_referencePoints.Clear();
        }

        public void ClearPixelPoints()
        {
            m_pixelPoints.Clear();
        }
    }
}
