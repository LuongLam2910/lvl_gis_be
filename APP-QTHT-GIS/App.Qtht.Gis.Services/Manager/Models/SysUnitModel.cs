using System.Collections.Generic;

namespace App.Qtht.Services.Models;

public class SysUnitModel
{
    public class UnitSelectModel
    {
        public string Unitcode { get; set; }

        public string Unitname { get; set; }
        public short? Loaiunit { get; set; }
        public string Macha { get; set; }
    }

    public class UnitMasterInsert
    {
        public string Matusinh { get; set; }
        public IList<UnitItemInsert> LstDmdonvi { get; set; }

        public class UnitItemInsert
        {
            public string Madonvicha { get; set; }
            public string Madonvi { get; set; }
            public string Tendonvi { get; set; }
            public short? Loaiunit { get; set; }
        }
    }

    public class SysUnitPagingModel
    {
        public SysUnitPagingModel()
        {
            LstChildren = new List<SysUnitPagingModel>();

        }
        public string UnitCode { get; set; }
        public string ParentCode { get; set; }
        public string UnitName { get; set; }
        public long? Status { get; set; }
        public string Level { get; set; }
        public bool Expanded { get; set; }
        public IList<SysUnitPagingModel> LstChildren { get; set; }
    }
}