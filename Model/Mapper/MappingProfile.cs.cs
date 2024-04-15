using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Model.DTOs;
using Model.Entitys;
namespace Model.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

            CreateMap<Actor, ActorDTO>();
            CreateMap<ActorDTO, Actor>();

            CreateMap<Movie, MovieDTO>()
            .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(
                src => src.Genres.Select(c => c.GenreId).ToArray()))
            .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(
                src => src.Actors.Select(c => c.ActorId).ToArray()));
            CreateMap<MovieDTO, Movie>();
            CreateMap<MovieDetail, MovieDetailDTO>();
            CreateMap<MovieDetailDTO, MovieDetail>();

        }
    }
            
}
