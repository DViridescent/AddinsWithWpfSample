using Rhino.Geometry;
using Sample.Geometry;
using System;
using System.Collections.Generic;
using System.Text;
using Point = Sample.Geometry.Point;

namespace Sample.Rhino.Utils
{
    internal static class Extensions
    {
        internal static Point ToSampleGeometry(this Point3d point) => new Point(point.X, point.Y, point.Z);
    }
}
