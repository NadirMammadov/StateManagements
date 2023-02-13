using StateManagaments.Cache_Redis_.Hubs;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.MiddlewareExtensions;
using StateManagaments.Cache_Redis_.Repository;
using StateManagaments.Cache_Redis_.SubscribeTableDependencies;
using StateManagments.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.ModelsConfiguration(builder.Configuration);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "CodeAcademyRedis_";
});
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSingleton<SubscribeFoodTableDependency>();

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

app.UseFoodTableDependency();
app.Run();
