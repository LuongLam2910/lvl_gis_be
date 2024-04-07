using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.Qtht.Services.Manager.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace App.QTHTGis.Services.Manager;

public interface ICasManager
{
    Task<ApiResponse<List<string>>> GetAppByAuthCode(string authCode);
    Task<ApiResponse<List<string>>> GetUserInfo(string idToken);

    Task<ApiResponse<bool>> RevokeToken(RevokeTokenModel model);
    Task<ApiResponse<bool>> isExpriedAccessToken(string access_token);
}

public class CasManager : ICasManager
{
    private readonly AppSettingModel _appSettings;
    private readonly ICustomHttpClient _customHttpClient;

    public CasManager(ICustomHttpClient customHttpClient, IOptionsSnapshot<AppSettingModel> appSettings)
    {
        _customHttpClient = customHttpClient;
        _appSettings = appSettings.Value;
    }

    public async Task<ApiResponse<List<string>>> GetAppByAuthCode(string authCode)
    {
        if (string.IsNullOrEmpty(authCode))
            return ApiResponse<List<string>>.Generate(GeneralCode.Error, "Không có authCode!");

        #region getBearerToken

        var headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");

        var data = new Dictionary<string, string>();
        data.Add("grant_type", "authorization_code");
        data.Add("code", authCode);
        data.Add("client_id", _appSettings.CASBG.clientId);
        data.Add("client_secret", _appSettings.CASBG.clientSecret);
        data.Add("redirect_uri", _appSettings.CASBG.redirectUri);

        var result = await _customHttpClient.PostFormUrlEncodedAsync<TokenResponse>(
            _appSettings.CASBG.providerUrl + _appSettings.CASBG.methodToken, data, null, headers);
        if (result.HttpStatusCode == HttpStatusCode.OK)
        {
            if (result != null && !string.IsNullOrEmpty(result.Response.id_token))
            {
                var token = result.Response.id_token;
                //decode access token
                var _t = GetTokenInfo(token);
                var u = string.Empty;
                _t.TryGetValue("sub", out u);
                if (!string.IsNullOrEmpty(u))
                {
                    var url = _appSettings.CASBG.providerUrl +
                              "services/RemoteUserStoreManagerService.RemoteUserStoreManagerServiceHttpsSoap12Endpoint/";
                    headers = new Dictionary<string, string>();
                    headers.Add("Accept-Encoding", "gzip,deflate");
                    headers.Add("Content-Type", "application/soap+xml;charset=UTF-8;action=\"urn:getRoleListOfUser\"");
                    headers.Add("Accept", "application/json");
                    headers.Add("Authorization",
                        "Basic " + Convert.ToBase64String(
                            Encoding.UTF8.GetBytes(_appSettings.CASBG.AdminName + ":" + _appSettings.CASBG.AdminPass)));

                    var body =
                        "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:ser=\"http://service.ws.um.carbon.wso2.org\"><soap:Header/><soap:Body><ser:getRoleListOfUser><ser:userName>" +
                        u + "</ser:userName></ser:getRoleListOfUser></soap:Body></soap:Envelope>";

                    var _resultApp = await _customHttpClient.PostRawStringSOAPAsync<string>(url, body, null, headers);
                    if (_resultApp.HttpStatusCode == HttpStatusCode.OK)
                    {
                        var _content = JObject.Parse(_resultApp.Response);
                        var dataReturn = _content["getRoleListOfUserResponse"]["return"];
                        var lst = new List<string>();
                        //Trigger access token first item
                        lst.Add(result.Response.access_token);
                        lst.Add(result.Response.id_token);

                        foreach (var item in dataReturn) lst.Add(item.ToString());
                        return lst;
                    }
                }
            }
        }
        else
        {
            return ApiResponse<List<string>>.Generate(GeneralCode.Error, "AuthCode hết hạn rồi");
        }

        #endregion

        return new List<string>();
    }

