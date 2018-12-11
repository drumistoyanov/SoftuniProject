namespace GroceryStore.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using GroceryStore.Common.Constants.AreaAdmin;
    using GroceryStore.Common.SeedDtoModels;
    using GroceryStore.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class ApplicationDbContextSeeder
    {
        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var context = scope.ServiceProvider.GetService<GroceryStoreDbContext>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (!await roleManager.RoleExistsAsync(AdminConstants.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(AdminConstants.AdminRoleName));
                }

                var user = await userManager.FindByNameAsync(AdminConstants.Admin);
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = AdminConstants.Admin,
                        Email = AdminConstants.AdminEmail,
                        DateOfRegistration = DateTime.UtcNow,
                        FullName = AdminConstants.AdminFullName,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, AdminConstants.AdminPassword);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, AdminConstants.AdminRoleName));
                    await userManager.AddToRoleAsync(user, AdminConstants.AdminRoleName);
                }

                if (!context.Manufacturers.Any())
                {
                    var jsonManufacturers = File.ReadAllText(@"wwwroot\seedfiles\manufacturers.json");
                    var manufacturerDtos = JsonConvert.DeserializeObject<ManufacturerDto[]>(jsonManufacturers);

                    SeedManufacturers(context, manufacturerDtos);
                }

                if (!context.Products.Any())
                {
                    var jsonProducts = File.ReadAllText(@"wwwroot\seedfiles\products.json");
                    var productDtos = JsonConvert.DeserializeObject<ProductDto[]>(jsonProducts);

                    SeedProducts(context, productDtos);
                }

                if (!context.Images.Any())
                {
                    var jsonImages = File.ReadAllText(@"wwwroot\seedfiles\images.json");
                    var imageDtos = JsonConvert.DeserializeObject<ImageDto[]>(jsonImages);

                    SeedImages(context, imageDtos);
                }
            }
        }

        private static void SeedManufacturers(GroceryStoreDbContext context, ManufacturerDto[] manufacturerDtos)
        {
            var manufacturersToCreate = manufacturerDtos
                .Select(t => new Manufacturer
                {
                    Name = t.Name,
                    LogoUrl = t.LogoUrl,
                })
                .ToArray();

            context.Manufacturers.AddRange(manufacturersToCreate);
            context.SaveChanges();
        }

        private static void SeedProducts(GroceryStoreDbContext context, ProductDto[] productDtos)
        {
            var productsToCreate = new List<Product>();
            foreach (var productDto in productDtos)
            {
                var manufacturer = context.Manufacturers.SingleOrDefault(x => x.Name == productDto.ManufacturerName);

                if (manufacturer != null)
                {
                    var manufacturerId = manufacturer.Id;
                    var product = new Product
                    {
                        Name = productDto.Name,
                        PictureUrl = productDto.PictureUrl,
                        Price = productDto.Price,
                        Discount = productDto.Discount,
                        Kind = productDto.Kind,
                        Type = productDto.Type,
                        Weight = productDto.Weight,
                        ManufacturerId = manufacturerId,
                    };

                    productsToCreate.Add(product);
                }
            }

            context.Products.AddRange(productsToCreate);
            context.SaveChanges();
        }

        private static void SeedImages(GroceryStoreDbContext context, ImageDto[] imagesDtos)
        {
            var imagesToCreate = new List<Image>();
            foreach (var imageDto in imagesDtos)
            {
                var product = context.Products.SingleOrDefault(x => x.Name == imageDto.ProductName);

                if (product != null)
                {
                    var productId = product.Id;
                    var image = new Image
                    {
                        Url = imageDto.Url,
                        ProductId = productId
                    };

                    imagesToCreate.Add(image);
                }
            }

            context.Images.AddRange(imagesToCreate);
            context.SaveChanges();
        }
    }
}
