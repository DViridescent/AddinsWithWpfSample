using Autodesk.AutoCAD.Geometry;
using Sample.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.AutoCAD.Utils
{
    internal static class Extensions
    {
        internal static Point ToSampleGeometry(this Point3d point) => new Point(point.X, point.Y, point.Z);
    }
}
