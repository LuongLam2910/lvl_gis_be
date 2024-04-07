using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services;
using App.CongAnGis.Services.Manager;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.ConganGis.Services.Model.SysphieuChienThuatModel;
using Microsoft.AspNetCore.Http;
using static App.Core.Common.Constants;
using System.IO;
using System.Reflection;

namespace App.ConganGis.Services.Manager
{
    public interface ISysphieuChienThuatManager
    {
        Task<ApiResponse> InsertDataCustom([FromBody] phieuChienThuatModel model);
        Task<ApiResponse> UpdateDataCustom([FromBody] phieuChienThuatModel model);
        Task<ApiResponse> InsertInfoHoso([FromBody] SysPhieuchienthuatManagerModel model);
        Task<ApiResponse<PageModelView<IEnumerable<SysphieuchienthuatModel>>>> Paging([FromBody] SysphieuchienthuatPagingModel _model);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse<SysListPhieuchienthuatManagerModel>> GetByttphieu(int id);
        Task<ApiResponse> UploadInfoPhieu(IList<IFormFile> files, UploadImgPhieuchienThuatModel model);
    }

    public class SysphieuChienThuatManager : ISysphieuChienThuatManager
    {
        private readonly AppSettingModel _appSetting;
        private readonly ICurrentContext _currentContext;

        public SysphieuChienThuatManager(IOptionsSnapshot<AppSettingModel> appSetting, ICurrentContext currentContext)
        {
            _appSetting = appSetting.Value;
            _currentContext = currentContext;
        }

