using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCAS.DataAccess;
using OCAS.WebAPI.Models;

namespace OCAS.WebAPI.Configurations
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }

}
