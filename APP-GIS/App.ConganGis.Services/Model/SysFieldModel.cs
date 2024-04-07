using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Model
{
    public class SysFieldModel
    {
        public class FieldModel
        {
            public decimal? Id { get; set; }
            public string Name { get; set; }
            public string Fieldname { get; set; }
            public short? Status { get; set; }
            public decimal? FeatureclassId { get; set; }
            public string Datatype { get; set; }
            public string Datalength { get; set; }
            public int? Show { get; set; }
            public string Config { get; set; }
        }
    }
}
