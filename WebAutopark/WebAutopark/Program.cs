using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Repositories;
using WebAutopark.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the app
builder.Services.AddTransient<IRepository<Components>, SQLComponentsRepository>();
builder.Services.AddTransient<IRepository<Orders>, SQLOrdersRepository>();
builder.Services.AddTransient<IRepository<Vehicles>, SQLVehiclesRepository>();
builder.Services.AddTransient<IRepository<VehicleTypes>, SQLVehicleTypeRepository>();
builder.Services.AddTransient<IRepository<OrderItems>, SQLOrderItemsRepository>();

// Add services to the container.
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
