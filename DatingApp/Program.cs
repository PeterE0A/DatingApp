using DatingApp.Data; // Adjust the namespace as needed
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DatingApp.Services; // Replace with your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = "DatingAppDB"; // Replace with your actual connection string

builder.Services.AddSingleton<IDatabaseService>(new DatabaseService(connectionString));


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
