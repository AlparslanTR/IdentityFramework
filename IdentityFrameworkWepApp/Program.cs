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
    options.ValidationInterval = TimeSpan.FromMinutes(15); // kullanýcýnýn kimlik doðrulama iþlemi sýrasýnda kullanýlan güvenlik damgasýnýn geçerlilik süresini belirler.
});

builder.Services.AddIdentityWithExt();
builder.Services.ConfigureApplicationCookie(opts =>
{
    var cookieBuilder= new CookieBuilder();
    cookieBuilder.Name = "LoginCookie"; // Cookinin adýný oluþturduk 
    opts.LoginPath = new PathString("/Login/SignIn"); // Tarayýcýnýn kimlik doðrulama yapmasý gerektiði sayfanýn yolu, "Login/SignIn" olarak belirlenir. Kullanýcý doðrulanmadýðý zaman, sistem otomatik olarak yönlendireceði sayfanýn adresidir.
    opts.Cookie=cookieBuilder;
    opts.ExpireTimeSpan=TimeSpan.FromDays(60); // Cookinin 60 gün boyunca geçerli olmasýný saðlar.
    opts.SlidingExpiration = true; // kullanýcýnýn belirli bir süre boyunca iþlem yapmamasý durumunda çerezin süresinin yeniden baþlatýlmasýný saðlar.
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory())); // bu kod, bir dosya saðlayýcýsýnýn IServiceCollection içinde kaydedilmesini saðlar ve bu saðlayýcý, çalýþan iþlem için geçerli çalýþma dizinindeki dosya bilgilerini saðlamak için kullanýlabilir hale getirir.

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
