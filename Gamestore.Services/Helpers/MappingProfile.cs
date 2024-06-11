using AutoMapper;
using Gamestore.BLL.Models;
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
        CreateMap<GameGenre, GenreModel>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Genre.Name))
            .ReverseMap();
        CreateMap<GameGenre, GenreModelDto>()
           .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Genre.Name));

        CreateMap<GamePlatform, Platform>()
            .ForMember(dst => dst.Type, src => src.MapFrom(x => x.Platform.Type));
        CreateMap<GamePlatform, PlatformModel>()
            .ForMember(dst => dst.Type, src => src.MapFrom(x => x.Platform.Type))
            .ReverseMap();
        CreateMap<GamePlatform, PlatformModelDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Platform.Type));

        CreateMap<Platform, PlatformModel>().ReverseMap();
        CreateMap<Platform, PlatformModelDto>().ReverseMap();
        CreateMap<Genre, GenreModel>().ReverseMap();
        CreateMap<Genre, GenreModelDto>().ReverseMap();
        CreateMap<Publisher, PublisherModel>().ReverseMap();
        CreateMap<Publisher, PublisherModelDto>().ReverseMap();

        CreateMap<GameAdd, Game>().ReverseMap();
        CreateMap<Game, GameModel>().ReverseMap();
        CreateMap<Game, GameModelDto>()
            .ForMember(dest => dest.Platforms, src => src.MapFrom(x => x.GamePlatforms))
            .ForMember(dest => dest.Genres, src => src.MapFrom(x => x.GameGenres));
        CreateMap<GameUpdateModel, Game>();
    }
}
