using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.DAL.Entities;
using Gamestore.MongoRepository.Entities;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GameGenre, Genre>()
         .ForMember(dst => dst.Name, src => src.MapFrom(x => x.Genre.Name))
         .ForMember(dst => dst.Id, src => src.MapFrom(x => x.GenreId));
        CreateMap<GameGenre, GenreModelDto>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Genre.Name));

        CreateMap<GamePlatform, Platform>()
            .ForMember(dst => dst.Type, src => src.MapFrom(x => x.Platform.Type));
        CreateMap<GamePlatform, PlatformModelDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Platform.Type));

        CreateMap<Platform, PlatformModelDto>().ReverseMap();
        CreateMap<Genre, GenreModelDto>().ReverseMap();
        CreateMap<Publisher, PublisherModelDto>().ReverseMap();
        CreateMap<Order, OrderModelDto>().ReverseMap();
        CreateMap<OrderGame, OrderGameModelDto>().ReverseMap();

        CreateMap<OrderGame, OrderDetailsDto>();

        CreateMap<Game, GameModelDto>()
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discount))
            .ForMember(dest => dest.Platforms, src => src.MapFrom(x => x.GamePlatforms))
            .ForMember(dest => dest.Genres, src => src.MapFrom(x => x.GameGenres))
            .ForMember(dest => dest.PublishDate, src => src.MapFrom(x => x.PublishDate))
            .ReverseMap();

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

        CreateMap<Product, Game>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
           .ForMember(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
           .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductIdGuid))
           .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
           .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
           .ForMember(dest => dest.Discount, src => src.MapFrom(x => x.Discontinued))
           .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
           .ForMember(dest => dest.Publisher, src => src.MapFrom(x => x.Supplier))
           .ForMember(dest => dest.GameGenres, src => src.MapFrom(x => x.GameGenres))
           .ForMember(dest => dest.GamePlatforms, src => src.MapFrom(x => x.GamePlatforms))
           .ForMember(dest => dest.Comments, src => src.MapFrom(x => new List<Comment>()));

        CreateMap<Product, GameModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductIdGuid))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
            .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discontinued))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
            .ForMember(dest => dest.Publisher, src => src.Ignore())
            .ForMember(dest => dest.Genres, src => src.Ignore());

        CreateMap<Category, GenreModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.CategoryId)));

        CreateMap<Category, Genre>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.CategoryId)));

        CreateMap<Supplier, PublisherModelDto>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.SupplierID)));

        CreateMap<Supplier, Publisher>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => GuidHelpers.IntToGuid(x.SupplierID)));

        CreateMap<Publisher, MongoPublisher>().ReverseMap();
        CreateMap<GameGenre, MongoGameGenre>().ReverseMap();
        CreateMap<GamePlatform, MongoGamePlatform>().ReverseMap();

        CreateMap<Shipper, ShipperModelDto>().ReverseMap();
        CreateMap<MongoOrder, MongoOrderDto>().ReverseMap();
    }
}
