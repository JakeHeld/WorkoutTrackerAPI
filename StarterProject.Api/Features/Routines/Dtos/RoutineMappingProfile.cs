using AutoMapper;
using StarterProject.Api.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterProject.Api.Features.Routines.Dtos
{
    public class RoutineMappingProfile : Profile
    {
        public RoutineMappingProfile()
        {
            CreateMap<RoutineCreateDto, Routine>();
            CreateMap<Routine, RoutineGetDto>();
            CreateMap<RoutineEditDto, Routine>();
            CreateMap<RoutineGetDto, Routine>();
            
        }
    }
}
