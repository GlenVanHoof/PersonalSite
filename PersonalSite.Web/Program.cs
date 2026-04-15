using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Services;
using PersonalSite.Infrastructure.Data;
using PersonalSite.Infrastructure.Repositories;
using System.Globalization;



var builder = WebApplication.CreateBuilder(args);

// Determine environment
var env = builder.Environment.EnvironmentName;

// Pick the correct connection string
string connectionString = env switch
{
    "Production" => builder.Configuration.GetConnectionString("ProductionConnection"),
    "Development" => builder.Configuration.GetConnectionString("DevelopmentConnection"),
    "Remote" => builder.Configuration.GetConnectionString("RemoteConnection"),
    _ => builder.Configuration.GetConnectionString("DevelopmentConnection") // fallback
} ?? throw new InvalidOperationException($"Connection string not found for environment: {env}");

// Register DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Globalization and Localization services
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en"),
        new CultureInfo("nl"),
        new CultureInfo("fr"),
        new CultureInfo("de")
    };
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.DefaultRequestCulture = new RequestCulture("en");
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
    .AddDataAnnotationsLocalization();

// Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectTranslationRepository, ProjectTranslationRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectTranslationService, ProjectTranslationService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IContactService, ContactService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Console.WriteLine("ENVIRONMENT = " + env);
Console.WriteLine("CONNECTION STRING = " + connectionString);
Console.WriteLine($"CONNECTION STRING = {connectionString}");

// Only redirect HTTPS when not in Remote environment (Tailscale doesn't use HTTPS)
if (env != "Remote")
{
    app.UseHttpsRedirection();
}

// Add static files middleware for remote access
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
      .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();