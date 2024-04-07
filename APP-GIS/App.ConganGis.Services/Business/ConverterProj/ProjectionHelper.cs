using App.CongAnGis.Services.Business.ConverterProj.VN2000;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Business.ConverterProj
{
    public static class ProjecttionHelper
    {
        public static ProjectionInfo ConvertCoordinateSystemsFromSrid(string srid = "4326")
        {
            switch (srid)
            {
                case "103003":
                    return VietnamCoordinateSystems.VN103003;
                case "103006":
                    return VietnamCoordinateSystems.VN103006;
                case "104003":
                    return VietnamCoordinateSystems.VN104003;
                case "104006":
                    return VietnamCoordinateSystems.VN104006;
                case "104303":
                    return VietnamCoordinateSystems.VN104303;
                case "104306":
                    return VietnamCoordinateSystems.VN104306;
                case "104453":
                    return VietnamCoordinateSystems.VN104453;
                case "104456":
                    return VietnamCoordinateSystems.VN104456;
                case "105003":
                    return VietnamCoordinateSystems.VN105003;
                case "105006":
                    return VietnamCoordinateSystems.VN105006;
                case "105303":
                    return VietnamCoordinateSystems.VN105303;
                case "105306":
                    return VietnamCoordinateSystems.VN105306;
                case "105453":
                    return VietnamCoordinateSystems.VN105453;
                case "105456":
                    return VietnamCoordinateSystems.VN105456;
                case "106003":
                    return VietnamCoordinateSystems.VN106003;
                case "106006":
                    return VietnamCoordinateSystems.VN106006;
                case "106153":
                    return VietnamCoordinateSystems.VN106153;
                case "106156":
                    return VietnamCoordinateSystems.VN106156;
                case "106303":
                    return VietnamCoordinateSystems.VN106303;
                case "106306":
                    return VietnamCoordinateSystems.VN106306;
                case "107003":
                    return VietnamCoordinateSystems.VN107003;
                case "107006":
                    return VietnamCoordinateSystems.VN107006;
                case "107153":
                    return VietnamCoordinateSystems.VN107153;
                case "107156":
                    return VietnamCoordinateSystems.VN107156;
                case "107303":
                    return VietnamCoordinateSystems.VN107303;
                case "107306":
                    return VietnamCoordinateSystems.VN107306;
                case "107453":
                    return VietnamCoordinateSystems.VN107453;
                case "107456":
                    return VietnamCoordinateSystems.VN107456;
                case "108003":
                    return VietnamCoordinateSystems.VN108003;
                case "108006":
                    return VietnamCoordinateSystems.VN108006;
                case "108153":
                    return VietnamCoordinateSystems.VN108153;
                case "108156":
                    return VietnamCoordinateSystems.VN108156;
                case "108303":
                    return VietnamCoordinateSystems.VN108303;
                case "108306":
                    return VietnamCoordinateSystems.VN108306;
            }
            return VietnamCoordinateSystems.WGS84;
        }
    }
}
