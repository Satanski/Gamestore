using AutoMapper;
using Gamestore.BLL.Models;
using Gamestore.DAL.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Tests.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Genre, GenreModelDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ReverseMap();
        CreateMap<GameGenre, Genre>()
            .ForMember(dst => dst.Name, src => src.MapFrom(x => x.Genre.Name))
            .ForMember(dst => dst.Id, src => src.MapFrom(x => x.GenreId))
            .ReverseMap();
        CreateMap<GameGenre, GenreModelDto>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Genre.Name))
           .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Genre.Id))
           .ForMember(dest => dest.ParentGenreId, src => src.MapFrom(x => x.Genre.ParentGenreId))
           .ReverseMap();

        CreateMap<GamePlatform, PlatformModelDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Platform.Type))
            .ReverseMap();

        CreateMap<Game, GameModelDto>()
            .ForMember(dest => dest.Discontinued, src => src.MapFrom(x => x.Discount))
            .ForMember(dest => dest.Platforms, src => src.MapFrom(x => x.GamePlatforms))
            .ForMember(dest => dest.Genres, src => src.MapFrom(x => x.GameGenres))
            .ForMember(dest => dest.PublishDate, src => src.MapFrom(x => x.PublishDate))
            .ReverseMap();
        CreateMap<Platform, PlatformModelDto>().ReverseMap();
        CreateMap<Publisher, PublisherModelDto>().ReverseMap();
    }
}
