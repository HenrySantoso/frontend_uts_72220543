namespace BlazorAppSolution.UI;
public class Course
{
    public int? courseId { get; set; }

    public string? name { get; set; }

    public string? imageName { get; set; }

    public int? duration { get; set; }

    public string? description { get; set; }

    public Category category { get; set; }
}
