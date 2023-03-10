using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Azure;
using Application.Dto;
using Azure.Storage.Blobs;
using Application.Services.Storage.Interfaces;
using Application.Services.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DataContext>(opt =>
{
    CosmosDbCredentialsDto config = builder.Configuration.GetSection("CosmosDb").Get<CosmosDbCredentialsDto>();

    opt.UseCosmos(
        config.AccountEndPoint,
        config.AccountKey,
        config.DatabaseName
        );
});

builder.Services.AddScoped(_ => { 
    return new BlobServiceClient(builder.Configuration.Get<AzureBlobCredentialsDto>().AzureBlobConf.ConnectionString);
});
builder.Services.AddScoped<IStorageService, AzureBlobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
