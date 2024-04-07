using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using App.QTHTGis.Dal.DatabaseSpecific;

namespace App.QTHTGis.Services.Manager;

public interface ISysGroupManager
{
    Task<ApiResponse<SysGroupModel.GetDataAppViewModel>> SelectGroupApp();
    Task<ApiResponse<List<SysGroupModel.GroupInfoViewModel>>> SelectGroupByAppId(int appId, int? level);

    Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByGroupIdAppId(int groupId);

    Task<ApiResponse> UpdateGroupMenuFunction(List<SysgroupmenuEntity> lstEntity);

    Task<ApiResponse> DeleteSysGroup(int groupId);
}

public class SysGroupManager : ISysGroupManager
{
    private static readonly string Appid = "Appid";
    private static readonly string Groupid = "Groupid";

    public async Task<ApiResponse<SysGroupModel.GetDataAppViewModel>> SelectGroupApp()
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                return new SysGroupModel.GetDataAppViewModel
                {
                    ListApp = metaData.Sysapp.Select(c => new SysGroupModel.GetDataAppViewModel.GroupAppViewModel
                    {
                        AppId = (int)c.Appid,
                        AppName = c.Tenapp
                    }).ToList()
                };
            }
        });
    }
    public async Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByGroupIdAppId(int groupId)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);

                    var app = metaData.Sysgroup.First(c => c.Groupid == groupId);
                    EntityCollection<SysmenuEntity> collectMenu = new EntityCollection<SysmenuEntity>();
                    PrefetchPath2 path = new PrefetchPath2(EntityType.SysmenuEntity);

                    PredicateExpression predicateSysGroup = new PredicateExpression(SysgroupmenuFields.Groupid == groupId);
                    path.Add(SysmenuEntity.PrefetchPathSysgroupmenus, 0, predicateSysGroup);

                    adapter.FetchEntityCollection(collectMenu, new RelationPredicateBucket(SysmenuFields.Appid == app.Appid + ""), path);

                    return collectMenu.OrderBy(c => c.Sothutu).Select(c => new SysGroupModel.SysGroupSelectByAppidModel
                    {
                        Menuid = c.Menuid,
                        Machucnang = c.Machucnang,
                        Macha = c.Macha,
                        Tenchucnang = c.Tenchucnang,
                        Function = c.Sysgroupmenus.FirstOrDefault() == null ? new string[] { "" } : c.Sysgroupmenus.First().Function.Split(','),
                        IsNew = c.Sysgroupmenus.FirstOrDefault() == null ? true : false
                    }).ToList();

                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>.Generate(GeneralCode.Error, ex.Message);
        }
    }
    //public async Task<ApiResponse<SysGroupModel.SysGroupSelectByAppidModel>> SelectByGroupIdAppId(int groupId)
    //{
    //    try
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
    //            {
    //                var metaData = new LinqMetaData(adapter);

    //                var app = metaData.Sysgroup.First(c => c.Groupid == groupId);
    //                var collectMenu = new EntityCollection<SysmenuEntity>();
    //                var path = new PrefetchPath2(EntityType.SysmenuEntity);

    //                var predicateSysGroup = new PredicateExpression(SysgroupmenuFields.Groupid == groupId);
    //                path.Add(SysmenuEntity.PrefetchPathSysgroupmenus, 0, predicateSysGroup);

    //                adapter.FetchEntityCollection(collectMenu,
    //                    new RelationPredicateBucket(SysmenuFields.Appid == app.Appid + ""), path);
    //                var appCurrent = new SysappEntity((long)app.Appid);
    //                adapter.FetchEntity(appCurrent);
    //                return new SysGroupModel.SysGroupSelectByAppidModel
    //                {
    //                    DinhDanhApp = appCurrent.Dinhdanhapp,
    //                    lstDetails = collectMenu.OrderBy(c => c.Sothutu).Select(c =>
    //                        new SysGroupModel.SysGroupSelectByAppidModel.SysGroupSelectByAppIdModelDetail
    //                        {
    //                            Menuid = c.Menuid,
    //                            Machucnang = c.Machucnang,
    //                            Macha = c.Macha,
    //                            Tenchucnang = c.Tenchucnang,
    //                            Function = c.Sysgroupmenus.FirstOrDefault() == null
    //                                ? new[] { "" }
    //                                : c.Sysgroupmenus.First().Function.Split(','),
    //                            IsNew = c.Sysgroupmenus.FirstOrDefault() == null ? true : false
    //                        }).ToList()
    //                };
    //            }
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return ApiResponse<SysGroupModel.SysGroupSelectByAppidModel>.Generate(GeneralCode.Error, ex.Message);
    //    }
    //}

    public async Task<ApiResponse> UpdateGroupMenuFunction(List<SysgroupmenuEntity> lstEntity)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var unitOfWork2 = new UnitOfWork2();
                var lstDelete = lstEntity.Where(c => c.Function == "").ToList();
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysgroupmenuEntity),
                    new RelationPredicateBucket(
                        (Predicate)SysgroupmenuFields.Groupid.In(lstDelete.Select(c => c.Groupid))
                        & (Predicate)SysgroupmenuFields.Menuid.In(lstDelete.Select(c => c.Menuid))));
                unitOfWork2.AddCollectionForSave(new EntityCollection<SysgroupmenuEntity>(lstEntity
                    .Where(c => c.Function != "").Select(c => new SysgroupmenuEntity
                    {
                        Groupid = c.Groupid,
                        Menuid = c.Menuid,
                        Function = c.Function,
                        IsNew = c.IsNew,
                        IsDirty = true
                    })));
                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.DeletesPerformedDirectly, UnitOfWorkBlockType.Inserts,
                    UnitOfWorkBlockType.Updates
                };
                await unitOfWork2.CommitAsync(adapter);
                return GeneralCode.Success;
            }
            catch (ORMException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> DeleteSysGroup(int groupId)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysgroupmenuEntity),
                new RelationPredicateBucket(SysgroupmenuFields.Groupid == groupId));
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysgroupEntity),
                new RelationPredicateBucket(SysgroupFields.Groupid == groupId));
            await unitOfWork2.CommitAsync(adapter, true);
            return GeneralCode.Success;
        }
    }

    public async Task<ApiResponse<List<SysGroupModel.GroupInfoViewModel>>> SelectGroupByAppId(int appId, int? level)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var metaData = new LinqMetaData(adapter);
            var group = await metaData.Sysgroup.Where(c => c.Appid == appId && c.Status == 1).Select(c =>
                new SysGroupModel.GroupInfoViewModel
                {
                    Id = (int)c.Groupid,
                    Mota = c.Description,
                    Ten = c.Groupname,
                }).ToListAsync();
            if (level != null)
                group = await metaData.Sysgroup.Where(c => c.Appid == appId && c.Status == 1).Select(
                    c => new SysGroupModel.GroupInfoViewModel
                    {
                        Id = (int)c.Groupid,
                        Mota = c.Description,
                        Ten = c.Groupname,
                    }).ToListAsync();
            return group;
        }
    }
}