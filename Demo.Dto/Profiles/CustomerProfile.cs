using AutoMapper;
using AutoMapper.Mappers;
using Demo.Domain;
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dto.Profiles
{
    public class CustomerProfile : Profile
    {

        protected override void Configure()
        { 
            var mapper=CreateMap<Customer, CustomerDto>();
            mapper.ForMember(dto => dto.FullName, mc => mc.MapFrom(e => e.FirstName + " " + e.LastName));
            mapper.ForMember(dto => dto.Birthday, mc => mc.MapFrom(e => e.Birthday.ToString("yyyy.MM.dd")));
        }
    }

   
}
