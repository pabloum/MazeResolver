using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace MazeResolver;

public class RequestHandler : IRequestHandler
{
    private HttpClient client;

    public RequestHandler(IConfiguration configuration, HttpClient httpClient)
    {
        var baseUrl = configuration.GetSection("MazeRunnerBaseUrl").Value;
        client = httpClient;
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<T> Get<T>(string url)
    {
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }

        return default(T);
    }

    public async Task<T> Post<T>(string url, string payload)
    {
        var response = await client.PostAsync(url, CreateContent(payload));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }

        return default(T);
    }

    private StringContent? CreateContent(string payload)
    {
        return new StringContent(payload, Encoding.UTF8, "application/json");
    }
}

public class TokenStructure
{
    public string access_token { get; set; }
    public string expires_at { get; set; }
}
