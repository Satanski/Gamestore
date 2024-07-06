using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.DAL.Entities;
using Gamestore.MongoRepository.Entities;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Helpers;

public class MappingProfile : Profile
{
    private const string CategoryGuidSubstring = "2222-6666-6666-6666-666666666666";
    private const string ProductGuidSubstring = "1111-6666-6666-6666-666666666666";
    private const string SupplierGuidSubstring = "3333-6666-6666-6666-666666666666";

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
           .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.ProductId:D4}{ProductGuidSubstring}")))
           .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
           .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
           .ForMember(dest => dest.Discount, src => src.MapFrom(x => x.Discontinued))
           .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
           .ForMember(dest => dest.PublisherId, src => src.MapFrom(x => new Guid($"{x.SupplierID:D4}{SupplierGuidSubstring}")))
           .ForMember(dest => dest.GameGenres, src => src.MapFrom(x => new List<GameGenre>() { new() { GameId = new Guid($"{x.ProductId:D4}{ProductGuidSubstring}"), GenreId = new Guid($"{x.CategoryID:D4}{CategoryGuidSubstring}") } }));

        CreateMap<Product, GameModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Key, src => src.MapFrom(x => x.ProductName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.ProductId:D4}{ProductGuidSubstring}")))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.UnitPrice))
            .ForMember(dest => dest.UnitInStock, src => src.MapFrom(x => x.UnitsInStock))
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discontinued))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.QuantityPerUnit))
            .ForMember(dest => dest.Publisher, src => src.MapFrom(x => new Publisher() { Id = new Guid($"{x.SupplierID:D4}{SupplierGuidSubstring}") }))
            .ForMember(dest => dest.Genres, src => src.MapFrom(x => new List<GameGenre>() { new() { GameId = new Guid($"{x.ProductId:D4}{ProductGuidSubstring}"), GenreId = new Guid($"{x.CategoryID:D4}{CategoryGuidSubstring}") } }));

        CreateMap<Category, GenreModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.CategoryId:D4}{CategoryGuidSubstring}")));

        CreateMap<Category, Genre>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CategoryName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.CategoryId:D4}{CategoryGuidSubstring}")));

        CreateMap<Supplier, PublisherModelDto>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.SupplierID:D4}{SupplierGuidSubstring}")));

        CreateMap<Supplier, Publisher>()
            .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => new Guid($"{x.SupplierID:D4}{SupplierGuidSubstring}")));
    }
}
