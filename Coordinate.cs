using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GMap.NET;

namespace Mercator.GIS.Map.Browser
{
    /// <summary>
    /// 点
    /// </summary>
    public class XY
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public XY()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// 经纬度
    /// </summary>
    public class LB
    {
        /// <summary>
        /// 经度
        /// </summary>
        public string L
        {
            get;
            set;
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public string B
        {
            get;
            set;
        }

        /// <summary>
        /// 经度的数字形式
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// 纬度的数字形式
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LB()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="l">经度</param>
        /// <param name="b">纬度</param>
        public LB(string l, string b)
        {
            L = l;
            B = b;

            Latitude = Coordinate.DMS2Digital(B);
            Longitude = Coordinate.DMS2Digital(L);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        public LB(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

            L = Coordinate.Digital2DMS(longitude);
            B = Coordinate.Digital2DMS(latitude);
        }
    }

    public class Rect
    {
        public double Latitude;
        public double Longitude;
        public double Width;
        public double Height;

        public RectLatLng Rectange
        {
            get
            {
                return new RectLatLng(Latitude, Longitude, Width, Height);
            }
        }

        public Rect()
        {

        }

        public Rect(double latitude,double longitude,double width,double height)
        {
            Latitude = latitude;
            Longitude = longitude;
            Width = width;
            Height = height;
        }
    }
        

    /// <summary>
    /// 坐标类
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// 不同坐标系之间进行转换的四参数
        /// </summary>
        /// <param name="coordinateFileName">坐标文件</param>
        /// <param name="direction">转换方向(经纬度转平面直角坐标为正向true，平面直接坐标转经纬度为反向false)</param>
        /// <returns></returns>
        public static Matrix GetFourParameter(string coordinateFileName,bool direction = false)
        {
            if (!File.Exists(coordinateFileName)) { return null; }

            var pixelPoints = new List<XY>();
            var referencePoints = new List<XY>();

            var document = new XmlDocument();
            document.Load(coordinateFileName);
            var root = document.DocumentElement;
            foreach (XmlNode coordinateNode in root.ChildNodes)
            {
                foreach(XmlNode node in coordinateNode.ChildNodes)
                {
                    double lng = double.MinValue, lat = double.MinValue;
                    foreach (XmlNode lbNode in node.ChildNodes)
                    {
                        switch (lbNode.Name.ToLower())
                        {
                            case "longitude":
                                lng = Convert.ToDouble(lbNode.InnerText.Trim());
                                break;
                            case "latitude":
                                lat = Convert.ToDouble(lbNode.InnerText.Trim());
                                break;
                        }
                    }

                    switch (node.Name.ToLower())
                    {
                        case "pixel":
                            if(lng != double.MinValue && lat != double.MinValue)
                            {
                                var pixelPoint = LB2XYWithoutZone(lng, lat);
                                pixelPoints.Add(pixelPoint);
                            }
                            break;
                        case "reference":
                            if (lng != double.MinValue && lat != double.MinValue)
                            {
                                var referencePoint = LB2XYWithoutZone(lng, lat);
                                referencePoints.Add(referencePoint);
                            }
                            break;
                    }
                }
            }

            var X = new Matrix(new double[4, 1]);
            var B = new Matrix(new double[pixelPoints.Count * 2, 4]);
            var L = new Matrix(new double[pixelPoints.Count * 2, 1]);
            var P = new Matrix(new double[pixelPoints.Count * 2, pixelPoints.Count * 2]);
            for (int i = 0; i < pixelPoints.Count * 2; i++)
            {
                for (int j = 0; j < pixelPoints.Count * 2; j++)
                {
                    if (i == j) { P[i, j] = 1; } else { P[i, j] = 0; }
                }
            }

            if(!direction)
            {
                // B和L赋值
                var index = 0;
                for (int i = 0; i < pixelPoints.Count; i++)
                {
                    var xy84 = pixelPoints[i];
                    var xy80 = referencePoints[i];

                    B[index, 0] = 1;
                    B[index, 1] = 0;
                    B[index, 2] = xy80.X;
                    B[index, 3] = xy80.Y;

                    B[index + 1, 0] = 0;
                    B[index + 1, 1] = 1;
                    B[index + 1, 2] = xy80.Y;
                    B[index + 1, 3] = xy80.X * -1;

                    L[index, 0] = xy84.X;
                    L[index + 1, 0] = xy84.Y;

                    index = index + 2;
                }
            }
            else
            {
                // B和L赋值
                var index = 0;
                for (int i = 0; i < pixelPoints.Count; i++)
                {
                    var xy84 = pixelPoints[i];
                    var xy80 = referencePoints[i];

                    B[index, 0] = 1;
                    B[index, 1] = 0;
                    B[index, 2] = xy84.X;
                    B[index, 3] = xy84.Y;

                    B[index + 1, 0] = 0;
                    B[index + 1, 1] = 1;
                    B[index + 1, 2] = xy84.Y;
                    B[index + 1, 3] = xy84.X * -1;

                    L[index, 0] = xy80.X;
                    L[index + 1, 0] = xy80.Y;

                    index = index + 2;
                }
            }

            X = (B.Transpose() * P * B).Inverse() * B.Transpose() * P * L;

            return X;

        }

        public static XY TransformByFourParameter(XY source, Matrix parameter)
        {
            /*
            var Delta = new Matrix(new double[2, 1]);
            Delta[0, 0] = parameter[0, 0];
            Delta[1, 0] = parameter[1, 0];

            var R = new Matrix(new double[2, 2]);
            R[0, 0] = parameter[2, 0];
            R[0, 1] = parameter[3, 0] * -1;
            R[1, 0] = parameter[3, 0];
            R[1, 1] = parameter[2, 0];

            var x1y1 = new Matrix(new double[2, 1]);
            x1y1[0, 0] = source.X;
            x1y1[1, 0] = source.Y;

            var x2y2 = Delta + R * x1y1;
            return new XY(x2y2[0, 0], x2y2[1, 0]);

            */

            var x = parameter[0, 0] + source.X * parameter[2, 0] + source.Y * parameter[3, 0];
            var y = parameter[1, 0] + source.Y * parameter[2, 0] - source.X * parameter[3, 0];
            return new XY(x, y);
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="source">源点</param>
        /// <param name="param4">四参数</param>
        /// <returns>目标点</returns>
        public static XY Transform(XY source, Matrix param4)
        {
            /*
            var a = param4[2, 0];
            var b = param4[3, 0];

            var Delta = new Matrix(new double[2, 1]);
            Delta[0, 0] = param4[0, 0];
            Delta[1, 0] = param4[1, 0];

            var R = new Matrix(new double[2, 2]);
            R[0, 0] = param4[2, 0];
            R[0, 1] = param4[3, 0] * -1;
            R[1, 0] = param4[3, 0];
            R[1, 1] = param4[2, 0];

            var x1y1 = new Matrix(new double[2, 1]);
            x1y1[0, 0] = source.X;
            x1y1[1, 0] = source.Y;

            // var x2y2 = K*(Delta + R * x1y1);
            var x2y2 = Delta + R * x1y1;
            return new XY(x2y2[0, 0], x2y2[1, 0]);
            */

            var x = param4[0, 0] + source.X * param4[2, 0] + source.Y * param4[3, 0];
            var y = param4[1, 0] + source.Y * param4[2, 0] - source.X * param4[3, 0];

            return new XY(x, y);
        }

        /// <summary>
        /// 获取西安80投影坐标系
        /// </summary>
        /// <param name="zone">投影带号</param>
        /// <param name="centralMeridian">中央子午线</param>
        /// <returns></returns>
        public static IProjectedCoordinateSystem Xian80(int zone)
        {
            var xian80 = string.Format("PROJCS[\"Xian80\","
                    + "GEOGCS[\"Xian80\","
                    + "DATUM[\"Xian80\", SPHEROID[\"Xian80\", 6378140.0,298.257]],"
                      + "PRIMEM[\"Greenwich\", 0.0],"
                      + "UNIT[\"Degree\", 0.0174532925199433]],"
                    + "PROJECTION[\"Transverse Mercator\"],"
                    + "PARAMETER[\"False_Easting\", 500000.0],"
                    + "PARAMETER[\"False_Northing\", 0.0],"
                    + "PARAMETER[\"Central_Meridian\", {0} ],"
                    + "PARAMETER[\"Scale_Factor\", 1.0],"
                    + "PARAMETER[\"Latitude_Of_Origin\", 0.0],"
                    + "UNIT[\"Meter\", 1.0]];", zone * 3);
            return CoordinateSystemWktReader.Parse(xian80) as IProjectedCoordinateSystem;
        }

        /// <summary>
        /// 获取WGS84投影坐标系
        /// </summary>
        /// <param name="zone">投影带号</param>
        /// <returns></returns>
        public static IProjectedCoordinateSystem WGS84(int zone)
        {
            var WGS84 = string.Format("PROJCS[\"WGS84\","
                    + "GEOGCS[\"WGS84\","
                    + "DATUM[\"WGS84\", SPHEROID[\"WGS84\", 6378137, 298.257223563]],"
                      + "PRIMEM[\"Greenwich\", 0.0],"
                      + "UNIT[\"Degree\", 0.0174532925199433]],"
                    + "PROJECTION[\"Transverse Mercator\"],"
                    + "PARAMETER[\"False_Easting\", 500000.0],"
                    + "PARAMETER[\"False_Northing\", 0.0],"
                    + "PARAMETER[\"Central_Meridian\", {0} ],"
                    + "PARAMETER[\"Scale_Factor\", 0.9996],"
                    + "PARAMETER[\"Latitude_Of_Origin\", 0.0],"
                    + "UNIT[\"Meter\", 1.0]];",zone * 3);
            return CoordinateSystemWktReader.Parse(WGS84) as IProjectedCoordinateSystem;
        }

        /// <summary>
        /// 经纬度转平面直角坐标(横轴墨卡托投影坐标正算)
        /// </summary>
        /// <param name="lng">经度(数字形式)</param>
        /// <param name="lat">纬度(数字形式)</param>
        /// <returns></returns>
        public static XY LB2XY(double longitude, double latitude)
        {
            var source = new double[] { longitude, latitude };
            var sourceGCS = GeographicCoordinateSystem.WGS84;
            var zone = (int)Round(longitude / 3, 0);
            var targetGCS = Xian80(zone);
            var transformationFactory = new CoordinateTransformationFactory();
            var transformation = transformationFactory.CreateFromCoordinateSystems(sourceGCS, targetGCS);
            var target = transformation.MathTransform.Transform(source);
            var xy = new XY(target[1], target[0] + zone * 1000000);
            return xy;
        }

        public static XY LB2XYWithoutZone(double longitude, double latitude)
        {
            var source = new double[] { longitude, latitude };
            var sourceGCS = GeographicCoordinateSystem.WGS84;
            var zone = (int)Round(longitude / 3, 0);
            var targetGCS = Xian80(zone);
            var transformationFactory = new CoordinateTransformationFactory();
            var transformation = transformationFactory.CreateFromCoordinateSystems(sourceGCS, targetGCS);
            var target = transformation.MathTransform.Transform(source);
            var xy = new XY(target[1], target[0]);
            return xy;
        }

        /// <summary>
        /// 经纬度转平面直角坐标(横轴墨卡托投影坐标正算)
        /// </summary>
        /// <param name="lb">经纬度</param>
        /// <returns></returns>
        public static XY LB2XY(LB lb)
        {
            return LB2XY(lb.Longitude, lb.Latitude);
        }

        /// <summary>
        /// 经纬度批量转平面直角坐标(横轴墨卡托投影正算)
        /// </summary>
        /// <param name="lbList">经纬度列表</param>
        /// <returns>坐标列表</returns>
        public static List<XY> LB2XY(List<LB> lbList)
        {
            var xyList = new List<XY>();
            foreach(var lb in lbList)
            {
                var xy = LB2XY(lb.Longitude, lb.Latitude);
                xyList.Add(xy);
            }
            return xyList;
        }

        /// <summary>
        /// 平面直角坐标转经纬度(横轴墨卡托投影反算)
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>经纬度</returns>
        public static LB XY2LB(double x, double y)
        {
            var zone = (int)y / 1000000;
            var source = new double[] { y - zone * 1000000, x };
            var sourceGCS = Xian80(zone);
            var targetGCS = GeographicCoordinateSystem.WGS84;
            var transformationFactory = new CoordinateTransformationFactory();
            var transformation = transformationFactory.CreateFromCoordinateSystems(sourceGCS, targetGCS);
            var target = transformation.MathTransform.Transform(source);
            var lb = new LB(target[0], target[1]);
            return lb;
        }

        /// <summary>
        /// 平面直角坐标转经纬度(横轴墨卡托投影反算)
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        public static LB XY2LB(XY xy)
        {
            return XY2LB(xy.X, xy.Y);
        }

        /// <summary>
        /// 平面直角坐标批量转经纬度(横轴墨卡托投影反算)
        /// </summary>
        /// <param name="xyList">坐标列表</param>
        /// <returns>经纬度列表</returns>
        public static List<LB> XY2LB(List<XY> xyList)
        {
            var lbList = new List<LB>();
            foreach (var xy in xyList)
            {
                var lb = XY2LB(xy.X, xy.Y);
                lbList.Add(lb);
            }
            return lbList;
        }

        /// <summary>
        /// 度°分′秒″转为数字形式
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DMS2Digital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');           //度的符号对应的 Unicode 代码为：00B0[1]（六十进制），显示为°。
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');           //分的符号对应的 Unicode 代码为：2032[1]（六十进制），显示为′。
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');           //秒的符号对应的 Unicode 代码为：2033[1]（六十进制），显示为″。
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }

        /// <summary>
        /// 数字形式转换为度°分′秒″
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string Digital2DMS(double d)
        {
            int Degree = Convert.ToInt32(Math.Truncate(d));//度
            d = d - Degree;
            int M = Convert.ToInt32(Math.Truncate((d) * 60));//分
            int S = Convert.ToInt32(Math.Round((d * 60 - M) * 60));

            if (S == 60)
            {
                M = M + 1;
                S = 0;
            }
            if (M == 60)
            {
                M = 0;
                Degree = Degree + 1;
            }
            string rstr = Degree.ToString() + "°";
            if (M < 10)
            {
                rstr = rstr + "0" + M.ToString();
            }
            else
            {
                rstr = rstr + M.ToString();
            }

            rstr += "′";

            if (S < 10)
            {
                rstr = rstr + "0" + S.ToString();
            }
            else
            {
                rstr = rstr + S.ToString();
            }

            rstr += "″";

            return rstr;
        }

        /// <summary>
        /// 四舍五入取整
        /// </summary>
        /// <param name="value">浮点数</param>
        /// <param name="digit">要保留的小数点位数</param>
        /// <returns></returns>
        public static double Round(double value, int digit)
        {
            double vt = Math.Pow(10, digit);
            //1.乘以倍数 + 0.5
            double vx = value * vt + 0.5;
            //2.向下取整
            double temp = Math.Floor(vx);
            //3.再除以倍数
            return (temp / vt);
        }

        /// <summary>
        /// 地球半径，单位米
        /// </summary>
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        public static double GetDistance(LB lb1, LB lb2)
        {
            var lat1 = lb1.Latitude;
            var lat2 = lb2.Latitude;
            var lng1 = lb1.Longitude;
            var lng2 = lb2.Longitude;
            double a = lat1 - lat2;
            double b = lng1 - lng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
    }
}
