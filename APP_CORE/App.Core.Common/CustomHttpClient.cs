using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Core.Common;

public interface ICustomHttpResponse<T>
{
    HttpStatusCode HttpStatusCode { get; set; }
    string Message { get; set; }
    T Response { get; set; }

    void LogObject(int userId, string sync, string v1, string ipClient, string userName, string v2);
}

public class CustomHttpResponse<T> : ICustomHttpResponse<T>
{
    public CustomHttpResponse(HttpResponseMessage response)
    {
        HttpStatusCode = response.StatusCode;
        Message = response.RequestMessage.ToString();
        if (HttpStatusCode == HttpStatusCode.OK) Response = GetRequestResponse(response);
    }

    public HttpStatusCode HttpStatusCode { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }

    public void LogObject(int userId, string sync, string v1, string ipClient, string userName, string v2)
    {
    }

    private T GetRequestResponse(HttpResponseMessage response)
    {
        try
        {
            var task = response.Content.ReadAsStringAsync();
            task.Wait(TimeSpan.FromMinutes(2));
            var resultContent = task.Result;
            if (string.IsNullOrEmpty(resultContent)) return default;

            if (typeof(T) == typeof(string)) return (T)Convert.ChangeType(resultContent, typeof(T));

            return JsonConvert.DeserializeObject<T>(resultContent);
        }
        catch (Exception ex)
        {
            return default;
        }
    }
}

public interface ICustomHttpClient
{
    Task<ICustomHttpResponse<T>> PostJsonAsync<T>(string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PostFormUrlEncodedAsync<T>(string url, IDictionary<string, string> data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PatchJsonAsync<T>(string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PostRawStringAsync<T>(string url, string data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PostRawStringSOAPAsync<T>(string url, string data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PostRawJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PutRawJsonAsync<T>(string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> DeleteAsync<T>(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> GetAsync<T>(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null, int timeout = 100);

    Task<byte[]> GetByteArrayAsync(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    HttpClient CreateHttpClient();

    HttpClient CreateHttpClient(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);

    Task<ICustomHttpResponse<T>> PutJsonAsync<T>(string url, object data, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null);
}

public class CustomHttpClient : ICustomHttpClient
{
    public async Task<ICustomHttpResponse<T>> PostRawStringAsync<T>(string url, string data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PostAsync(string.Empty,
                new StringContent(data,
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")));
        }
    }

    public async Task<ICustomHttpResponse<T>> PostRawJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PostAsync(string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")));
        }
    }

    public async Task<ICustomHttpResponse<T>> PatchJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PatchAsync(string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json")));
        }
    }


    public async Task<ICustomHttpResponse<T>> PostJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PostAsync(string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json")));
        }
    }

    public async Task<ICustomHttpResponse<T>> PostRawStringSOAPAsync<T>(string url, string data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PostAsync(string.Empty,
                new StringContent(data,
                    Encoding.UTF8,
                    "application/soap+xml")));
        }
    }

    public async Task<ICustomHttpResponse<T>> PostFormUrlEncodedAsync<T>(string url, IDictionary<string, string> data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PostAsync(string.Empty, new FormUrlEncodedContent(data)));
        }
    }


    public async Task<ICustomHttpResponse<T>> PutRawJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PutAsync(string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json")));
        }
    }

    public async Task<ICustomHttpResponse<T>> DeleteAsync<T>(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.DeleteAsync(string.Empty));
        }
    }

    public async Task<ICustomHttpResponse<T>> GetAsync<T>(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null, int timeout = 100)
    {
        using (var netclient = BuildHttpClient(url, queries, headers, timeout))
        {
            return new CustomHttpResponse<T>(await netclient.GetAsync(string.Empty));
        }
    }

    public async Task<byte[]> GetByteArrayAsync(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return await netclient.GetByteArrayAsync(string.Empty);
        }
    }

    public HttpClient CreateHttpClient()
    {
        return new HttpClient();
    }

    public HttpClient CreateHttpClient(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null)
    {
        return BuildHttpClient(url, queries, headers);
    }

    public async Task<ICustomHttpResponse<T>> PutJsonAsync<T>(string url, object data,
        IDictionary<string, string> queries = null, IDictionary<string, string> headers = null)
    {
        using (var netclient = BuildHttpClient(url, queries, headers))
        {
            return new CustomHttpResponse<T>(await netclient.PutAsync(string.Empty,
                new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json")));
        }
    }

    private HttpClient BuildHttpClient(string url, IDictionary<string, string> queries = null,
        IDictionary<string, string> headers = null, int timeout = 100)
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        {
            return true;
        };

        var netclient = new HttpClient(clientHandler);


        if (string.IsNullOrEmpty(url))
            return netclient;

        if (headers != null)
            foreach (var header in headers)
                netclient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

        if (queries != null)
        {
            var queriesHelpers = new List<string>();
            foreach (var query in queries)
                queriesHelpers.Add(
                    $"{WebUtility.UrlEncode(query.Key)}={WebUtility.UrlEncode(query.Value)}");
            if (queriesHelpers.Count > 0) url += "?" + string.Join("&", queriesHelpers);
        }

        //FIXME: Cuonghv 20220304 - add timeout
        netclient.Timeout = TimeSpan.FromSeconds(timeout);

        netclient.BaseAddress = new Uri(url);
        return netclient;
    }
}