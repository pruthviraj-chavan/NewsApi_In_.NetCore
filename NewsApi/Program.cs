


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(); // Adds controller services for the application
builder.Services.AddHttpClient(); // Adds support for HttpClient
builder.Services.AddEndpointsApiExplorer(); // Adds support for Swagger endpoints
builder.Services.AddSwaggerGen(); // Adds Swagger generator

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseAuthorization(); // Ensure authorization is in the pipeline

app.MapControllers(); // Map controller endpoints

app.Run();
