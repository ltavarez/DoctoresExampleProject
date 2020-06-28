using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Model;
using ViewModels;

namespace MantenimientoDoctor.Infraestructure.AutoMapper
{
    public class AutomapperConfiguration : Profile
    {

        public AutomapperConfiguration()
        {
            ConfigureDoctor();
            ConfigureEspecialidades();
            ConfigureUsuario();
        }

        private void ConfigureDoctor()
        {
            CreateMap<DoctorViewModel, Doctor>().ReverseMap().ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.EspecialidadIds, opt => opt.Ignore())
                .ForMember(dest => dest.Especialidades, opt => opt.Ignore());
        }

        private void ConfigureEspecialidades()
        {
            CreateMap<EspecialidadViewModel, Especialidad>().ReverseMap();
        }

        private void ConfigureUsuario()
        {
            CreateMap<RegisterViewModel, Usuario>().ReverseMap()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.SelectedRol, opt => opt.Ignore());
        }
    }
}
