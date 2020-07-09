using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;

namespace Repository.Repository
{
    public class DoctorEspecialidadRepository : RepositoryBase<DoctorEspecialidad, ConsultorioMedicoContext>
    {

        public DoctorEspecialidadRepository(ConsultorioMedicoContext context) : base(context)
        {

        }

        public async Task<bool> AddDoctorEspecialidades(List<int> especialidadIds,int doctorId)
        {
            try
            {
                foreach (var especialidadId in especialidadIds)
                {
                    var doctorEspecilidad = new DoctorEspecialidad
                    {
                        IdDoctor = doctorId,
                        IdEspecialidad = especialidadId
                    };

                    await base.Add(doctorEspecilidad);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                 return false;
            }
          
        }

        public async Task<bool> UpdateDoctorEspecialidades(List<int> especialidadIds, int doctorId)
        {
            try
            {
                var especilidadesDoctor = await _context.DoctorEspecialidad.Where(w => w.IdDoctor == doctorId).ToListAsync();

                foreach (var item in especilidadesDoctor)
                {
                    base._context.Remove(item);
                }
                await _context.SaveChangesAsync();

                await AddDoctorEspecialidades(especialidadIds, doctorId);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<int>> GetDoctorEspecialidadIds(int idDoctor)
        {
            return await _context.DoctorEspecialidad.Where(c=> c.IdDoctor == idDoctor).Select(x => x.IdEspecialidad).ToListAsync();
        }

    }
}
