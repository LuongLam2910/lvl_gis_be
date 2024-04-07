using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;

namespace App.QTHTGis.Services.Manager;

public interface ISysMenuManager
{
    Task<ApiResponse<SysMenuModel.ItemAddNew>> GetItemAddNewEdit();
}

public class SysMenuManager : ISysMenuManager
{
    public async Task<ApiResponse<SysMenuModel.ItemAddNew>> GetItemAddNewEdit()
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);

                return new SysMenuModel.ItemAddNew
                {
                    ListApp = metaData.Sysapp.Select(c => new SysMenuModel.ItemAddNew.AppModel
                    {
                        Id = (int)c.Appid,
                        Name = c.Tenapp
                    }).ToList(),
                    ListMenu = metaData.Sysmenu.Select(c => new SysMenuModel.ItemAddNew.MenuModel
                    {
                        Id = c.Machucnang,
                        Name = c.Tenchucnang
                    }).ToList(),
                    ListUnit = metaData.Sysunit.Select(c => new SysMenuModel.ItemAddNew.UnitModel
                    {
                        Code = c.Unitcode,
                        Name = c.Tendonvi
                    }).ToList()
                };
                adapter.CloseConnection();
            }
        });
    }
}