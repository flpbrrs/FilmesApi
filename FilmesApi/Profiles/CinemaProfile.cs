using AutoMapper;
using FilmesApi.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDTO, Cinema>();
        CreateMap<UpdateCinemaDTO, Cinema>();
        CreateMap<Cinema, ReadCinemaDTO>()
            .ForMember(
                cinemaDTO => cinemaDTO.endereco,
                opt => opt.MapFrom(cinema => cinema.Endereco)
            );
    }
}
