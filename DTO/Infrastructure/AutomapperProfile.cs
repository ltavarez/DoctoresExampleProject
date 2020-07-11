using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Database.Model;

namespace DTO.Infrastructure
{
   public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            DoctorConfiguration();
            EspecialidadConfiguration();

        }

        private void DoctorConfiguration()
        {
            CreateMap<DoctorDto, Doctor>().ReverseMap()
                .ForMember(dest => dest.Especialidades, opt => opt.Ignore());

            CreateMap<DoctorDtoUpdate, Doctor>().ReverseMap()
                .ForMember(dest => dest.EspecialidadIds, opt => opt.Ignore());

        }

        private void EspecialidadConfiguration()
        {
            CreateMap<EspecialidadDto, Especialidad>().ReverseMap();

        }

    }
}
