using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.QTHTGis.Services.Manager;

public interface ISysAppManager
{
    Task<ApiResponse<EntityCollection<SysappEntity>>> SelectbyStatus();
    Task<ApiResponse<List<SysAppModel.AppAndCheckUserInAppModel>>> SelectCheckExistWithUserId(int userId);
}

public class SysAppManager : ISysAppManager
{
    public async Task<ApiResponse<List<SysAppModel.AppAndCheckUserInAppModel>>> SelectCheckExistWithUserId(int userId)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var metaData = new LinqMetaData(adapter);
            var result = await metaData.Sysapp.Select(c => new SysAppModel.AppAndCheckUserInAppModel
            {
                AppId = (int)c.Appid,
                DinhDanhApp = c.Dinhdanhapp,
                MoTa = c.Mota,
                TenApp = c.Tenapp,
                ExistRoleInUser = false
            }).ToListAsync();

            var lstMenu = (from c in metaData.Sysgroupmenu
                join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                join t in metaData.Sysusergroup on c.Groupid equals t.Groupid
                where p.State != null && t.Userid == userId
                                      && c.Function != null
                select p.Appid).ToList();
            //get menu, function from menuuser
            var lstMenu_user = (from c in metaData.Sysusermenu
                join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                where c.Userid == userId && p.State != null
                                         && c.Function != null
                select p.Appid).ToList();
            lstMenu.AddRange(lstMenu_user);
            lstMenu = lstMenu.Distinct().ToList();
            result.All(item =>
            {
                if (lstMenu.Contains(item.AppId.ToString())) item.ExistRoleInUser = true;
                return true;
            });
            return result;
        }
    }

    public async Task<ApiResponse<EntityCollection<SysappEntity>>> SelectbyStatus()
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var sysapp = new EntityCollection<SysappEntity>();
                adapter.FetchEntityCollection(sysapp, new RelationPredicateBucket(SysappFields.Trangthai == 1));
                return ApiResponse<EntityCollection<SysappEntity>>.Generate(sysapp);
            }
        });
    }
}