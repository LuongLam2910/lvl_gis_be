using System;
using System.Collections.Generic;
using App.CongAnGis.Services.Business.ConverterProj;
using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;
using ProjNet.CoordinateSystems.Transformations;

namespace App.CongAnGis.Services.Business.ConverterProj.VN2000
{
    /// <summary>
    /// Chuyển đổi hệ quy chiếu VN2000
    /// </summary>
    public class GisTransform : IMathTransform
    {
       
        public ProjectionInfo Source;
        public ProjectionInfo Target;
        /// <summary>
        /// Creates an instance of this class
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public GisTransform(ProjectionInfo source, ProjectionInfo target)
        {
            Source = source;
            Target = target;
        }



        public int DimSource
        {
            get { return Source.IsGeocentric ? 3 : 2; }
        }

        public int DimTarget
        {
            get { return Target.IsGeocentric ? 3 : 2; }
        }

        public bool Identity()
        {
            return Source.Equals(Target);
        }

        public string WKT
        {
            get { return string.Empty; }
        }

        public string XML
        {
            get { throw new NotSupportedException(); }
        }

        public double[,] Derivative(double[] point)
        {
            throw new NotSupportedException();
        }

        public List<double> GetCodomainConvexHull(List<double> points)
        {
            throw new NotSupportedException();
        }


        public IMathTransform Inverse()
        {
            return new GisTransform(Target, Source);
        }

        public double[] Transform(double[] point)
        {

            var xy = new[] { point[0], point[1] };
            var z = new double[1];

            if (DimSource > 2)
                z[0] = point[2];

            IDatumTransform idt = new DatumTransform(new IDatumTransformStage[] {
                new DatumTransformStage("-191.90441429 ", "-39.30318279", new Spheroid(), new Spheroid(), -0.00928836, 0.01975479, 0.00427372)
            });
            //DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, Source,0, Target, 0,idt,0, 1);
            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);
            //DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, Source, Target, 0, points.Count);

            if (DimTarget > 2)
                return new[] { xy[0], xy[1], z[0] };

            return xy;
        }

        public IList<double[]> TransformList(IList<double[]> points)
        {
            var xy = new double[2 * points.Count];
            var z = new double[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                xy[2 * i] = points[i][0];
                xy[2 * i + 1] = points[i][1];
                if (DimSource > 2)
                    z[i] = points[i][2];
            }

            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, points.Count);