        public async Task<ApiResponse<SysListPhieuchienthuatManagerModel>> GetByttphieu(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var metaData = new LinqMetaData(adapter);
                        var data = metaData.Systtphieuchienthuat.First(c => c.Idphieuct == id);
                        if (data == null)
                        {
                            return ApiResponse<SysListPhieuchienthuatManagerModel>.Generate(GeneralCode.Error, "ERROR");
                        }
                        var listMucA = metaData.Sysphuongana.Where(c => c.Idttphieu == data.Id);
                        var listMucB = metaData.Sysphuonganb.Where(c => c.Idttphieu == data.Id);
                        var listMucC = metaData.Sysphuonganc.Where(c => c.Idttphieu == data.Id);
                        var listMucD = metaData.Sysphuongand.Where(c => c.Idttphieu == data.Id);

                        return new SysListPhieuchienthuatManagerModel
                        {
                            listInfo = new SysInfoHoSoModel
                            {
                                id = data.Id,
                                vitridl = data.Vitridialy,
                                tencoso = data.Tendonvi,
                                sohoso = data.Sohoso,
                                dienthoai1 = data.Dienthoai1,
                                diachi = data.Diachi,
                                coquancaptructiep = data.Cqqltructiep,
                                dienthoai2 = data.Dienthoai2,
                                ngaylap = data.Ngaylapphuongan,
                                ngayduyet = data.Ngayduyet,
                                nguoilap = data.Nguoilapphuongan,
                                nguoipheduyet = data.Nguoipheduyet,
                                imghosonha = data.Imgtoanha,
                                idphieuct = data.Idphieuct,
                            },
                            listMuca = listMucA.Select(c => new SysMucaModel
                            {
                                id = c.Id,
                                vitridl = c.Vitridialy,
                                ngosau = c.Ngosau,
                                giaothongpvcc = c.Giaothongpvcc,
                                nuocbentrong = c.Nuocbentrong,
                                nuocbenngoai = c.Nuocbenngoai,
                                dacdiem = c.Dacdiem,
                                luclungcc = c.Luclungcc,
                                lucluongtt = c.Luclungcc,
                                phuongtien = c.Phuongtien,
                                idttphieu = c.Idttphieu,
                            }).ToList(),
                            listMucb = listMucB.Select(c => new SysMucbModel
                            {
                                id = c.Id,
                                tinhhuongpt = c.Tinhhuongpt,
                                trienkhaicc = c.Trienkhaicc,
                                giaothongpvcc = c.Giaothongpvcc,
                                sdtrienkhai = c.Sdtrienkhai,
                                nhiemvu = c.Nhiemvu,
                                truluong = c.Truluong,
                                tinhhuong= c.Tinhhuong,
                                idttphieu = c.Idttphieu,
                            }).ToList(),
                            listMucc = listMucC.Select(c => new SysMuccModel
                            {
                                id = c.Id,
                                ndbosung = c.Ndbosung,
                                idttphieu = c.Idttphieu,
                            }).ToList(),
                            listMucd = listMucD.Select(c => new SysMucdModel
                            {
                                id = c.Id,
                                ndhoctap = c.Ndhoctap,
                                idttphieu = c.Idttphieu,
                            }).ToList()
                        };
                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<SysListPhieuchienthuatManagerModel>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
        public async Task<ApiResponse> InsertDataCustom(phieuChienThuatModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    var _sysphieuchienthuat = new SysphieuchienthuatEntity();
                    _sysphieuchienthuat.Tenphieu = model.tenphieu;
                    _sysphieuchienthuat.Donvi = model.donvi;
                    _sysphieuchienthuat.Createddate = model.createdate;
                    _sysphieuchienthuat.Status = model.status;
                    _sysphieuchienthuat.Iddonvicpcs = model.idDonvicbcs;
                    adapter.SaveEntity(_sysphieuchienthuat, true);
                    return GeneralCode.Success;
                }
                catch (ORMEntityValidationException ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> UpdateDataCustom(phieuChienThuatModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    SysphieuchienthuatEntity entity = new SysphieuchienthuatEntity(model.id);
                    adapter.FetchEntity(entity);
                    entity.Tenphieu = model.tenphieu;
                    entity.Donvi = model.donvi;
                    entity.Createddate = model.createdate;
                    entity.Status = model.status;
                    entity.Iddonvicpcs = model.idDonvicbcs;
                    adapter.SaveEntity(entity);
                    adapter.Commit();
                    return GeneralCode.Success;
                }
                catch (ORMEntityValidationException ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> InsertInfoHoso(SysPhieuchienthuatManagerModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    //ttphieuchienthuat
                    string rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                    string filePath = _appSetting.FileLocation.Path;
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        filePath = rootPath + FileBase.File + @"\SysFileManager";
                    }

                    if (!string.IsNullOrWhiteSpace(model.listInfo.imghosonha))
                    {
                        filePath += @"\" + "PhieuChienThuat";
                    }

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string subPath = "";
                    string subPath2 = "";
                    if (!string.IsNullOrWhiteSpace(model.listInfo.imghosonha))
                    {
                    string fullPath = filePath + @"\" + model.listInfo.imghosonha;
                    string prePath = rootPath + FileBase.File;
                       subPath = fullPath.Replace(prePath, "");
                    }
                    if (!string.IsNullOrWhiteSpace(model.listMucb.sdtrienkhai))
                    {
                        string fullPath = filePath + @"\" + model.listMucb.sdtrienkhai;
                        string prePath = rootPath + FileBase.File;
                        subPath2 = fullPath.Replace(prePath, "");
                    }

                    var _systtphieuchienthuat = new SysttphieuchienthuatEntity();
                    if (model.Type != "Create")
                    {
                        _systtphieuchienthuat = new SysttphieuchienthuatEntity(model.listInfo.id);
                        _systtphieuchienthuat.IsNew = false;
                    }
                    _systtphieuchienthuat.Vitridialy = model.listInfo.vitridl;
                    _systtphieuchienthuat.Tendonvi = model.listInfo.tencoso;
                    _systtphieuchienthuat.Sohoso = model.listInfo.sohoso;
                    _systtphieuchienthuat.Dienthoai1 = model.listInfo.dienthoai1;
                    _systtphieuchienthuat.Dienthoai2 = model.listInfo.dienthoai2;
                    _systtphieuchienthuat.Diachi = model.listInfo.diachi;
                    _systtphieuchienthuat.Cqqltructiep = model.listInfo.coquancaptructiep;
                    _systtphieuchienthuat.Ngaylapphuongan = model.listInfo.ngaylap;
                    _systtphieuchienthuat.Ngayduyet = model.listInfo.ngayduyet;
                    _systtphieuchienthuat.Nguoipheduyet = model.listInfo.nguoipheduyet;
                    _systtphieuchienthuat.Imgtoanha = subPath;
                    _systtphieuchienthuat.Idphieuct = model.id;
                    
                    adapter.SaveEntity(_systtphieuchienthuat, true);
                    var idttphieu = metadata.Systtphieuchienthuat.First(c => c.Idphieuct == model.id).Id;
                    //phuongana
                    var _sysphuongana = new SysphuonganaEntity();
                    if (model.Type != "Create")
                    {
                        _sysphuongana = new SysphuonganaEntity(model.listMuca.id);
                        _sysphuongana.IsNew = false;
                    }
                    _sysphuongana.Vitridialy = model.listMuca.vitridl;
                    _sysphuongana.Ngosau = model.listMuca.ngosau;
                    _sysphuongana.Giaothongpvcc = model.listMuca.giaothongpvcc;
                    _sysphuongana.Nuocbentrong = model.listMuca.nuocbentrong;
                    _sysphuongana.Nuocbenngoai = model.listMuca.nuocbenngoai;
                    _sysphuongana.Truluong = model.listMuca.truluong;
                    _sysphuongana.Kcnnuoc = model.listMuca.kcnnuoc;
                    _sysphuongana.Luuy = model.listMuca.luuy;
                    _sysphuongana.Dacdiem = model.listMuca.dacdiem;
                    _sysphuongana.Luclungcc = model.listMuca.luclungcc;
                    _sysphuongana.Lucluongtt = model.listMuca.lucluongtt;
                    _sysphuongana.Phuongtien = model.listMuca.phuongtien;
                    _sysphuongana.Idttphieu = idttphieu;
                    adapter.SaveEntity(_sysphuongana, true);
                    //phuonganb
                    var _sysphuonganb = new SysphuonganbEntity();
                    if (model.Type != "Create")
                    {
                        _sysphuonganb = new SysphuonganbEntity(model.listMucb.id);
                        _sysphuonganb.IsNew = false;
                    }
                    _sysphuonganb.Tinhhuongpt = model.listMucb.tinhhuongpt;
                    _sysphuonganb.Trienkhaicc = model.listMucb.trienkhaicc;
                    _sysphuonganb.Giaothongpvcc = model.listMucb.giaothongpvcc;
                    _sysphuonganb.Sdtrienkhai = subPath2;
                    _sysphuonganb.Nhiemvu = model.listMucb.nhiemvu;
                    _sysphuonganb.Truluong = model.listMucb.truluong;
                    _sysphuonganb.Tinhhuong = model.listMucb.tinhhuong;
                    _sysphuonganb.Tctrienkhaicc = model.listMucb.tctrienkhaicc;
                    _sysphuonganb.Sodotrienkhai = model.listMucb.sodotrienkhai;
                    _sysphuonganb.Nvchihuycc = model.listMucb.nvchihuycc;
                    _sysphuonganb.Idttphieu = idttphieu;
                    adapter.SaveEntity(_sysphuonganb, true);

                    //phuonganc
                    var _sysphuonganc = new SysphuongancEntity();
                    if (model.Type != "Create")
                    {
                        _sysphuonganc = new SysphuongancEntity(model.listMucc.id);
                        _sysphuonganc.IsNew = false;
                    }
                    _sysphuonganc.Createdate = model.listMucc.createdate;
                    _sysphuonganc.Ndbosung = model.listMucc.ndbosung;
                    _sysphuonganc.Nguoixaydung = model.listMucc.nguoixaydung;
                    _sysphuonganc.Nguoiduyet = model.listMucc.nguoiduyet;
                    _sysphuonganc.Idttphieu = idttphieu;
                    adapter.SaveEntity(_sysphuonganc, true);

                    //phuonganc
                    var _sysphuongand = new SysphuongandEntity();
                    if (model.Type != "Create")
                    {
                        _sysphuongand = new SysphuongandEntity(model.listMucd.id);
                        _sysphuongand.IsNew = false;
                    }
                    _sysphuongand.Createdate = model.listMucd.createdate;
                    _sysphuongand.Ndhoctap = model.listMucd.ndhoctap;
                    _sysphuongand.Thchay = model.listMucd.thchay;
                    _sysphuongand.Lucluongthgia = model.listMucd.lucluongthgia;
                    _sysphuongand.Danhgia = model.listMucd.danhgia;
                    _sysphuongand.Idttphieu = idttphieu;
                    adapter.SaveEntity(_sysphuongand, true);
                    return GeneralCode.Success;
                }
                catch (ORMEntityValidationException ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> UploadInfoPhieu(IList<IFormFile> files, UploadImgPhieuchienThuatModel model)
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var unitOfWork2 = new UnitOfWork2();
                    foreach (var item in files)
                    {
                        if (item.Length > 0)
                        {
                            string rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                            if (item.Length > (_appSetting.FileLocation.MaxSize * 1048576)) // Bytes (B)
                            {
                                return ApiResponse.Generate(GeneralCode.Error, "Dung dượng file quá lớn");
                            }
                            string filePath = _appSetting.FileLocation.Path;
                            if (string.IsNullOrWhiteSpace(filePath))
                            {
                                filePath = rootPath + FileBase.File + @"\SysFileManager";
                            }

                            if (!string.IsNullOrWhiteSpace(model.folder))
                            {
                                filePath += @"\" + model.folder;
                            }

                            if (!Directory.Exists(filePath))
                            {
                                Directory.CreateDirectory(filePath);
                            }

                            string fullPath = filePath + @"\" + item.FileName;
                            using (var fileStream = item.OpenReadStream())

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {

                                await item.CopyToAsync(stream);
                            }
                            string prePath = rootPath + FileBase.File;
                            string subPath = fullPath.Replace(prePath, "");

                            if (model.type == "Image")
                            {   
                                var imgSodonha = new SysttphieuchienthuatEntity(model.id);
                                    adapter.FetchEntity(imgSodonha);
                                    imgSodonha.Imgtoanha = subPath;
                                    unitOfWork2.AddForSave(imgSodonha, true);

                                var imgSodotrienkhai = new SysphuonganbEntity(model.idMucB);
                                    adapter.FetchEntity(imgSodotrienkhai);
                                    imgSodotrienkhai.Sodotrienkhai = subPath;
                                    unitOfWork2.AddForSave(imgSodotrienkhai, true);
                                await unitOfWork2.CommitAsync(adapter);
                                return GeneralCode.Success;
                            }
                        }
                    }
                    return ApiResponse.Generate(GeneralCode.Success, "Thành Công");
                }
                catch (System.Exception ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse<PageModelView<IEnumerable<SysphieuchienthuatModel>>>> Paging([FromBody] SysphieuchienthuatPagingModel _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysphieuchienthuatFields.Donvi.Like(_model.strKey))
                        //.Or(SysphieuchienthuatFields.Createddate.Equal(_model.strKey))
                        .Or(SysphieuchienthuatFields.Tenphieu.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new CongAnGis.Dal.HelperClasses.EntityCollection<SysphieuchienthuatEntity>();
                        int totalPagesCount = 0;
                        int totalRecord = (int)adapter.GetDbCount(data, null);
                        int recordsCount = (int)adapter.GetDbCount(data, filter, null);
                        if (recordsCount <= _model.pageSize)
                        {
                            totalPagesCount = 1;
                            _model.currentPage = totalPagesCount;
                        }
                        else
                        {
                            int remainder = 0;
                            totalPagesCount = Math.DivRem(recordsCount, _model.pageSize, out remainder);
                            if (remainder > 0)
                            {
                                totalPagesCount++;
                            }
                        }
                        SortExpression sort = new SortExpression(SysphieuchienthuatFields.Id | SortOperator.Descending);
                        try
                        {
                            var parameters = new QueryParameters(_model.currentPage, _model.pageSize, _model.pageSize, filter)
                            {
                                CollectionToFetch = data,
                                CacheResultset = true,
                                SorterToUse = sort,
                                CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                            };
                            adapter.FetchEntityCollection(parameters);
                            //adapter.FetchEntityCollection(data, filter, 0, sort, null, _model.currentPage, _model.pageSize);
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<PageModelView<IEnumerable<SysphieuchienthuatModel>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysphieuchienthuatModel>>()
                        {
                            Data = data.Select(c => new SysphieuchienthuatModel
                            {
                                id = c.Id,
                                tenphieu = c.Tenphieu,
                                donVi = c.Donvi,
                                status = c.Status,
                                createDate = c.Createddate,
                                tendonvi = c.Iddonvicpcs
                            }).ToList(),
                            CurrentPage = _model.currentPage,
                            PageSize = _model.pageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        };
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<SysphieuchienthuatModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    SysphieuchienthuatEntity _SysphieuchienthuatEntity = new SysphieuchienthuatEntity(id);
                    var Idttphieu = metadata.Systtphieuchienthuat.FirstOrDefault(c => c.Idphieuct == id).Id;
                    var Idtt = metadata.Systtphieuchienthuat.FirstOrDefault(c => c.Idphieuct == id);
                    if (Idtt != null)
                    {
                        int? Idttp = Idtt.Id;

                        if (Idttp != null)
                        {
                            SysttphieuchienthuatEntity _sysphuonganaEntity = new SysttphieuchienthuatEntity(Idttp.Value);
                            adapter.DeleteEntity(_sysphuonganaEntity);
                        }
                    }
                    var idMuca = metadata.Sysphuongana.FirstOrDefault(c => c.Idttphieu == Idttphieu);
                    if (idMuca != null)
                    {
                        int? idMa = idMuca.Id;

                        if (idMa != null)
                        {
                            SysphuonganaEntity _sysphuonganaEntity = new SysphuonganaEntity(idMa.Value);
                            adapter.DeleteEntity(_sysphuonganaEntity);
                        }
                    }
                    var idMucb = metadata.Sysphuonganb.FirstOrDefault(c => c.Idttphieu == Idttphieu);
                    if (idMucb != null)
                    {
                        int? idMb = idMucb.Id;

                        if (idMb != null)
                        {
                            SysphuonganbEntity _sysphuonganbEntity = new SysphuonganbEntity(idMb.Value);
                            adapter.DeleteEntity(_sysphuonganbEntity);
                        }
                    }
                    var idMucc = metadata.Sysphuonganc.FirstOrDefault(c => c.Idttphieu == Idttphieu);
                    if (idMucc != null)
                    {
                        int? idMc = idMucc.Id;

                        if (idMc != null)
                        {
                            SysphuongancEntity _sysphuongancEntity = new SysphuongancEntity(idMc.Value);
                            adapter.DeleteEntity(_sysphuongancEntity);
                        }
                    }
                    var idMucd = metadata.Sysphuongand.FirstOrDefault(c => c.Idttphieu == Idttphieu);
                    if (idMucd != null)
                    {
                        int? idMd = idMucd.Id;

                        if (idMd != null)
                        {
                            SysphuongandEntity _sysphuongandEntity = new SysphuongandEntity(idMd.Value);
                            adapter.DeleteEntity(_sysphuongandEntity);
                        }
                    }
                    adapter.DeleteEntity(_SysphieuchienthuatEntity);
                }
                return GeneralCode.Success;
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }
}
