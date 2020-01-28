using AutoMapper;
using PushFive.Catalog.Domain.Models;
using PushFive.Catalog.WebApi.Dtos;

namespace PushFive.Catalog.WebApi.AutoMapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Song, SongDto>().ConvertUsing<SongToSongDtoConverter>();
        }
    }
}
