using System.Collections.Generic;

namespace App.Qtht.Services.Models;

public class SysMenuModel
{
    public class ItemAddNew
    {
        public IEnumerable<MenuModel> ListMenu { get; set; }
        public IEnumerable<UnitModel> ListUnit { get; set; }
        public IEnumerable<AppModel> ListApp { get; set; }

        public class UnitModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class AppModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class MenuModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}