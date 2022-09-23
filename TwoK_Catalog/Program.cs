using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TwoK_Catalog.Models.BusinessModels;
using TwoK_Catalog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TwoK_CatalogDB;Trusted_Connection=True;MultipleActiveResultSets=True;"));
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mssqllocaldb;Trusted_Connection=True;"));
//builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDBConnectionString")));
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = ".@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞßàáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿ1234567890";
    opts.SignIn.RequireConfirmedPhoneNumber = false;
    opts.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddTransient<ICartRepository, EFCartRepository>();
builder.Services.AddTransient<IUserRepository, EFUsersRepository>();
builder.Services.AddTransient<ICategoriesAndCompanysInfoRepository, EFCategoriesAndCompanysInfoRepository>();
builder.Services.AddScoped<Cart>(sp => Cart.GetCart(sp));
builder.Services.AddScoped<SessionRegisterViewModel>(sp => (SessionRegisterViewModel)SessionRegisterViewModel.GetRegisterViewModel(sp));
builder.Services.AddScoped<SessionLogInViewModel>(sp => (SessionLogInViewModel)SessionLogInViewModel.GetLogInViewModel(sp));
builder.Services.AddTransient<CategoriesAndCompanysInfoViewModel>(sp => CategoriesAndCompanysInfoViewModel.GetCategoriesAndCompanysInfoViewModel(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMvc(mvcOptions =>
{
    mvcOptions.EnableEndpointRouting = false;
});
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
await ApplicationIdentityDbContext.CreateDefaultRoles(app, app.Configuration);
await ApplicationIdentityDbContext.CreateSeniorAdminAccount(app, app.Configuration);
await ApplicationDbContext.CreateDefaultCopanys(app);
await ApplicationDbContext.CreateDefaultCategories(app);
SeedData.EnsurePopulated(app);

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "pagination",
        template: "Products/Page{productPage}",
        defaults: new { controller = "Product", action = "List" });
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run(async context =>
{
    var response = context.Response;
    response.Headers.ContentLanguage = "ru-RU";
});

app.Run();
