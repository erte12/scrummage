using AutoMapper;
using Scrummage.Core.Domain;
using Scrummage.Dtos;
using Scrummage.Models;
using Scrummage.Presentation.Dtos;
using Scrummage.Presentation.ViewModels;
using Scrummage.ViewModels;

namespace Scrummage.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamDto, Team>();
            CreateMap<Team, TeamDto>();

            CreateMap<ScrumTaskNewDto, ScrumTask>();

            CreateMap<SprintDto, Sprint>();
            CreateMap<SprintNewViewModel, Sprint>();

            CreateMap<ScrumTask, ScrumTaskDto>();

            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<Sprint, SprintStatisticsViewModel>();

            CreateMap<Estimation, EstimationDto>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
        }
    }
}