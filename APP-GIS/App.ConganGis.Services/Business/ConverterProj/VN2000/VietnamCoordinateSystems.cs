
using GeoAPI.CoordinateSystems.Transformations;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace App.CongAnGis.Services.Business.ConverterProj.VN2000
{
    /// <summary>
    /// Chuyển đổi hệ quy chiếu VN2000 sang WGS84 và ngược lại
    /// </summary>
    public class VietnamCoordinateSystems
    {
        #region "Projection"
        public static ProjectionInfo VN2000
        {
            get { return ProjectionInfo.FromProj4String("+title=Vn2000 +proj=longlat +ellps=WGS84 +towgs84=-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278 +units=m +no_defs"); }
        }
        public static ProjectionInfo WGS84
        {
            get { return KnownCoordinateSystems.Geographic.World.WGS1984; }
        }
        public static ProjectionInfo Proj4756
        {
            get { return ProjectionInfo.FromProj4String("+title=Vn2000 +proj=longlat +ellps=WGS84 +towgs84=-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278 +units=m +no_defs"); }
        }
        public static ProjectionInfo VN103003
        {
            get { return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",103.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]"); }
        }
        public static ProjectionInfo VN103006
        {
            get { return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",103.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]"); }
        }
        public static ProjectionInfo VN104003
        {
            get { return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]"); }
        }
        public static ProjectionInfo VN104006
        {
            get { return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]"); }
        }

        public static ProjectionInfo VN104303
        {
            get { return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.5],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]"); }
        }
        public static ProjectionInfo VN104306
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.5],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }

        public static ProjectionInfo VN104453
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.75],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN104456
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",104.75],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN105003
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN105006
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }

        public static ProjectionInfo VN105303
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.50],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN105306
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.50],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN105453
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.75],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN105456
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.75],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106003
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106006
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106153
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.25],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106156
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.25],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106303
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.50],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN106306
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",106.50],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107003
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }

        public static ProjectionInfo VN107006
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107153
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.25],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107156
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.25],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107303
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.50],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107306
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.50],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107453
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.75],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN107456
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",107.75],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN108003
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]");
            }
        }
        public static ProjectionInfo VN108006
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.0],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN108153
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.25],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN108156
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.25],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN108303
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.50],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo VN108306
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",108.50],PARAMETER[\"Scale_Factor\",0.9996],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo Proj34051
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN_2000_UTM_Zone_48N\",GEOGCS[\"GCS_VN_2000\",DATUM[\"D_Vietnam_2000\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",105.0],PARAMETER[\"Scale_Factor\",0.9999],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3405]]");
            }
        }
        public static ProjectionInfo Proj3405
        {
            get
            {
                return ProjectionInfo.FromEsriString("PROJCS[\"VN-2000 / UTM zone 48N\",GEOGCS[\"VN-2000\",DATUM[\"D_\",SPHEROID[\"WGS_1984\",6378137,298.257223563]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",0],PARAMETER[\"central_meridian\",105],PARAMETER[\"scale_factor\",0.9996],PARAMETER[\"false_easting\",500000],PARAMETER[\"false_northing\",0],UNIT[\"Meter\",1]]");
            }
        }

        #endregion

        #region "WKT"
        /// <summary>
        /// VN2000 --> WGS84
        /// </summary>
        /// <param name="geoWkt">WKT</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>WKT</returns>
        public static string TransformWKTVN2000ToWGS84(string geoWkt, ProjectionInfo sourceSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(sourceSRS, wgs84);
            string result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(geoWkt, mTransform);
            IMathTransform gTransform = new GisTransform(VietnamCoordinateSystems.VN2000, wgs84);
            result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(result, gTransform);
            return result;
        }

        /// <summary>
        /// VN2000 --> WGS84
        /// </summary>
        /// <param name="geoWkt">Geometry</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>WKT</returns>
        public static string TransformWKTVN2000ToWGS84(Geometry geom, ProjectionInfo sourceSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(sourceSRS, wgs84);
            var result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.Transform(geom, mTransform);
            IMathTransform gTransform = new GisTransform(VietnamCoordinateSystems.VN2000, wgs84);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryToWKT(result, gTransform);
        }

        /// <summary>
        /// VN2000 --> WGS84
        /// </summary>
        /// <param name="geoWkt">WKT</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>Geometry</returns>
        public static Geometry TransformWKTVN2000ToWGS84Geometry(string geoWkt, ProjectionInfo sourceSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(sourceSRS, wgs84);
            string result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(geoWkt, mTransform);
            IMathTransform gTransform = new GisTransform(VietnamCoordinateSystems.VN2000, wgs84);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKT(result, gTransform);
        }

        /// <summary>
        /// VN2000 --> WGS84
        /// </summary>
        /// <param name="geoWkt">Geometry</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>Geometry</returns>
        public static Geometry TransformWKTVN2000ToWGS84Geometry(Geometry geom, ProjectionInfo sourceSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(sourceSRS, wgs84);
            var result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.Transform(geom, mTransform);
            IMathTransform gTransform = new GisTransform(VietnamCoordinateSystems.VN2000, wgs84);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.Transform(result, gTransform);
        }

        /// <summary>
        /// WGS84 --> VN2000
        /// </summary>
        /// <param name="geoWkt">WKT</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>WKT</returns>
        public static string TransformWKTWGS84ToVN2000(string geoWkt, ProjectionInfo destSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(wgs84, VietnamCoordinateSystems.VN2000);
            string result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(geoWkt, mTransform);
            IMathTransform gTransform = new GisTransform(wgs84, destSRS);
            result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(result, gTransform);
            return result;
        }

        /// <summary>
        /// WGS84 --> VN2000
        /// </summary>
        /// <param name="geoWkt">Geometry</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>WKT</returns>
        public static string TransformWKTWGS84ToVN2000(Geometry geom, ProjectionInfo destSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(wgs84, VietnamCoordinateSystems.VN2000);
            var result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.Transform(geom, mTransform);
            IMathTransform gTransform = new GisTransform(wgs84, destSRS);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryToWKT(result, gTransform);
        }

        /// <summary>
        /// WGS84 --> VN2000
        /// </summary>
        /// <param name="geoWkt">WKT</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>Geometry</returns>
        public static Geometry TransformWKTWGS84ToVN2000Geometry(string geoWkt, ProjectionInfo destSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(wgs84, VietnamCoordinateSystems.VN2000);
            string result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKTToWKT(geoWkt, mTransform);
            IMathTransform gTransform = new GisTransform(wgs84, destSRS);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryFromWKT(result, gTransform);
        }

        /// <summary>
        /// WGS84 --> VN2000
        /// </summary>
        /// <param name="geoWkt">Geometry</param>
        /// <param name="sourceSRS">Sử dụng VietnamCoordinateSystems.{Hệ quy chiếu}</param>
        /// <returns>WKT</returns>
        public static string TransformWKTWGS84ToVN2000Geometry(Geometry geom, ProjectionInfo destSRS)
        {
            ProjectionInfo wgs84 = KnownCoordinateSystems.Geographic.World.WGS1984;
            IMathTransform mTransform = new GisTransform(wgs84, VietnamCoordinateSystems.VN2000);
            var result = App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.Transform(geom, mTransform);
            IMathTransform gTransform = new GisTransform(wgs84, destSRS);
            return App.CongAnGis.Services.Business.ConverterProj.VN2000.Reproject.TransformGeometryToWKT(result, gTransform);
        }
        #endregion
    }
}