    public async Task<ApiResponse<List<string>>> GetUserInfo(string idToken)
    {
        if (string.IsNullOrEmpty(idToken))
            return ApiResponse<List<string>>.Generate(GeneralCode.Error, "Không có idToken!");

        var _t = GetTokenInfo(idToken);
        var u = string.Empty;
        _t.TryGetValue("sub", out u);
        if (!string.IsNullOrEmpty(u))
        {
            var url = _appSettings.CASBG.providerUrl +
                      "services/RemoteUserStoreManagerService.RemoteUserStoreManagerServiceHttpsSoap12Endpoint/";
            var headers = new Dictionary<string, string>();
            headers.Add("Accept-Encoding", "gzip,deflate");
            headers.Add("Content-Type", "application/soap+xml;charset=UTF-8;action=\"urn:getUserClaimValues\"");
            headers.Add("Accept", "application/json");
            headers.Add("Authorization",
                "Basic " + Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(_appSettings.CASBG.AdminName + ":" + _appSettings.CASBG.AdminPass)));

            var body =
                "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:ser=\"http://service.ws.um.carbon.wso2.org\"><soap:Header/><soap:Body><ser:getUserClaimValues><ser:userName>" +
                u +
                "</ser:userName><ser:profileName>default</ser:profileName></ser:getUserClaimValues></soap:Body></soap:Envelope>";

            var _resultApp = await _customHttpClient.PostRawStringSOAPAsync<string>(url, body, null, headers);
            if (_resultApp.HttpStatusCode == HttpStatusCode.OK)
            {
                var _content = JObject.Parse(_resultApp.Response);
                //var dataReturn = _content["getUserClaimValuesResponse"]["return"];
                var lst = new List<string>();

                //email
                try
                {
                    var email = _content
                        .SelectToken(
                            "$.getUserClaimValuesResponse.return[?(@.claimUri == 'http://wso2.org/claims/emailaddress')]['value']")
                        .ToString();
                    lst.Add(email);
                }
                catch
                {
                    lst.Add("nobody@xxx.com");
                }

                var lastName = string.Empty;

                try
                {
                    lastName = _content
                        .SelectToken(
                            "$.getUserClaimValuesResponse.return[?(@.claimUri == 'http://wso2.org/claims/lastname')]['value']")
                        .ToString();
                }
                catch (Exception)
                {
                    lastName = "Văn A";
                }

                var givenName = string.Empty;
                try
                {
                    givenName = _content
                        .SelectToken(
                            "$.getUserClaimValuesResponse.return[?(@.claimUri == 'http://wso2.org/claims/givenname')]['value']")
                        .ToString();
                }
                catch
                {
                    givenName = "Nguyễn";
                }

                lst.Add(givenName + " " + lastName);
                return lst;
            }
        }

        return new List<string>();
    }

    public async Task<ApiResponse<bool>> isExpriedAccessToken(string access_token)
    {
        if (string.IsNullOrEmpty(access_token))
            return ApiResponse<bool>.Generate(GeneralCode.Error, "Không có access token!");

        var headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");
        headers.Add("Authorization",
            "Basic " + Convert.ToBase64String(
                Encoding.UTF8.GetBytes(_appSettings.CASBG.AdminName + ":" + _appSettings.CASBG.AdminPass)));

        var data = new Dictionary<string, string>();
        data.Add("token", access_token);

        var result = await _customHttpClient.PostFormUrlEncodedAsync<ValidAccessTokenResponse>(
            _appSettings.CASBG.providerUrl + _appSettings.CASBG.validAccessTokenMethod, data, null, headers);

        if (result.HttpStatusCode == HttpStatusCode.OK)
            if (result.Response.active)
            {
                var exp = result.Response.exp;
                var currentTimestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                return currentTimestamp < exp;
            }

        return false;
    }

    public async Task<ApiResponse<bool>> RevokeToken(RevokeTokenModel model)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");

        var data = new Dictionary<string, string>();
        data.Add("token", model.token);
        data.Add("token_type_hint", model.tokenTypeHint);
        data.Add("client_id", model.client_id);
        data.Add("client_secret", model.client_secret);

        var result = await _customHttpClient.PostFormUrlEncodedAsync<ApiResponse>(
            _appSettings.CASBG.providerUrl + _appSettings.CASBG.methodRevokeToken, data, null, headers);

        if (result.HttpStatusCode == HttpStatusCode.OK) return true;
        return false;
    }


    protected Dictionary<string, string> GetTokenInfo(string token)
    {
        var TokenInfo = new Dictionary<string, string>();

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var claims = jwtSecurityToken.Claims.ToList();

        foreach (var claim in claims) TokenInfo.Add(claim.Type, claim.Value);

        return TokenInfo;
    }
}