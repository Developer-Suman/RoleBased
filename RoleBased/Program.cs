using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleBased.Automapper;
using RoleBased.Models.Domain;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Repository.RepoInterface;
using RoleBased.Services.ServiceImplementation;
using RoleBased.Services.ServiceInterface;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;







var builder = WebApplication.CreateBuilder(args);

var mapconfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<DomainProfile>();
}
 );




// Add services to the container.
builder.Services.AddScoped<UnitOfWorkRepoImpl>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDb>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAuthenticationServices,UserAuthenticationService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserServiceImpl>();







builder.Services.AddScoped<IMaterialServicecs, MaterialsServices>();
builder.Services.AddScoped<ICategoriesServices, CategoriesServicesImpl>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/UserAuthentication/Login";
//        options.AccessDeniedPath = "/Account/AccessDenied";
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
//});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.LoginPath = "/UserAuthentication/Login";
});







//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDb>()
    .AddDefaultTokenProviders();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();

//}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Configure error handling for production environment
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePages}/{id?}");

app.Run();



