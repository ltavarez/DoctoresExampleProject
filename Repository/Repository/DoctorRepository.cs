using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Model;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;

namespace Repository.Repository
{
    public class DoctorRepository : RepositoryBase<Doctor, ConsultorioMedicoContext>
    {
        private readonly UsuarioDoctorRepository _usuarioDoctorRepository;
        private readonly EspecialidadRepository _especialidadRepository;
        private readonly DoctorEspecialidadRepository _doctorEspecialidadRepository;
        public DoctorRepository(ConsultorioMedicoContext context) : base(context)
        {
            _usuarioDoctorRepository = new UsuarioDoctorRepository(context);
            _especialidadRepository = new EspecialidadRepository(context);
            _doctorEspecialidadRepository = new DoctorEspecialidadRepository(context);
        }

        public async Task<List<Doctor>> GetDoctorByUser(string userId)
        {
            var userDoctorListId = await _usuarioDoctorRepository.GetDoctorUsuarioIds(userId);

            var listadoDoctores = new List<Doctor>();

            if (userDoctorListId.Count != 0)
            {
                listadoDoctores = await _context.Doctor.Where(s => userDoctorListId.Contains(s.Id)).ToListAsync();
            }

            return listadoDoctores;
        }

        public async Task<DoctorDto> GetByIdDto(int id)
        {
            var doctor = await _context.Doctor.Include(x => x.DoctorEspecialidad).FirstOrDefaultAsync(x=> x.Id == id);

            if (doctor == null)
            {
                return null;
            }
            else
            {
                var dto = Mapper.Map<DoctorDto>(doctor);

                var listIds = doctor.DoctorEspecialidad.Select(s => s.IdEspecialidad).ToList();

                var listEspecialidadDtos = await _especialidadRepository.GetEspecialidadesDtoByIds(listIds);

                dto.Especialidades = listEspecialidadDtos;

                return dto;
            }

             
        }



        public async Task<List<DoctorDto>> GetAllDto()
        {
            var list = await _context.Doctor.Include(x => x.DoctorEspecialidad).ToListAsync();

            var listDto = new List<DoctorDto>();

            foreach (var item in list)
            {
                var dto =  Mapper.Map<DoctorDto>(item);

                var listIds = item.DoctorEspecialidad.Select(s => s.IdEspecialidad).ToList();

                var listEspecialidadDtos = await _especialidadRepository.GetEspecialidadesDtoByIds(listIds);

                dto.Especialidades = listEspecialidadDtos;

                listDto.Add(dto);
            }
            return listDto;
        }

        public async Task<List<DoctorDto>> GetAllDtoByName(string name)
        {
            var list = await _context.Doctor.Include(x => x.DoctorEspecialidad).Where(x=> x.Nombre.Contains(name)).ToListAsync();

            var listDto = new List<DoctorDto>();

            foreach (var item in list)
            {
                var dto = Mapper.Map<DoctorDto>(item);

                var listIds = item.DoctorEspecialidad.Select(s => s.IdEspecialidad).ToList();

                var listEspecialidadDtos = await _especialidadRepository.GetEspecialidadesDtoByIds(listIds);

                dto.Especialidades = listEspecialidadDtos;

                listDto.Add(dto);
            }
            return listDto;
        }

        public async Task<bool> Update(int id , DoctorDtoUpdate dto)
        {
            try
            {
                var doctor = await GetById(id);

                doctor.CodigoPostal = dto.CodigoPostal;
                doctor.Correo = dto.Correo;
                doctor.Telefono = dto.Telefono;
                doctor.Nombre = dto.Nombre;
                doctor.FechaNacimiento = dto.FechaNacimiento;

                await Update(doctor);

                await _doctorEspecialidadRepository.UpdateDoctorEspecialidades(dto.EspecialidadIds, doctor.Id);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> Add(DoctorDtoUpdate dto)
        {
            try
            {
               var doctor = Mapper.Map<Doctor>(dto);

               await Add(doctor);

                await _doctorEspecialidadRepository.AddDoctorEspecialidades(dto.EspecialidadIds, doctor.Id);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> DeleteDoctor(int doctorid)
        {

            try
            {
                await _usuarioDoctorRepository.DeleteDoctorUsuarioId(doctorid);
                await _doctorEspecialidadRepository.DeleteDoctorEspecialidadId(doctorid);
                await base.Delete(doctorid);
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return false;
            }

        }

    }
}
