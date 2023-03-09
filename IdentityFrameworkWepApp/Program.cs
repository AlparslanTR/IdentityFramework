using IdentityFrameworkWepApp.Data;
using Microsoft.EntityFrameworkCore;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Identity;
using IdentityFrameworkWepApp.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan=TimeSpan.FromHours(1);
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(15); // kullan�c�n�n kimlik do�rulama i�lemi s�ras�nda kullan�lan g�venlik damgas�n�n ge�erlilik s�resini belirler.
});

builder.Services.AddIdentityWithExt();
builder.Services.ConfigureApplicationCookie(opts =>
{
    var cookieBuilder= new CookieBuilder();
    cookieBuilder.Name = "LoginCookie"; // Cookinin ad�n� olu�turduk 
    opts.LoginPath = new PathString("/Login/SignIn"); // Taray�c�n�n kimlik do�rulama yapmas� gerekti�i sayfan�n yolu, "Login/SignIn" olarak belirlenir. Kullan�c� do�rulanmad��� zaman, sistem otomatik olarak y�nlendirece�i sayfan�n adresidir.
    opts.Cookie=cookieBuilder;
    opts.ExpireTimeSpan=TimeSpan.FromDays(60); // Cookinin 60 g�n boyunca ge�erli olmas�n� sa�lar.
    opts.SlidingExpiration = true; // kullan�c�n�n belirli bir s�re boyunca i�lem yapmamas� durumunda �erezin s�resinin yeniden ba�lat�lmas�n� sa�lar.
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory())); // bu kod, bir dosya sa�lay�c�s�n�n IServiceCollection i�inde kaydedilmesini sa�lar ve bu sa�lay�c�, �al��an i�lem i�in ge�erli �al��ma dizinindeki dosya bilgilerini sa�lamak i�in kullan�labilir hale getirir.

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

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
