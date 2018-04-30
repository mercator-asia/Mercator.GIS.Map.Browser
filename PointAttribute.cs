using GMap.NET;
using Mercator.GIS.Projection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercator.GIS.Map.Browser
{
    public class PointAttribute
    {
        private double lat;
        private double lng;

        public PointAttribute(PointLatLng point)
        {
            lat = point.Lat;
            lng = point.Lng;
        }

        [DescriptionAttribute("经度的数字形式"), CategoryAttribute("坐标")]
        public double L
        {
            get
            {
                return lng;
            }
            set
            {
                lng = value;
            }
        }
        [DescriptionAttribute("纬度的数字形式"), CategoryAttribute("坐标")]
        public double B
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
            }
        }
        [DescriptionAttribute("西安80坐标系的X坐标"), CategoryAttribute("坐标")]
        public double X
        {
            get
            {
                var projection = new GaussKrugerProjection();
                projection.Ellipsoid = ReferenceEllipsoid.International1975;
                double E, N;
                projection.Forward(lat, lng, out E, out N);
                return E;
            }
        }
        [DescriptionAttribute("西安80坐标系的Y坐标"), CategoryAttribute("坐标")]
        public double Y
        {
            get
            {
                var projection = new GaussKrugerProjection();
                projection.Ellipsoid = ReferenceEllipsoid.International1975;
                double E, N;
                projection.Forward(lat, lng, out E, out N);

                return N;
            }
        }
    }
}
