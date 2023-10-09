using System.Reflection;
using AnniesTech.DataBase;
using AnniesTech.Infrastructure.Interfaces;
using AnniesTech.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.LoginPath ="/Cuenta/Login";
});
builder.Services.AddDbContext<AnnisTechDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConnectionHome");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

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
app.UseAuthentication();
app.UseAuthorization();

//Midleware que redirige a la pagina principal a los usuiarios que no estan autorizados a cirtas vistas o controladores
app.UseStatusCodePagesWithRedirects("/Home/Index");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
