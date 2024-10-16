﻿using AutoMapper;
using FilmesApi.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDTO, Endereco>();
        CreateMap<UpdateEnderecoDTO, Endereco>();
        CreateMap<Endereco, ReadEnderecoDTO>();
    }
}
