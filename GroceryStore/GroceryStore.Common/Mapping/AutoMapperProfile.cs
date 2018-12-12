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

namespace GroceryStore.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserIndexViewModel>();

            CreateMap<User, UserDetailsViewModel>()
                .ForMember(dto => dto.Orders, dest => dest.MapFrom(o => o.Orders));

            CreateMap<Manufacturer, ManufacturerIndexViewModel>()
                .ForMember(dto => dto.ProductsCount, dest => dest.MapFrom(p => p.Products.Count));

            CreateMap<Manufacturer, ManufacturerDetailsViewModel>();

            CreateMap<ManufacturerBindingModel, Manufacturer>();

            CreateMap<Manufacturer, ManufacturerBindingModel>();

            CreateMap<Product, ProductIndexViewModel>();

            CreateMap<ProductBindingModel, Product>();

            CreateMap<Product, ProductBindingModel>();

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(dto => dto.Manufacturer, dest => dest.MapFrom(t => t.Manufacturer))
                .ForMember(dto => dto.Images, dest => dest.MapFrom(im => im.Images));

            CreateMap<ImageBindingModel, Image>();

            CreateMap<Image, ImageBindingModel>();

            CreateMap<Product, ProductCartViewModel>();

            CreateMap<Order, UserOrdersViewModel>()
                .ForMember(dto => dto.OrderProductsCount, dest => dest.MapFrom(op => op.OrderProducts.Count));

            CreateMap<OrderProduct, OrderProductsViewModel>();
        }
    }
}
