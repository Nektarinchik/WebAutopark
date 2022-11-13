using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Repositories;
using WebAutopark.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the app
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<IRepository<Components>, SQLComponentsRepository>(provider => new SQLComponentsRepository(connection));
builder.Services.AddTransient<IRepository<Orders>, SQLOrdersRepository>(provider => new SQLOrdersRepository(connection));
builder.Services.AddTransient<IRepository<Vehicles>, SQLVehiclesRepository>(provider => new SQLVehiclesRepository(connection));
builder.Services.AddTransient<IRepository<VehicleTypes>, SQLVehicleTypeRepository>(provider => new SQLVehicleTypeRepository(connection));
builder.Services.AddTransient<IRepository<OrderItems>, SQLOrderItemsRepository>(provider => new SQLOrderItemsRepository(connection));
builder.Services.AddControllersWithViews();

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

app.Run();
