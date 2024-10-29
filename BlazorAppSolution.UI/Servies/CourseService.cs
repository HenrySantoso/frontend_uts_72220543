using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppSolution.UI.Model; // Adjust the namespace according to your project structure

namespace BlazorAppSolution.UI.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://actualbackendapp.azurewebsites.net/api/Courses";

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all courses
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>(ApiUrl);
        }

        // Get a course by ID
        public async Task<Course> GetCourseAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"{ApiUrl}/{id}");
        }

        // Create a new course
        public async Task<Course> CreateCourseAsync(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Course>();
        }

        // Update an existing course
        public async Task<Course> UpdateCourseAsync(int id, Course course)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Course>();
        }

        // Delete a course
        public async Task DeleteCourseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
