using System;
using Database.Model;
using Repository.RepositoryBase;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class UsuarioDoctorRepository : RepositoryBase<UsuarioDoctor, ConsultorioMedicoContext>
    {

        public UsuarioDoctorRepository(ConsultorioMedicoContext context) : base(context)
        {

        }

        public async Task<List<int?>> GetDoctorUsuarioIds(string userId)
        {
            return await _context.UsuarioDoctor.Where(c => c.UsuarioId == userId).Select(s => s.DoctorId).ToListAsync();
        }

        public async Task<bool> DeleteDoctorUsuarioId(int doctorId)
        {

            try
            {
                var doctorUser = await base._context.UsuarioDoctor.Where(c => c.DoctorId == doctorId).ToListAsync();

                foreach (var item in doctorUser)
                {
                    _context.Remove(item);
                }

               await _context.SaveChangesAsync();


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
