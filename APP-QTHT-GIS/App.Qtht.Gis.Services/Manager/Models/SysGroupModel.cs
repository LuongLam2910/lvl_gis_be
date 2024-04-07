using System.Collections.Generic;

namespace App.Qtht.Services.Models;

public class SysGroupModel
{
    public class GroupInfoViewModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public long Level { get; set; }
    }

    public class GetDataAppViewModel
    {
        public List<GroupAppViewModel> ListApp { get; set; }

        public class GroupAppViewModel
        {
            public int AppId { get; set; }
            public string AppName { get; set; }
        }
    }
    public class SysGroupSelectByAppidModel
    {
        public decimal Menuid { get; set; }
        public string Machucnang { get; set; }
        public string Macha { get; set; }
        public string Tenchucnang { get; set; }
        public string Appid { get; set; }
        public string[] Function { get; set; }
        public bool IsNew { get; set; }
    }
    public class SysGroupSelectByAppidCusTomModel
    {
        public string DinhDanhApp { get; set; }
        public List<SysGroupSelectByAppIdModelDetail> lstDetails { get; set; }

        public class SysGroupSelectByAppIdModelDetail
        {
            public decimal Menuid { get; set; }
            public string Machucnang { get; set; }
            public string Macha { get; set; }
            public string Tenchucnang { get; set; }
            public string Appid { get; set; }
            public string[] Function { get; set; }
            public bool IsNew { get; set; }
        }
    }
}