﻿using AutoMapper;
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

            CreateMap<ApplicationUser, UserDto>();

            CreateMap<NewScrumTaskDto, ScrumTask>();

            CreateMap<SprintDto, Sprint>();
            CreateMap<NewSprintViewModel, Sprint>();

            CreateMap<ScrumTask, ScrumTaskDto>();

            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<Sprint, StatisticsSprintViewModel>();

            CreateMap<Estimation, EstimationDto>();

        }
    }
}