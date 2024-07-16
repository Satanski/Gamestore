using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.MongoRepository.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Tests.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, GenreModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ReverseMap();
        CreateMap<ProductCategory, Category>()
            .ForMember(dst => dst.Name, src => src.MapFrom(x => x.Category.Name))
            .ForMember(dst => dst.Id, src => src.MapFrom(x => x.CategoryId))
            .ReverseMap();
        CreateMap<ProductCategory, GenreModelDto>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Category.Name))
           .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Category.Id))
           .ForMember(dest => dest.ParentGenreId, src => src.MapFrom(x => x.Category.ParentCategoryId))
           .ReverseMap();

        CreateMap<ProductPlatform, PlatformModelDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Platform.Type))
            .ReverseMap();

        CreateMap<DAL.Entities.Product, GameModelDto>()
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discount))
            .ForMember<List<PlatformModelDto>>(dest => dest.Platforms, src => src.MapFrom(x => x.ProductPlatforms))
            .ForMember<List<GenreModelDto>>(dest => dest.Genres, src => src.MapFrom(x => x.ProductCategories))
            .ForMember(dest => dest.PublishDate, src => src.MapFrom(x => x.PublishDate))
            .ReverseMap();
        CreateMap<Platform, PlatformModelDto>().ReverseMap();
        CreateMap<Supplier, PublisherModelDto>().ReverseMap();

        CreateMap<Product, Product>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductId))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
            .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
            .ForMember(dest => dest.Discount, src => src.MapFrom(x => x.Discontinued))
            .ForMember(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.PublisherId, src => src.MapFrom(x => x.SupplierID))
            .ForMember(dest => dest.ProductCategories, src => src.MapFrom(x => new ProductCategory() { ProductId = Guid.Parse(x.ObjectId), CategoryId = Guid.Parse(x.CategoryID.ToString()) }))
            .ReverseMap();
    }
}
