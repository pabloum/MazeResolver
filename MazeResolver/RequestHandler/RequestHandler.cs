using System.Net.Http.Headers;
using System.Text;

namespace MazeResolver;

public class RequestHandler : IRequestHandler
{
    private const string CodeQueryString = "?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==";
    private HttpClient client;

    public RequestHandler(IConfiguration configuration, HttpClient httpClient)
    {
        var baseUrl = configuration.GetSection("MazeRunnerBaseUrl").Value;
        client = httpClient;
        client.BaseAddress = new Uri(baseUrl ?? "https://mazerunnerapi6.azurewebsites.net/api/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<T> Get<T>(string url)
    {
        var response = await client.GetAsync(url + CodeQueryString);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }

        return default(T) ?? throw new Exception("Http Request failed");
    }

    public async Task<T> Post<T>(string url, string payload)
    {
        var response = await client.PostAsync(url + CodeQueryString, CreateContent(payload));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }

        return default(T) ?? throw new Exception("Http Request failed");
    }

    private StringContent? CreateContent(string payload)
    {
        return new StringContent(payload, Encoding.UTF8, "application/json");
    }
}