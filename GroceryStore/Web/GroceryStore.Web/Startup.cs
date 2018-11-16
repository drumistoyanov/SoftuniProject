using AutoMapper;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Services.ManufacturersProducts;
using GroceryStore.Services.ManufacturersProducts.Interfaces;
using GroceryStore.Services.Orders;
using GroceryStore.Services.Orders.Interfaces;
using GroceryStore.Services.Products;
using GroceryStore.Services.Products.Interfaces;
using GroceryStore.Services.ShopCart;
using GroceryStore.Services.ShopCart.Interfaces;
using GroceryStore.Services.Types;
using GroceryStore.Services.Types.Interfaces;
using GroceryStore.Web.Areas.Identity.Services;
using GroceryStore.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<GroceryStoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<GroceryStoreDbContext>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IShoppingCartManager, ShoppingCartManager>();
            services.AddSingleton(new ProductCountToBuy());

            //services.AddAuthentication()
            //   .AddFacebook(option =>
            //   {
            //       option.AppId = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
            //       option.AppSecret = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
            //   })
            //   .AddGoogle(googleOptions =>
            //   {
            //       googleOptions.ClientId = this.Configuration.GetSection("ExternalAuthentication:Google:AppId").Value;
            //       googleOptions.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:Google:AppSecret").Value;
            //   });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 4,
                    RequiredUniqueChars = 1,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };

                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper();

            RegisterServiceLayer(services);

            services.AddSession();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "area",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IAdminUsersService, AdminUsersService>();
            services.AddScoped<IAdminManufacturersService, AdminManufacturersService>();
            services.AddScoped<IAdminProductsService, AdminProductsService>();
            services.AddScoped<IAdminImagesService, AdminImagesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IManufacturerProductsServices, ManufacturerProductsServices>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ITypesService, TypesService>();
        }
    }
}
