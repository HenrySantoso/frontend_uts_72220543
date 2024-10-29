using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : Controller
{
    private readonly HttpClient _httpClient;

    public CategoryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        string apiUrl = "https://actualbackendapp.azurewebsites.net/api/v1/Categories";

        try
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var jsonData = JsonSerializer.Deserialize<Category[]>(data);
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

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category newCategory)
    {
        string apiUrl = "https://actualbackendapp.azurewebsites.net/api/v1/Categories";

        try
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, newCategory);

            if (response.IsSuccessStatusCode)
            {
                var createdCategory = await response.Content.ReadFromJsonAsync<Category>();
                return Created(apiUrl, createdCategory);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error creating new category.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updatedCategory)
    {
        string apiUrl = $"https://actualbackendapp.azurewebsites.net/api/v1/Categories/{id}";

        try
        {
            var response = await _httpClient.PutAsJsonAsync(apiUrl, updatedCategory);

            if (response.IsSuccessStatusCode)
            {
                var updatedData = await response.Content.ReadFromJsonAsync<Category>();
                return new JsonResult(updatedData);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error updating category.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        string apiUrl = $"https://actualbackendapp.azurewebsites.net/api/v1/Categories/{id}";

        try
        {
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error deleting category.");
            }
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}