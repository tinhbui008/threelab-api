using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threelab.Domain.Entities;
using Threelab.Domain.Requests;
using Threelab.Domain.Response;

namespace Threelab.Application.Core.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<UserInfoResponse, User>().ReverseMap();
        }
    }
}
