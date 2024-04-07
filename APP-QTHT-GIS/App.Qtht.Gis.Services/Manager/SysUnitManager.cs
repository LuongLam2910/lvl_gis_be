using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using static App.Qtht.Services.Models.SysUnitModel;
using App.QTHTGis.Dal.DatabaseSpecific;

namespace App.QTHTGis.Services.Manager;

public interface ISysUnitManager
{
    Task<ApiResponse<EntityCollection<SysunitEntity>>> GetListTenDonVi(List<string> unitcodes);
    Task<ApiResponse> Delete(string unitcode);
    Task<ApiResponse<IEnumerable<SysUnitModel.UnitSelectModel>>> GetSysUnitList(IPredicate[] predicates = null);
    Task<ApiResponse<SysUnitModel.UnitSelectModel>> GetUnitName(string unitcode);
    Task<ApiResponse<SysUnitModel.UnitMasterInsert>> SelectMasterInsert();
    Task<ApiResponse<SysunitEntity>> SelectOne(string unitcode);
    Task<ApiResponse> DeleteByApp(string unitcode, string appName);

    Task<ApiResponse<List<CongAnList>>> GetAllCongAn();
    Task<ApiResponse<PageModelView<IEnumerable<SysUnitPagingModel>>>> Paging(PageModel pageModel, SortClause sortBy = null, params EntityField2[] _params);
}

public class SysUnitManager : ISysUnitManager
{

    public async Task<ApiResponse<PageModelView<IEnumerable<SysUnitPagingModel>>>> Paging(PageModel pageModel, SortClause sortBy = null, params EntityField2[] _params)
    {
        try
        {
            return await Task.Run(() =>
            {
                Predicate predicate = null;

                if (!string.IsNullOrEmpty(pageModel.Search) && _params.Length > 0)
                {
                    foreach (var item in _params)
                    {
                        predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();
                    }
                }

                if (!string.IsNullOrEmpty(pageModel.Condition))
                {
                    //predicate = (predicate) & fieldCodition == pageModel.Condition;
                }

                RelationPredicateBucket filter = new RelationPredicateBucket(predicate);

                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var data = new EntityCollection<SysunitEntity>();
                    int totalPagesCount = 0;
                    int totalRecord = (int)adapter.GetDbCount(data, null);

                    SortExpression sort = null;
                    if (sortBy != null)
                    {
                        sort = new SortExpression(sortBy);
                    }

                    //adapter.FetchEntityCollection(data, filter, 0, sort, pageModel.CurrentPage, pageModel.PageSize);
                    adapter.FetchEntityCollection(data, filter, 0, sort);

                    var result = new List<SysUnitPagingModel>();

                    result = data.Where(c1 => c1.Macha == "").Select(c1 => new SysUnitPagingModel
                    {
                        UnitCode = c1.Unitcode,
                        UnitName = c1.Tendonvi,
                        Status = c1.Trangthai,
                        ParentCode = c1.Macha,
                        Level = c1.Capdonvi,
                        LstChildren = data.Where(c2 => c2.Macha == c1.Unitcode).Select(c2 => new SysUnitPagingModel
                        {
                            UnitCode = c2.Unitcode,
                            UnitName = c2.Tendonvi,
                            Status = c2.Trangthai,
                            ParentCode = c2.Macha,
                            Level = c2.Capdonvi,
                            LstChildren = data.Where(c3 =>  c3.Macha == c2.Unitcode).Select(c3 => new SysUnitPagingModel
                            {
                                UnitCode = c3.Unitcode,
                                UnitName = c3.Tendonvi,
                                Status = c3.Trangthai,
                                ParentCode = c3.Macha,
                                Level = c3.Capdonvi,
                                LstChildren = data.Where(c4 =>  c4.Macha == c3.Unitcode).Select(c4 => new SysUnitPagingModel
                                {
                                    UnitCode = c4.Unitcode,
                                    UnitName = c4.Tendonvi,
                                    Status = c4.Trangthai,
                                    ParentCode = c4.Macha,
                                    Level = c4.Capdonvi,
                                    LstChildren = new List<SysUnitPagingModel>()
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList();

                    result = result.Skip((pageModel.CurrentPage - 1) * pageModel.PageSize).Take(pageModel.PageSize).ToList();

                    int recordsCount = result.Count;

                    if (recordsCount <= pageModel.PageSize)
                    {
                        totalPagesCount = 1;
                        pageModel.CurrentPage = totalPagesCount;
                    }
                    else
                    {
                        int remainder = 0;
                        totalPagesCount = Math.DivRem(recordsCount, pageModel.PageSize, out remainder);
                        if (remainder > 0)
                        {
                            totalPagesCount++;
                        }
                    }

                    return new PageModelView<IEnumerable<SysUnitPagingModel>>()
                    {
                        Data = result,
                        CurrentPage = pageModel.CurrentPage,
                        PageSize = pageModel.PageSize,
                        TotalPage = totalPagesCount,
                        TotalRecord = totalRecord,
                        RecordsCount = recordsCount
                    };
                }
            });
        }
        catch (ORMEntityValidationException ex)
        {
            return ApiResponse<PageModelView<IEnumerable<SysUnitPagingModel>>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }
    public async Task<ApiResponse<EntityCollection<SysunitEntity>>> GetListTenDonVi(List<string> unitcodes)
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var sysunits = new EntityCollection<SysunitEntity>();
                adapter.FetchEntityCollection(sysunits,
                    new RelationPredicateBucket(SysunitFields.Unitcode.In(unitcodes)));
                return ApiResponse<EntityCollection<SysunitEntity>>.Generate(sysunits);
            }
        });
    }

    public async Task<ApiResponse> Delete(string unitcode)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                await adapter.DeleteEntitiesDirectlyAsync(nameof(SysunitEntity),
                    new RelationPredicateBucket(SysunitFields.Unitcode == unitcode));
                return GeneralCode.Success;
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> DeleteByApp(string unitcode, string appName)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var filter = new RelationPredicateBucket((SysunitFields.Unitcode == unitcode) &
                                                         (SysunitFields.Dinhdanhapp == appName));
                //EntityCollection<SysunitEntity> sysunitEntities = new EntityCollection<SysunitEntity>();
                //adapter.FetchEntityCollection(sysunitEntities, filter);
                await adapter.DeleteEntitiesDirectlyAsync(nameof(SysunitEntity), filter);
                return GeneralCode.Success;
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<SysUnitModel.UnitSelectModel>>> GetSysUnitList(
        IPredicate[] predicates = null)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<SysunitEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.AddWithAnd(SysunitFields.Trangthai == 1);

                    if (predicates != null)
                        foreach (var item in predicates)
                            filter.PredicateExpression.AddWithAnd(item);

                    adapter.FetchEntityCollection(collect, filter);

                    return collect.Select(c => new SysUnitModel.UnitSelectModel
                    {
                        Unitcode = c.Unitcode,
                        Unitname = c.Tendonvi,
                        Loaiunit = c.Loaiunit,
                        Macha = c.Macha
                    }).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<SysUnitModel.UnitSelectModel>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<SysUnitModel.UnitSelectModel>> GetUnitName(string unitcode)
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var sysunits = new EntityCollection<SysunitEntity>();
                adapter.FetchEntityCollection(sysunits,
                    new RelationPredicateBucket(SysunitFields.Unitcode == unitcode));
                var model = new SysUnitModel.UnitSelectModel();
                if (sysunits.Count > 0)
                {
                    model.Unitcode = sysunits[0].Unitcode;
                    model.Unitname = sysunits[0].Tendonvi;
                }

                return ApiResponse<SysUnitModel.UnitSelectModel>.Generate(model);
            }
        });
    }

