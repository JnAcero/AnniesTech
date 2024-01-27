using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnniesTech.Infrastructure.DTOs;
using AnniesTech.Models;
using AnniesTech.Models.DTOs;
using AutoMapper;

namespace AnniesTech.Infrastructure.Profiles
{
    public class MapperProfiel : Profile
    {
        public MapperProfiel()
        {
            CreateMap<Usuario,RegisterDto>().ReverseMap();
            CreateMap<Post,PostCreationDTO>().ReverseMap();
            CreateMap<Usuario,UsuarioCreationDTO>().ReverseMap();
        }
    }
}