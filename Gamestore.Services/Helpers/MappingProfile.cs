using System.Globalization;
using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.DAL.Entities;
using Gamestore.IdentityRepository.Identity;
using Gamestore.MongoRepository.Entities;
using Gamestore.MongoRepository.Helpers;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductCategory, Category>()
         .ForMember(dst => dst.Name, src => src.MapFrom(x => x.Category.Name))
         .ForMember(dst => dst.Id, src => src.MapFrom(x => x.CategoryId));
        CreateMap<ProductCategory, GenreModelDto>()
           .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Category.Id))
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Category.Name))
           .ReverseMap();

        CreateMap<ProductPlatform, Platform>()
            .ForMember(dst => dst.Id, src => src.MapFrom(x => x.Platform.Id))
            .ForMember(dst => dst.Type, src => src.MapFrom(x => x.Platform.Type));
        CreateMap<ProductPlatform, PlatformModelDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Platform.Id))
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Platform.Type))
            .ReverseMap();

        CreateMap<Platform, PlatformModelDto>().ReverseMap();
        CreateMap<Category, GenreModelDto>().ReverseMap();
        CreateMap<Supplier, PublisherModelDto>().ReverseMap();
        CreateMap<Order, OrderModelDto>()
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.OrderDate.ToString("yyyy-MM-dd")))
            .ReverseMap();

        CreateMap<OrderProduct, OrderGameModelDto>().ReverseMap();
        CreateMap<OrderProduct, OrderDetailsDto>();

        CreateMap<Product, GameModelDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Description, src => src.MapFrom<string>(x => x.Description))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price))
            .ForMember(dest => dest.PublishDate, src => src.MapFrom(x => x.PublishDate))
            .ForMember<PublisherModelDto>(dest => dest.Publisher, src => src.MapFrom(x => x.Publisher))
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discount))
            .ForMember<List<PlatformModelDto>>(dest => dest.Platforms, src => src.MapFrom(x => x.ProductPlatforms))
            .ForMember<List<GenreModelDto>>(dest => dest.Genres, src => src.MapFrom(x => x.ProductCategories));

        CreateMap<GameModelDto, Product>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember<string>(dest => dest.Description, src => src.MapFrom(x => x.Description))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price))
            .ForMember(dest => dest.PublishDate, src => src.MapFrom(x => x.PublishDate))
            .ForMember(dest => dest.Publisher, src => src.MapFrom<PublisherModelDto>(x => x.Publisher))
            .ForMember(dest => dest.Discount, src => src.MapFrom(x => x.Discontinued))
            .ForMember(dest => dest.ProductPlatforms, src => src.Ignore())
            .ForMember(dest => dest.ProductCategories, src => src.Ignore());

        CreateMap<PaymentModelDto, VisaMicroservicePaymentModel>()
            .ForMember(dest => dest.CardHolderName, src => src.MapFrom(x => x.Model.Holder))
            .ForMember(dest => dest.CardNumber, src => src.MapFrom(x => x.Model.CardNumber))
            .ForMember(dest => dest.Cvv, src => src.MapFrom(x => x.Model.Cvv2))
            .ForMember(dest => dest.ExpirationMonth, src => src.MapFrom(x => x.Model.MonthExpire))
            .ForMember(dest => dest.ExpirationYear, src => src.MapFrom(x => x.Model.YearExpire))
            .ForMember(dest => dest.TransactionAmount, src => src.MapFrom(x => x.Model.TransactionAmount))
            .ReverseMap();

        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body));

        CreateMap<MongoProduct, Product>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
           .ForMember(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
           .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductIdGuid))
           .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
           .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
           .ForMember(dest => dest.Discount, src => src.MapFrom(x => x.Discontinued))
           .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
           .ForMember(dest => dest.Publisher, src => src.MapFrom(x => x.Supplier))
           .ForMember(dest => dest.ProductCategories, src => src.MapFrom(x => x.ProductGenres))
           .ForMember(dest => dest.ProductPlatforms, src => src.MapFrom(x => x.ProductPlatforms))
           .ForMember(dest => dest.Comments, src => src.MapFrom(x => new List<Comment>()));

        CreateMap<MongoProduct, GameModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
            .ForMember<string>(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductIdGuid))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
            .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discontinued))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
            .ForMember<PublisherModelDto>(dest => dest.Publisher, src => src.MapFrom(x => x.Supplier))
            .ForMember<List<GenreModelDto>>(dest => dest.Genres, src => src.MapFrom(x => new List<GenreModelDto>() { new() { Id = x.ProductGenres[0].CategoryId } }))
            .ForMember<List<PlatformModelDto>>(dest => dest.Platforms, src => src.MapFrom(x => new List<PlatformModelDto>() { new() { Id = x.ProductPlatforms[0].PlatformId } }));

        CreateMap<MongoCategory, GenreModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.CategoryId)));

        CreateMap<MongoCategory, Category>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.CategoryId)));

        CreateMap<MongoSupplier, PublisherModelDto>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.SupplierID)));

        CreateMap<MongoSupplier, Supplier>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.SupplierID)));

        CreateMap<MongoPublisher, Supplier>().ReverseMap();
        CreateMap<MongoPublisher, PublisherModelDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName));
        CreateMap<MongoProductCategory, ProductCategory>().ReverseMap();
        CreateMap<MongoProductCategory, GenreModelDto>().ReverseMap();
        CreateMap<MongoProductPlatform, ProductPlatform>().ReverseMap();
        CreateMap<MongoProductPlatform, PlatformModelDto>().ReverseMap();

        CreateMap<MongoShipper, ShipperModelDto>().ReverseMap();

        CreateMap<MongoOrder, Order>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.OrderId)))
            .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => Guid.Empty))
            .ForMember(dest => dest.OrderDate, src => src.MapFrom(x => x.OrderDate))
            .ForMember(dest => dest.OrderProducts, src => src.Ignore())
            .ReverseMap();

        CreateMap<MongoOrder, OrderModelDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.OrderId)))
            .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => x.CustomerId))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.OrderDate))
            .ReverseMap();

        CreateMap<MongoOrderModel, OrderModelDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => x.CustomerId))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date.ToString("yyyy-MM-dd")));

        CreateMap<MongoOrder, MongoOrderModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.OrderId)))
            .ForMember(dest => dest.CustomerId, src => src.MapFrom(x => x.CustomerId))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => DateTime.ParseExact(x.OrderDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)))
            .ReverseMap();

        CreateMap<AppUser, CustomerDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.UserName));

        CreateMap<AppRole, RoleModel>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
    }
}
