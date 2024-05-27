using AutoMapper;
using Gamestore.DAL.Entities;
using Gamestore.Services.Models;

namespace Gamestore.BLL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Game, GameModel>().ReverseMap();
        CreateMap<Genre, GenreModel>().ReverseMap();
        CreateMap<Platform, PlatformModel>().ReverseMap();

        CreateMap<Game, GameModelDto>().ReverseMap();
        CreateMap<Genre, GenreModelDto>().ReverseMap();
        CreateMap<Platform, PlatformModelDto>().ReverseMap();
    }
}
