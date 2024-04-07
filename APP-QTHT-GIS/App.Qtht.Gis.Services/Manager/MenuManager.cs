using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.FactoryClasses;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.QuerySpec.Adapter;
using App.QTHTGis.Dal.DatabaseSpecific;

namespace App.QTHTGis.Services.Manager;

public interface IMenuManager
{
    Task<ApiResponse<List<MenuModel.ListMenuModel>>> GetMenuByUserLogin();
    //Task<MenuModel.FuseNavigation> GetMenuByUserLogin_Fuse();
}

public class MenuManager : IMenuManager
{
    private readonly IOptionsSnapshot<AppSettingModel> _appSettings;
    private readonly ICurrentContext _currentContext;
    private ICustomHttpClient _httpClient;

    public MenuManager(ICurrentContext currentContext, IOptionsSnapshot<AppSettingModel> appSettings,
        ICustomHttpClient httpClient)
    {
        _currentContext = currentContext;
        _appSettings = appSettings;
        _httpClient = httpClient;
    }

    //public async Task<ApiResponse<List<MenuModel.ListMenuModel>>> GetMenuByUserLogin()
    //{
    //    var endPoint = $"{_appSettings.Value.Authorize.Authority}/App/Auth/api/Menu/GetMenuByUserLogin";
    //    Dictionary<string, string> headers = new Dictionary<string, string>();
    //    headers.Add(HeaderNames.Authorization, _currentContext.Token);
    //    var result = await _httpClient.GetAsync<ApiResponse<List<MenuModel.ListMenuModel>>>(endPoint, null, headers)
    //        );
    //    return result.Response;
    //}

    public async Task<ApiResponse<List<MenuModel.ListMenuModel>>> GetMenuByUserLogin()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var qf = new QueryFactory();
                    var result = qf.GetMenuByUserFunc("temptable", _currentContext.UserName, _currentContext.AppId);
                    //var resultSet = new DataTable();
                    //adapter.FetchAsDataTable(result, resultSet);
                    var metaData = new LinqMetaData(adapter);
                    DataTable resultSet = RetrievalProcedures.GetMenuByUserFunc(_currentContext.UserName, _currentContext.AppId, adapter);
                    var lstResult = new List<MenuModel.ListMenuModel>();
                    foreach (DataRow row in resultSet.Rows)
                        lstResult.Add(new MenuModel.ListMenuModel
                        {
                            Value = row.Field<string>("MACHUCNANG"),
                            Text = row.Field<string>("TENCHUCNANG"),
                            Parent = row.Field<string>("MACHA"),
                            Id = row.Field<long>("MENUID").ToString(),
                            CssClass = row.Field<string>("CSSCLASS"),
                            IconClass = row.Field<string>("ICONCLASS"),
                            State = row.Field<string>("STATE")
                        });
                    return lstResult;
                }
            });
            return null;
        }
        catch (Exception ex)
        {
            return ApiResponse<List<MenuModel.ListMenuModel>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    //public async Task<MenuModel.FuseNavigation> GetMenuByUserLogin_Fuse()
    //{
    //    var toReturn = new MenuModel.FuseNavigation();
    //    var compact = new List<MenuModel.FuseNavigationItem>();
    //    var defaultFuse = new List<MenuModel.FuseNavigationItem>();
    //    var futuristic = new List<MenuModel.FuseNavigationItem>();
    //    var horizontal = new List<MenuModel.FuseNavigationItem>();
    //    try
    //    {
    //        return await Task.Run(() =>
    //        {
    //            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
    //            {
    //                var qf = new QueryFactory();
    //                var result = qf.GetMenuByUserFunc("temptable", _currentContext.UserName, _currentContext.AppId);
    //                var resultSet = new DataTable();
    //                adapter.FetchAsDataTable(result, resultSet);
    //                var metaData = new LinqMetaData(adapter);
    //                foreach (DataRow row in resultSet.Rows)
    //                {
    //                    var _FuseNavigationItem = new MenuModel.FuseNavigationItem();
    //                    _FuseNavigationItem.type = "basic";
    //                    if (string.IsNullOrEmpty(row[SysmenuFields.Macha.Name].ToString()) ||
    //                        row[SysmenuFields.Macha.Name].ToString() == "0") _FuseNavigationItem.type = "group";
    //                    _FuseNavigationItem.Id = row[SysmenuFields.Menuid.Name].ToString();
    //                    _FuseNavigationItem.title = row[SysmenuFields.Tenchucnang.Name].ToString();
    //                    _FuseNavigationItem.icon = row[SysmenuFields.Iconclass.Name].ToString();
    //                    compact.Add(_FuseNavigationItem);
    //                    defaultFuse.Add(_FuseNavigationItem);
    //                    futuristic.Add(_FuseNavigationItem);
    //                    horizontal.Add(_FuseNavigationItem);
    //                }

    //                toReturn.compact = defaultFuse;
    //                toReturn.defaultFuse = defaultFuse;
    //                toReturn.futuristic = defaultFuse;
    //                toReturn.horizontal = defaultFuse;
    //                return toReturn;
    //            }
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}
}