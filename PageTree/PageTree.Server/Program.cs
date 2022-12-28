using Corelibs.AspNetApi;
using Corelibs.Basic.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using PageTree.App.UseCases;
using PageTree.Server.Api;
using PageTree.Server.ApiContracts;
using PageTree.Server.Data;
using PageTree.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi()
                    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
                    .AddInMemoryTokenCaches()
                    .AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
                    .AddInMemoryTokenCaches();
builder.Services.AddDbContext<AppDbContext>(builder.Environment, builder.Configuration.GetConnectionString);
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews(opts => opts.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddRazorPages();

builder.Services.AddMediatorExt();
builder.Services.AddRepositories(builder.Environment);
builder.Services.AddAutoMapper();

var app = builder.Build();

var logger = app.Services.GetRequiredService<Corelibs.Basic.Logging.ILogger>();
logger.Log("Init log");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseExceptionLog();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
