using ClientProcessing.Application.Implementation;
using ClientProcessing.Application.Interface;
using ClientProcessing.Domain.Models;
using ClientProcessing.Infrastructure;
using ClientProcessing.Infrastructure.Implementation;
using ClientProcessing.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appSettings = LoadAppSettings();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Set default JSON serialization settings
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;  // Format JSON output for readability
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());  // Handle enums as strings
    })
    .AddXmlSerializerFormatters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    context.Request.Headers["Accept"] = "application/xml";
    await next();
});

app.UseRouting();

app.UseCors("AllowAnyOrigin");


app.UseAuthorization();

app.MapRazorPages();

app.Run();

IConfigurationRoot LoadAppSettings() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, false)
            .Build();
