using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Payment;
using Gamestore.DAL.Entities;
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
            .ReverseMap();

        CreateMap<PaymentModelDto, VisaMicroservicePaymentModel>()
            .ForMember(dest => dest.CardHolderName, src => src.MapFrom(x => x.Model.Holder))
            .ForMember(dest => dest.CardNumber, src => src.MapFrom(x => x.Model.CardNumber))
            .ForMember(dest => dest.Cvv, src => src.MapFrom(x => x.Model.Cvv2))
            .ForMember(dest => dest.ExpirationMonth, src => src.MapFrom(x => x.Model.MonthExpire))
            .ForMember(dest => dest.ExpirationYear, src => src.MapFrom(x => x.Model.YearExpire))
            .ForMember(dest => dest.TransactionAmount, src => src.MapFrom(x => x.Model.TransactionAmount))
            .ReverseMap();

        CreateMap<PaymentModelDto, IboxPaymentModel>()
            .ForMember(dest => dest.InvoiceNumber, src => src.MapFrom(x => x.OrderId))
            .ForMember(dest => dest.AccountNumber, src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TransactionAmount, src => src.MapFrom(x => x.Sum))
            .ReverseMap();

        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body));
    }
}
