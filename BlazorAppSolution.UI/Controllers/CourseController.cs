using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace BlazorAppSolution.UI.Controllers;

[Route("courses")]
[ApiController]
public class CourseController : Controller
{
    private readonly HttpClient _httpClient;

    public CourseController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        string apiUrl = "https://actualbackendapp.azurewebsites.net/api/Courses";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var jsonData = JsonSerializer.Deserialize<object>(data); 
                return new JsonResult(jsonData);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error fetching data from external API.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
