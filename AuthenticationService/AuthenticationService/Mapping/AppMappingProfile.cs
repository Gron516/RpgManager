﻿using AuthenticationService.Entities;
using AuthenticationService.Helpers;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Configurations;

public class AppMappingProfile : Profile 
{
    public AppMappingProfile()
    {
        CreateMap<PersonModel, PersonEntity>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashHelper.GetHash(src.Password)))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<GroupModel, GroupEntity>();
        CreateMap<PersonGroupModel, PersonGroupEntity>();
        CreateMap<GroupEntity?, GroupModel?>();
        CreateMap<PersonGroupEntity?, PersonGroupModel?>();
        CreateMap<PersonEntity?, PersonModel? >();
    }
}