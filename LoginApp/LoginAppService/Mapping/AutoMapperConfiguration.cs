﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginAppService.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Reset();
            Mapper.Initialize(x =>
            {
                x.AddProfile<BusinessToDbMappingProfile>();
                x.AddProfile<DbToBusinessMappingProfile>();
            });
        }
    }
}
