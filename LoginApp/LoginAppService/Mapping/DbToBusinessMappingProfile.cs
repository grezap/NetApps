using AutoMapper;
using DataAccess.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginAppService.Mapping
{
    public class DbToBusinessMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DbToBusinessMappingMappings"; }
        }

        public DbToBusinessMappingProfile()
        {
            CreateMap<AppUser, ApplicationUser>();
            CreateMap<AppRole, ApplicationRole>();
            CreateMap<AppUserToRole,ApplicationUserRole>();
        }

    }
}