            var ret = new List<double[]>(points.Count);
            if (DimTarget > 2)
            {
                for (int i = 0; i < points.Count; i++)
                    ret.Add(new[] { xy[2 * i], xy[2 * i + 1], z[i] });
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                    ret.Add(new[] { xy[2 * i], xy[2 * i + 1] });
            }
            return ret;
        }

        public IList<GeoAPI.Geometries.Coordinate> TransformList(IList<GeoAPI.Geometries.Coordinate> points)
        {
            var xy = new double[2 * points.Count];
            var z = new double[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                xy[2 * i] = points[i].X;
                xy[2 * i + 1] = points[i].Y;
                if (DimSource > 2)
                    z[i] = points[i].Z;
            }

            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, points.Count);

            var ret = new List<GeoAPI.Geometries.Coordinate>(points.Count);
            if (DimTarget > 2)
            {
                for (int i = 0; i < points.Count; i++)
                    ret.Add(new GeoAPI.Geometries.Coordinate(xy[2 * i], xy[2 * i + 1], z[i]));
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                    ret.Add(new GeoAPI.Geometries.Coordinate(xy[2 * i], xy[2 * i + 1]));
            }
            return ret;
        }

        public void Invert()
        {
            var tmp = Source;
            Source = Target;
            Target = tmp;
        }

        [Obsolete]
        public ICoordinate Transform(ICoordinate coordinate)
        {
            var xy = new[] { coordinate.X, coordinate.Y };
            var z = new[] { coordinate.Z };

            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);

            var ret = (ICoordinate)coordinate.Clone();
            ret.X = xy[0];
            ret.Y = xy[1];
            ret.Z = z[0];

            return ret;
        }

        public GeoAPI.Geometries.Coordinate Transform(GeoAPI.Geometries.Coordinate coordinate)
        {
            var xy = new[] { coordinate.X, coordinate.Y };
            double[] z = null;
            if (coordinate.Z != coordinate.Z)
                z = new[] { coordinate.Z };

            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);

            var ret = (GeoAPI.Geometries.Coordinate)coordinate.Copy();
            ret.X = xy[0];
            ret.Y = xy[1];
            if (z != null)
                ret.Z = z[0];

            return ret;
        }

        public ICoordinateSequence Transform(ICoordinateSequence coordinateSequence)
        {
            //use shortcut if possible
            //if (coordinateSequence is DotSpatialAffineCoordinateSequence)
            //    return TransformDotSpatialAffine((DotSpatialAffineCoordinateSequence)coordinateSequence);

            var xy = new double[2 * coordinateSequence.Count];
            double[] z = null;
            if (!double.IsNaN(coordinateSequence.GetOrdinate(0, GeoAPI.Geometries.Ordinate.Z)))
                z = new double[coordinateSequence.Count];

            var j = 0;
            for (var i = 0; i < coordinateSequence.Count; i++)
            {
                xy[j++] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.X);
                xy[j++] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z);
                if (z != null) z[i] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z);
            }

            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, coordinateSequence.Count);

            var ret = (ICoordinateSequence)coordinateSequence.Clone();
            j = 0;
            for (var i = 0; i < coordinateSequence.Count; i++)
            {
                ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.X, xy[j]);
                ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.X, xy[j]);
                if (z != null && DimTarget > 2)
                    ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.Z, z[j]);
                else
                    ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.Z, coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z));
            }

            return ret;
        }

        private DotSpatialAffineCoordinateSequence TransformDotSpatialAffine(DotSpatialAffineCoordinateSequence sequence)
        {
            var seq = (DotSpatialAffineCoordinateSequence)sequence.Copy();
            App.CongAnGis.Services.Business.ConverterProj.Reproject.ReprojectPoints(seq.XY, seq.Z, Source, Target, 0, seq.Count);
            return seq;
        }

        GeoAPI.CoordinateSystems.Transformations.DomainFlags IMathTransform.GetDomainFlags(List<double> points)
        {
            throw new NotImplementedException();
        }
    }

    //public class gTransform : MathTransform
    //{
    //    public ProjectionInfo Source;
    //    public ProjectionInfo Target;

    //    public override int DimSource { get; }

    //    public override int DimTarget { get; }

    //    public override string WKT { get; }

    //    public override string XML { get; }


    //    /// <summary>
    //    /// Creates an instance of this class
    //    /// </summary>
    //    /// <param name="source"></param>
    //    /// <param name="target"></param>
    //    public gTransform(ProjectionInfo source, ProjectionInfo target)
    //    {
    //        Source = source;
    //        Target = target;
    //    }


    //    public new double[] Transform(double[] point)
    //    {

    //        var xy = new[] { point[0], point[1] };
    //        var z = new double[1];

    //        if (DimSource > 2)
    //            z[0] = point[2];

    //        IDatumTransform idt = new DatumTransform(new IDatumTransformStage[] { 
    //            new DatumTransformStage("-191.90441429 ", "-39.30318279", new Spheroid(), new Spheroid(), -0.00928836, 0.01975479, 0.00427372) 
    //        });
    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);

    //        if (DimTarget > 2)
    //            return new[] { xy[0], xy[1], z[0] };

    //        return xy;
    //    }

    //    public new IList<double[]> TransformList(IList<double[]> points)
    //    {
    //        var xy = new double[2 * points.Count];
    //        var z = new double[points.Count];

    //        for (int i = 0; i < points.Count; i++)
    //        {
    //            xy[2 * i] = points[i][0];
    //            xy[2 * i + 1] = points[i][1];
    //            if (DimSource > 2)
    //                z[i] = points[i][2];
    //        }

    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, points.Count);

    //        var ret = new List<double[]>(points.Count);
    //        if (DimTarget > 2)
    //        {
    //            for (int i = 0; i < points.Count; i++)
    //                ret.Add(new[] { xy[2 * i], xy[2 * i + 1], z[i] });
    //        }
    //        else
    //        {
    //            for (int i = 0; i < points.Count; i++)
    //                ret.Add(new[] { xy[2 * i], xy[2 * i + 1] });
    //        }
    //        return ret;
    //    }

    //    public IList<GeoAPI.Geometries.Coordinate> TransformList(IList<GeoAPI.Geometries.Coordinate> points)
    //    {
    //        var xy = new double[2 * points.Count];
    //        var z = new double[points.Count];

    //        for (int i = 0; i < points.Count; i++)
    //        {
    //            xy[2 * i] = points[i].X;
    //            xy[2 * i + 1] = points[i].Y;
    //            if (DimSource > 2)
    //                z[i] = points[i].Z;
    //        }

    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, points.Count);

    //        var ret = new List<GeoAPI.Geometries.Coordinate>(points.Count);
    //        if (DimTarget > 2)
    //        {
    //            for (int i = 0; i < points.Count; i++)
    //                ret.Add(new GeoAPI.Geometries.Coordinate(xy[2 * i], xy[2 * i + 1], z[i]));
    //        }
    //        else
    //        {
    //            for (int i = 0; i < points.Count; i++)
    //                ret.Add(new GeoAPI.Geometries.Coordinate(xy[2 * i], xy[2 * i + 1]));
    //        }
    //        return ret;
    //    }


    //    [Obsolete]
    //    public ICoordinate Transform(ICoordinate coordinate)
    //    {
    //        var xy = new[] { coordinate.X, coordinate.Y };
    //        var z = new[] { coordinate.Z };

    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);

    //        var ret = (ICoordinate)coordinate.Clone();
    //        ret.X = xy[0];
    //        ret.Y = xy[1];
    //        ret.Z = z[0];

    //        return ret;
    //    }

    //    public GeoAPI.Geometries.Coordinate Transform(GeoAPI.Geometries.Coordinate coordinate)
    //    {
    //        var xy = new[] { coordinate.X, coordinate.Y };
    //        double[] z = new[] { coordinate.Z };

    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, 1);

    //        var ret = coordinate.Copy();
    //        ret.X = xy[0];
    //        ret.Y = xy[1];
    //        if (z != null)
    //            ret.Z = z[0];

    //        return ret;
    //    }

    //    public ICoordinateSequence Transform(ICoordinateSequence coordinateSequence)
    //    {
    //        ////use shortcut if possible
    //        //if (coordinateSequence is DotSpatialAffineCoordinateSequence)
    //        //    return TransformDotSpatialAffine((DotSpatialAffineCoordinateSequence)coordinateSequence);

    //        var xy = new double[2 * coordinateSequence.Count];
    //        double[] z = null;
    //        if (!double.IsNaN(coordinateSequence.GetOrdinate(0, GeoAPI.Geometries.Ordinate.Z)))
    //            z = new double[coordinateSequence.Count];

    //        var j = 0;
    //        for (var i = 0; i < coordinateSequence.Count; i++)
    //        {
    //            xy[j++] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.X);
    //            xy[j++] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z);
    //            if (z != null) z[i] = coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z);
    //        }

    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, z, Source, Target, 0, coordinateSequence.Count);

    //        var ret = (ICoordinateSequence)coordinateSequence.Clone();
    //        j = 0;
    //        for (var i = 0; i < coordinateSequence.Count; i++)
    //        {
    //            ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.X, xy[j]);
    //            ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.X, xy[j]);
    //            if (z != null && DimTarget > 2)
    //                ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.Z, z[j]);
    //            else
    //                ret.SetOrdinate(i, GeoAPI.Geometries.Ordinate.Z, coordinateSequence.GetOrdinate(i, GeoAPI.Geometries.Ordinate.Z));
    //        }

    //        return ret;
    //    }

    //    private void TransformDotSpatialAffine(DotSpatialAffineCoordinateSequence sequence)
    //    {
    //        var seq = (DotSpatialAffineCoordinateSequence)sequence.Copy();
    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(seq.XY, seq.Z, Source, Target, 0, seq.Count);
    //    }

    //    public override MathTransform Inverse()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void Transform(ref double x, ref double y, ref double z)
    //    {
    //        var xy = new[] { x, y };
    //        double[] zz = new[] { 0.0 };
    //        App.Gis.Services.Business.ConverterProj.Reproject.ReprojectPoints(xy, zz, Source, Target, 0, 1);
    //        x = xy[0];
    //        y = xy[1];
    //        z = zz[0];
    //    }

    //    public override void Invert()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
