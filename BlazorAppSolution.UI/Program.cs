using BlazorAppSolution.UI.Components;
using BlazorAppSolution.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register CategoryService
builder.Services.AddScoped<CategoryService>(); // Add this line
builder.Services.AddScoped<CourseService>();

// Add controllers
builder.Services.AddControllers();

// Register HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseRouting();
app.MapControllers(); // Maps controllers based on their routes

app.MapBlazorHub();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
