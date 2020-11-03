using ESRI.ArcGIS.Geometry;
using HelperArcGIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperArcGIS
{
    public class HelperGeometry
    {
        private static object _missing = Type.Missing;
        public IGeometry Get_GeometryPolygon(PolygonModel polygon)
        {
            IPoint[] centerPointArray = new IPoint[polygon.Posts.Count];
            for (int i = 0; i < polygon.Posts.Count; i++)
            {
                centerPointArray[i] = ConstructPoint2D(polygon.Posts[i].X, polygon.Posts[i].Y);
            }

            IGeometryCollection geometryCollection = new PolygonClass();
            IPointCollection pointCollection = new RingClass();
            for (int i = 0; i < centerPointArray.Length; i++)
                pointCollection.AddPoint(centerPointArray[i], ref _missing, ref _missing);
            pointCollection.AddPoint(pointCollection.get_Point(0), ref _missing, ref _missing);
            geometryCollection.AddGeometry(pointCollection as IGeometry, ref _missing, ref _missing);

            MakeZAware(geometryCollection as IGeometry);
            ITopologicalOperator topologicalOperator = geometryCollection as ITopologicalOperator;
            topologicalOperator.Simplify();
            return geometryCollection as IGeometry;
        }

        public IGeometry Get_GeometryPolygonRing(PolygonModel polygon)
        {
            IPoint[] centerPointArray = new IPoint[polygon.Posts.Count];
            for (int i = 0; i < polygon.Posts.Count; i++)
            {
                centerPointArray[i] = ConstructPoint2D(polygon.Posts[i].X, polygon.Posts[i].Y);
            }


            IPointCollection pointCollection = new RingClass();
            for (int i = 0; i < centerPointArray.Length; i++)
                pointCollection.AddPoint(centerPointArray[i], ref _missing, ref _missing);
            pointCollection.AddPoint(pointCollection.get_Point(0), ref _missing, ref _missing);

            Ring ring = new RingClass();
            ring.AddPointCollection(pointCollection);

            IGeometryCollection geometryCollection = new PolygonClass();
            geometryCollection.AddGeometry(ring as IGeometry, ref _missing, ref _missing);

            MakeZAware(geometryCollection as IGeometry);
            ITopologicalOperator topologicalOperator = geometryCollection as ITopologicalOperator;
            topologicalOperator.Simplify();
            return geometryCollection as IGeometry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postition"></param>
        /// <param name="RingRadius">外扩范围</param>
        /// <returns></returns>
        public IGeometry Get_GeometryCircle(IPoint postition, double ringRadius=0.1)
        {
             double RingVertexCount = 36;
             double RingDegrees = 360.0;
             double VectorComponentOffset = 0.0000001;
             double RingRadius = ringRadius;

            IPoint centerPoint = ConstructPoint2D(postition.X, postition.Y);
            IPointCollection pointCollection = new RingClass();

            IVector3D upperAxisVector3D = new Vector3DClass();
            upperAxisVector3D.SetComponents(0, 0, 10);
            IVector3D lowerAxisVector3D = new Vector3DClass();
            lowerAxisVector3D.SetComponents(0, 0, -10);
            lowerAxisVector3D.XComponent += VectorComponentOffset;
            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;
            normalVector3D.Magnitude = RingRadius;
            double rotationAngleInRadians = (RingDegrees / RingVertexCount) * (Math.PI / 180);

            for (int i = 0; i < RingVertexCount; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);
                pointCollection.AddPoint(ConstructPoint2D(centerPoint.X + normalVector3D.XComponent, centerPoint.Y + normalVector3D.YComponent), ref _missing, ref _missing);
            }

            pointCollection.AddPoint(pointCollection.get_Point(0), ref _missing, ref _missing);

            IGeometryCollection geometryCollection = new PolygonClass();
            geometryCollection.AddGeometry(pointCollection as IGeometry, ref _missing, ref _missing);

            MakeZAware(geometryCollection as IGeometry);
            ITopologicalOperator topologicalOperator = geometryCollection as ITopologicalOperator;
            topologicalOperator.Simplify();

            return geometryCollection as IGeometry;
        }

        /// <summary>
        /// IGeometry 转换为点
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public IPoint GetIPoint_IGeometry(IGeometry geometry)
        {
            switch (geometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    IPoint pointCollection = geometry as IPoint;
                    if (pointCollection != null) return pointCollection;
                    break;
            }
            return null;
        }

        private static IPoint ConstructPoint2D(double x, double y)
        {
            return ConstructPoint2D((decimal)x, (decimal)y);
        }

        private static IPoint ConstructPoint2D(decimal x, decimal y)
        {

            IPoint point = new PointClass();

            point.PutCoords((double)x, (double)y);

            return point;

        }

        private static void MakeZAware(IGeometry geometry)
        {

            IZAware zAware = geometry as IZAware;

            zAware.ZAware = false;

        }
    }
}
