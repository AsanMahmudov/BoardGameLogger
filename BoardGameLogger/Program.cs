using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.Services;
using BoardGameLogger.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<BoardGameLoggerDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IBoardGameService, BoardGameService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
