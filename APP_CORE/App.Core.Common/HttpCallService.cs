using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace App.Core.Common;

public interface IHttpCallService
{
    Task<ICustomHttpResponse<TEntity>> CallGetServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class;

    Task<ICustomHttpResponse<TEntity>> CallPostServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class;

    Task<ICustomHttpResponse<TEntity>> CallDeleteServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class;

    Task<ICustomHttpResponse<TEntity>> CallPutServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class;
}

public class HttpCallService : IHttpCallService
{
    private readonly ICurrentContext _currentContext;
    private readonly ICustomHttpClient _httpClient;

    public HttpCallService(ICustomHttpClient httpCurrent, ICurrentContext currentContext)
    {
        _httpClient = httpCurrent;
        _currentContext = currentContext;
    }

    public async Task<ICustomHttpResponse<TEntity>> CallDeleteServiceAsync<TEntity>(string serviceProtocol, string port,
        string url, IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
        where TEntity : class
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(serviceProtocol).Append(":").Append(port).Append(url);
        if (headers == null) headers = new Dictionary<string, string>();
        headers.Add(HeaderNames.Authorization, _currentContext.Token);
        return await _httpClient.DeleteAsync<TEntity>(stringBuilder.ToString(), queries, headers);
    }

    public async Task<ICustomHttpResponse<TEntity>> CallGetServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(serviceProtocol).Append(":").Append(port).Append(url);
        if (headers == null) headers = new Dictionary<string, string>();
        headers.Add(HeaderNames.Authorization, _currentContext.Token);
        return await _httpClient.GetAsync<TEntity>(stringBuilder.ToString(), queries, headers);
    }

    public async Task<ICustomHttpResponse<TEntity>> CallPostServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(serviceProtocol).Append(":").Append(port).Append(url);
        if (headers == null) headers = new Dictionary<string, string>();
        headers.Add(HeaderNames.Authorization, _currentContext.Token);
        return await _httpClient.PostJsonAsync<TEntity>(stringBuilder.ToString(), data, queries, headers);
    }

    public async Task<ICustomHttpResponse<TEntity>> CallPutServiceAsync<TEntity>
    (string serviceProtocol, string port, string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
        where TEntity : class
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(serviceProtocol).Append(":").Append(port).Append(url);
        if (headers == null) headers = new Dictionary<string, string>();
        headers.Add(HeaderNames.Authorization, _currentContext.Token);
        return await _httpClient.PutRawJsonAsync<TEntity>(stringBuilder.ToString(), data, queries, headers);
    }
}