using AutoMapper;
using Scrummage.Dtos;
using Scrummage.Models;

namespace Scrummage.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamDto, Team>();
            CreateMap<Team, TeamDto>();
        }
    }
}