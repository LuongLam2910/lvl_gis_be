using System;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using GeoAPI.CoordinateSystems.Transformations;

namespace App.CongAnGis.Services.Business.ConverterProj.VN2000
{
    public class Reproject
    {
        public static string TransformGeometryToWKT(Geometry geom, IMathTransform transform)
        {
            return Transform(geom, transform).AsText();
        }
        public static Geometry TransformGeometryFromWKT(String geom, IMathTransform transform)
        {
            WKTReader wktreader = new WKTReader();
            Geometry geomIn = wktreader.Read(geom);
            return Transform(geomIn, transform);
        }
        public static String TransformGeometryFromWKTToWKT(String geom, IMathTransform transform)
        {
            WKTReader wktreader = new WKTReader();
            Geometry geomIn = wktreader.Read(geom);
            Geometry geomOut = Transform(geomIn, transform);
            return geomOut.AsText();
        }

        public static Geometry Transform(Geometry geom, IMathTransform transform)
        {
            geom = geom.Copy();
            geom.Apply(new MTF(transform));
            return geom;
        }
    }

    sealed class MTF : NetTopologySuite.Geometries.ICoordinateSequenceFilter
    {
        private readonly IMathTransform _mathTransform;

        public MTF(IMathTransform mathTransform) => _mathTransform = mathTransform;

        public bool Done => false;
        public bool GeometryChanged => true;
        public void Filter(NetTopologySuite.Geometries.CoordinateSequence seq, int i)
        {
            double[] result = new double[] { };
            double x = seq.GetX(i);
            double y = seq.GetY(i);
            double z = seq.GetZ(i);
            result = _mathTransform.Transform(new double[] { x, y, z });
          
            seq.SetX(i, result[0]);
            seq.SetY(i, result[1]);
            if(result.Length == 3)
                seq.SetZ(i, result[2]);
        }
    }
}
