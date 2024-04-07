using App.Core.Common;
using Microsoft.Extensions.Options;

namespace App.QTHTGis.Services.Manager;

public interface IAuthorizationManager
{
}

public class AuthorizationManager : IAuthorizationManager
{
    private readonly IOptionsSnapshot<AppSettingModel> _appSettings;
    private readonly ICurrentContext _currentContext;
    private ICustomHttpClient _httpClient;

    public AuthorizationManager(ICurrentContext currentContext, IOptionsSnapshot<AppSettingModel> appSettings,
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
}