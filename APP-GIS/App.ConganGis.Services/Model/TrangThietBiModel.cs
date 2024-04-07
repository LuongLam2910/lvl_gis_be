using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ConganGis.Services.Model
{
    public class TrangThietBiModel
    {
        public class PageSystrangthietbi
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
            public int idDoiTuong { get; set; }
            public string tableName { get; set; }
        }
        public class TTBJoinModel
        {
            public int _Id { get; set; }
            public int _iddoituong { get; set; }
            public int? _idthietbi { get; set; }
            public int? _soluong { get; set; }
            public string _ngaytao { get; set; }
            public string _tablename { get; set; }
            public string _tenThietBi { get; set; }
            public string _moTa { get; set; }

        }
    }
}
