using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

//[ApiController]
//[Route("api/[controller]")]
//public class NewsController : ControllerBase
//{
//    private readonly HttpClient _httpClient;

//    public NewsController(IHttpClientFactory httpClientFactory)
//    {
//        _httpClient = httpClientFactory.CreateClient();
//    }

//    [HttpGet]
//    [Route("latest")]
//    public async Task<IActionResult> GetLatestNews([FromQuery] string query = "technology", [FromQuery] string language = "en")
//    {
//        // Replace with your News API endpoint and API key
//        string apiKey = "f9be800d366e452f9616d299deb230f4";
//        string url = $"https://newsapi.org/v2/everything?q={query}&language={language}&apiKey={apiKey}";

//        try
//        {
//            var response = await _httpClient.GetFromJsonAsync<object>(url);
//            return Ok(response);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { message = "Error fetching news", error = ex.Message });
//        }
//    }
//}

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public NewsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet]
    [Route("latest")]
    public async Task<IActionResult> GetLatestNews([FromQuery] string query = "technology", [FromQuery] string language = "en")
    {
        string apiKey = "f9be800d366e452f9616d299deb230f4";
        string url = $"https://newsapi.org/v2/everything?q={query}&language={language}&apiKey={apiKey}";

        // Add User-Agent header to the HttpClient request
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0"); // Replace "YourAppName/1.0" with your app's name and version

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, new { message = "Error fetching news", error = errorContent });
            }

            var data = await response.Content.ReadFromJsonAsync<object>();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error fetching news", error = ex.Message });
        }
    }
}
