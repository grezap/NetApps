using AutoMapper;
using DataAccess.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginAppService.Mapping
{
    public class BusinessToDbMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "BusinessToDbMappings"; }
        }

        public BusinessToDbMappingProfile()
        {
            CreateMap<ApplicationUser, AppUser>();
            CreateMap<ApplicationRole, AppRole>();
            CreateMap<ApplicationUserRole, AppUserToRole>();
        }
    }
}
