using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Images;
using GroceryStore.Common.BindingModels.Admin.Manufacturers;
using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Common.ViewModels.Admin.Users;
using GroceryStore.Common.ViewModels.Orders;
using GroceryStore.Common.ViewModels.ShoppingCart;
using GroceryStore.Data.Models;

namespace GroceryStore.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserIndexViewModel>();

            this.CreateMap<User, UserDetailsViewModel>()
                .ForMember(dto => dto.Orders, dest => dest.MapFrom(o => o.Orders));

            this.CreateMap<Manufacturer, ManufacturerIndexViewModel>()
                .ForMember(dto => dto.ProductsCount, dest => dest.MapFrom(p => p.Products.Count));

            this.CreateMap<Manufacturer, ManufacturerDetailsViewModel>();

            this.CreateMap<ManufacturerBindingModel, Manufacturer>();

            this.CreateMap<Manufacturer, ManufacturerBindingModel>();

            this.CreateMap<Product, ProductIndexViewModel>();

            this.CreateMap<ProductBindingModel, Product>();

            this.CreateMap<Product, ProductBindingModel>();

            this.CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(dto => dto.Manufacturer, dest => dest.MapFrom(t => t.Manufacturer))
                .ForMember(dto => dto.Images, dest => dest.MapFrom(im => im.Images));

            this.CreateMap<ImageBindingModel, Image>();

            this.CreateMap<Image, ImageBindingModel>();

            this.CreateMap<Product, ProductCartViewModel>();

            this.CreateMap<Order, UserOrdersViewModel>()
                .ForMember(dto => dto.OrderProductsCount, dest => dest.MapFrom(op => op.OrderProducts.Count));

            this.CreateMap<OrderProduct, OrderProductsViewModel>();
        }
    }
}
