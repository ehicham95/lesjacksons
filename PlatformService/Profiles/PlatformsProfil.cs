using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dtos;
using Models;

namespace Profiles
{
    //TODO: understand profile
    public class PlatformsProfil : Profile
    {
        public PlatformsProfil()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}