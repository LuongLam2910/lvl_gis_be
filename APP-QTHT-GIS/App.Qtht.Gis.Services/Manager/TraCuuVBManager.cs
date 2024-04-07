using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Common;
using App.Qtht.Services.Manager.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace App.QTHTGis.Services.Manager;

public interface ITraCuuVBManager
{
    Task<ApiResponse<SearchResult>> TimKiemVanBan(SearchVBModel model);
    Task<ApiResponse<List<LinhVucInfo>>> GetAllLinhVuc(List<int> lstDonvi);
    Task<ApiResponse<List<CoQuanBienTapInfo>>> GetAllCoQuanBienTap();
    Task<ApiResponse<List<CoQuanBanHanhInfo>>> GetAllCoQuanPhatHanh();
    Task<ApiResponse<List<LoaiVanBanInfo>>> GetAllLoaiVanBan(List<int> lstDonvi);
    Task<ApiResponse<List<ChucDanhInfo>>> GetAllChucDanh();
    Task<ApiResponse<List<NguoiKyInfo>>> GetAllNguoiKy();
    Task<ApiResponse> GetById();
    Task<ApiResponse<List<FileAttach>>> TaiFileDinhKem(List<string> lstAttachUrl);
}

public class TraCuuVBManager : ITraCuuVBManager
{
    private readonly AppSettingModel _appSettings;
    private readonly ICustomHttpClient _customHttpClient;

    public TraCuuVBManager(ICustomHttpClient customHttpClient, IOptionsSnapshot<AppSettingModel> appSettings)
    {
        _customHttpClient = customHttpClient;
        _appSettings = appSettings.Value;
    }

    public async Task<ApiResponse<List<ChucDanhInfo>>> GetAllChucDanh()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        var result =
            await _customHttpClient.GetAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllChucDanh, null, headers);

        return JsonConvert.DeserializeObject<List<ChucDanhInfo>>(result.Response.data);
    }

    /// <summary>
    ///     Danh sach Don vi
    /// </summary>
    /// <returns>Task&lt;ApiResponse&lt;List&lt;CoQuanBienTapInfo&gt;&gt;&gt;.</returns>
    public async Task<ApiResponse<List<CoQuanBienTapInfo>>> GetAllCoQuanBienTap()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        var data = new BodyRequest();
        data.DonVi = new List<int>().ToArray();
        var tg = new T_gridRequest();
        var s = new Sort();
        s.field = "ID";
        s.dir = "desc";
        tg.sort = new[] { s };
        tg.page = 1;
        tg.pageSize = 50;

        data.t_gridRequest = JsonConvert.SerializeObject(tg);

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllCoQuanBienTap, data, null,
                headers);


        return JsonConvert.DeserializeObject<List<CoQuanBienTapInfo>>(result.Response.data);
    }

    public async Task<ApiResponse<List<CoQuanBanHanhInfo>>> GetAllCoQuanPhatHanh()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        var data = new BodyRequest();
        data.DonVi = new List<int>().ToArray();
        var tg = new T_gridRequest();
        var s = new Sort();
        s.field = "ID";
        s.dir = "desc";
        tg.sort = new[] { s };
        tg.page = 1;
        tg.pageSize = 50;

        data.t_gridRequest = JsonConvert.SerializeObject(tg);

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllCoQuanPhatHanh, data, null,
                headers);


        return JsonConvert.DeserializeObject<List<CoQuanBanHanhInfo>>(result.Response.data);
    }

    public async Task<ApiResponse<List<LinhVucInfo>>> GetAllLinhVuc(List<int> lstDonvi)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        var data = new BodyRequest();
        data.DonVi = lstDonvi.ToArray();
        var tg = new T_gridRequest();
        var s = new Sort();
        s.field = "ID";
        s.dir = "desc";
        tg.sort = new[] { s };
        tg.page = 1;
        tg.pageSize = 50;

        data.t_gridRequest = JsonConvert.SerializeObject(tg);

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllLinhVuc, data, null,
                headers);


        return JsonConvert.DeserializeObject<List<LinhVucInfo>>(result.Response.data);
    }

    public async Task<ApiResponse<List<LoaiVanBanInfo>>> GetAllLoaiVanBan(List<int> lstDonvi)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        var data = new BodyRequest();
        data.DonVi = lstDonvi.ToArray();
        var tg = new T_gridRequest();
        var s = new Sort();
        s.field = "ID";
        s.dir = "desc";
        tg.sort = new[] { s };
        tg.page = 1;
        tg.pageSize = 50;

        data.t_gridRequest = JsonConvert.SerializeObject(tg);

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllLoaiVanBan, data, null,
                headers);


        return JsonConvert.DeserializeObject<List<LoaiVanBanInfo>>(result.Response.data);
    }

    public async Task<ApiResponse<List<NguoiKyInfo>>> GetAllNguoiKy()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        var result =
            await _customHttpClient.GetAsync<LGSPResponeInfo>(_appSettings.VBQPPL.GetAllNguoiKy, null, headers);

        return JsonConvert.DeserializeObject<List<NguoiKyInfo>>(result.Response.data);
    }

    public Task<ApiResponse> GetById()
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<FileAttach>>> TaiFileDinhKem(List<string> lstAttachUrl)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        var attachInfo = new DownloadAttachInfo();
        attachInfo.lstAttachUrl = lstAttachUrl.ToArray();

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.TaiFileDinhKem, attachInfo, null,
                headers);

        return JsonConvert.DeserializeObject<List<FileAttach>>(result.Response.data);
    }

    public async Task<ApiResponse<SearchResult>> TimKiemVanBan(SearchVBModel model)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + (await GetTokenLgsp()).access_token);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");

        if (model.t_gridRequest == null)
        {
            var tg = new T_gridRequest();
            var s = new Sort();
            s.field = "ID";
            s.dir = "desc";
            tg.sort = new[] { s };
            tg.page = 1;
            tg.pageSize = 50;

            model.t_gridRequest = JsonConvert.SerializeObject(tg);
        }

        var result =
            await _customHttpClient.PostJsonAsync<LGSPResponeInfo>(_appSettings.VBQPPL.TimKiem, model, null, headers);

        return JsonConvert.DeserializeObject<SearchResult>(result.Response.data);
    }

    private async Task<TokenResponse> GetTokenLgsp()
    {
        var headers = new Dictionary<string, string>();
        var queries = new Dictionary<string, string>();
        headers.Add("Authorization", "Basic " + _appSettings.GetTokenLgspBacGiang.Key);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/x-www-form-urlencoded");
        queries.Add("grant_type", "client_credentials");
        var rawString = "grant_type=client_credentials";
        var result =
            await _customHttpClient.PostRawStringAsync<TokenResponse>(_appSettings.GetTokenLgspBacGiang.Url, rawString,
                null, headers);
        return result.Response;
    }
}