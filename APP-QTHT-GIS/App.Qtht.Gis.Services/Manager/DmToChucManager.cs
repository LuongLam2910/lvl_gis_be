using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.Qtht.Services.Models;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.QTHTGis.Services.Manager;

public interface IDmToChucManager
{
    Task<ApiResponse<IEnumerable<DmToChucModel>>> GetToChucListByStatus(bool? activeStatus);
    Task<ApiResponse<IEnumerable<DmChucVuDetailModel>>> GetLstChucVu();
    Task<ApiResponse> InsertCustom(DmToChucInsertUpdateModel model);
    Task<ApiResponse<DmToChucInsertUpdateModel>> SelecOneCustom(int key);
    Task<ApiResponse> UpdateCustom(DmToChucInsertUpdateModel model);
    Task<ApiResponse> DeleteCustom(int key);
}

public class DmToChucManager : IDmToChucManager
{
    public async Task<ApiResponse<IEnumerable<DmToChucModel>>> GetToChucListByStatus(bool? activeStatus)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<DmtochucEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.Add(DmtochucFields.Trangthai == (activeStatus == true ? 1 : 0));

                    adapter.FetchEntityCollection(collect, filter);

                    var data = collect.Select(c => new DmToChucModel
                    {
                        Id = c.Id,
                        Ten = c.Ten,
                        TrangThai = c.Trangthai
                    }).ToList();

                    return ApiResponse<IEnumerable<DmToChucModel>>.Generate(data, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<DmToChucModel>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<DmChucVuDetailModel>>> GetLstChucVu()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<DmchucvuEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.Add(DmchucvuFields.Trangthai == 1);

                    adapter.FetchEntityCollection(collect, filter);

                    var data = collect.Select(c => new DmChucVuDetailModel
                    {
                        Id = c.Id,
                        Ten = c.Ten,
                        Trangthai = c.Trangthai
                    }).ToList();

                    return ApiResponse<IEnumerable<DmChucVuDetailModel>>.Generate(data, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<DmChucVuDetailModel>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> InsertCustom(DmToChucInsertUpdateModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var collect = new EntityCollection<DmtochucEntity>();
                adapter.FetchEntityCollection(collect,
                    new RelationPredicateBucket(DmtochucFields.Ten == model.DmToChuc.Ten));
                if (collect.Count > 0) return GeneralCode.Duplicate;
                var dmtochuc = new DmtochucEntity();
                dmtochuc.Ten = model.DmToChuc.Ten;
                dmtochuc.Trangthai = (short)model.DmToChuc.TrangThai;
                await adapter.SaveEntityAsync(dmtochuc, true);
                var listToChucChucVu = model.ListToChucChucVu.Select(c => new TochucChucvuEntity
                {
                    IdTochuc = dmtochuc.Id,
                    IdChucvu = c.IdChucVu,
                    Cap = c.Cap
                });
                var ListData = new EntityCollection<TochucChucvuEntity>(listToChucChucVu);
                await adapter.SaveEntityCollectionAsync(ListData);
                return GeneralCode.Success;
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<DmToChucInsertUpdateModel>> SelecOneCustom(int key)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);
                    var tochuc = metaData.Dmtochuc.First(c => c.Id == key);
                    var tochucChucvu = metaData.TochucChucvu.Where(c => c.IdTochuc == key);
                    return new DmToChucInsertUpdateModel
                    {
                        DmToChuc = new DmToChucModel
                        {
                            Id = tochuc.Id,
                            Ten = tochuc.Ten,
                            TrangThai = tochuc.Trangthai
                        },
                        ListToChucChucVu = tochucChucvu.Select(c => new DmToChucChucVuModel
                        {
                            IdToChuc = c.IdTochuc,
                            IdChucVu = c.IdChucvu,
                            Cap = (short)c.Cap
                        }).ToList()
                    };
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<DmToChucInsertUpdateModel>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> UpdateCustom(DmToChucInsertUpdateModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var unitOfWork2 = new UnitOfWork2();

                var metaData = new LinqMetaData(adapter);
                var tc = metaData.Dmtochuc.FirstOrDefault(c => c.Id == model.DmToChuc.Id);
                if (tc == null) return ApiResponse.Generate(GeneralCode.Error, "Tổ chức không tồn tại!");

                var tochuc = new DmtochucEntity();
                tochuc.Id = model.DmToChuc.Id;
                tochuc.Ten = model.DmToChuc.Ten;
                tochuc.Trangthai = (short)model.DmToChuc.TrangThai;
                tochuc.IsNew = false;
                unitOfWork2.AddForSave(tochuc, true);

                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(TochucChucvuEntity),
                    new RelationPredicateBucket(TochucChucvuFields.IdTochuc == model.DmToChuc.Id));
                var listToChucChucVu = model.ListToChucChucVu.Select(c => new TochucChucvuEntity
                {
                    IdTochuc = model.DmToChuc.Id,
                    IdChucvu = c.IdChucVu,
                    Cap = c.Cap
                });
                var ListData = new EntityCollection<TochucChucvuEntity>(listToChucChucVu);
                unitOfWork2.AddCollectionForSave(ListData);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.Updates,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                await unitOfWork2.CommitAsync(adapter);
                return GeneralCode.Success;
            }

            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> DeleteCustom(int key)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var unitOfWork2 = new UnitOfWork2();
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(TochucChucvuEntity),
                    new RelationPredicateBucket(TochucChucvuFields.IdTochuc == key));
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(DmtochucEntity),
                    new RelationPredicateBucket(DmtochucFields.Id == key));
                await unitOfWork2.CommitAsync(adapter, true);
            }

            return GeneralCode.Success;
        }
        catch (Exception ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }
}