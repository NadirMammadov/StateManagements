using StateManagaments.Cache_Redis_.BackgroundServices;
using StateManagaments.Cache_Redis_.Hubs;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.Repository;
using StateManagments.Models;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.ModelsConfiguration(builder.Configuration);
// Add services to the container.
builder.Services.AddHostedService<BackgroundWorkerService>();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "CodeAcademyRedis_";
});
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<FoodDepHub>("/fooddepHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