    public async Task<ApiResponse<SysUnitModel.UnitMasterInsert>> SelectMasterInsert()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);
                    var DataDonvi = metaData.Sysunit.ToList();
                    var Ma = metaData.Sysunit.Where(c => c.Macha == null).Count() + 1;
                    while (DataDonvi.Any(c => c.Unitcode == Ma.ToString())) Ma++;
                    var Convert = Ma.ToString();
                    if (Ma < 10) Convert = "0" + Convert;
                    while (DataDonvi.Any(c => c.Unitcode == Convert)) Convert = "0" + Ma++;
                    return new SysUnitModel.UnitMasterInsert
                    {
                        Matusinh = Convert,
                        LstDmdonvi = DataDonvi.Select(c => new SysUnitModel.UnitMasterInsert.UnitItemInsert
                        {
                            Madonvi = c.Unitcode,
                            Tendonvi = c.Tendonvi,
                            Madonvicha = c.Macha,
                            Loaiunit = c.Loaiunit
                        }).ToList()
                    };
                }
            });
        }
        catch (ORMException ex)
        {
            return ApiResponse<SysUnitModel.UnitMasterInsert>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<SysunitEntity>> SelectOne(string unitcode)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = new EntityCollection<SysunitEntity>();
                    adapter.FetchEntityCollection(entity,
                        new RelationPredicateBucket(SysunitFields.Unitcode == unitcode));
                    return entity[0];
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<SysunitEntity>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<List<CongAnList>>> GetAllCongAn()
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var listCongAn = metaData.Sysunit.Where(w =>
                    new[] { 2, 3, 5 }.Contains(w.Unitcode.Length) && w.Tendonvi.Contains("Công an")).ToList();
                return listCongAn.Select(s => new CongAnList
                {
                    Unitcode = s.Unitcode,
                    Ten = s.Tendonvi,
                    MaDonViCongAnCha = s.Macha,
                    TenDonViCongAnCha = listCongAn.FirstOrDefault(f => f.Unitcode == s.Macha)?.Tendonvi
                }).ToList();
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse<List<CongAnList>>.Generate(GeneralCode.Error, ex.Message);
        }
    }
}