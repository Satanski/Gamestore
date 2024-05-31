using AutoMapper;
using Gamestore.DAL.Entities;
using Gamestore.Services.Models;

namespace Gamestore.Tests.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GenreModelDto, GameGenre>().ForMember(dest => dest.GenreId, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.GameId, src => src.Ignore());

        CreateMap<PlatformModelDto, GamePlatform>().ForMember(dest => dest.PlatformId, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.GameId, src => src.Ignore());

        CreateMap<Game, GameModel>().ReverseMap();
        CreateMap<Genre, GenreModel>().ReverseMap();
        CreateMap<Platform, PlatformModel>().ReverseMap();

        CreateMap<Game, GameModelDto>().ReverseMap();
        CreateMap<Genre, GenreModelDto>().ReverseMap();
        CreateMap<Platform, PlatformModelDto>().ReverseMap();
    }
}
