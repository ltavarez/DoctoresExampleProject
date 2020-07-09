using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;

namespace Repository.Repository
{
    public class DoctorRepository : RepositoryBase<Doctor, ConsultorioMedicoContext>
    {
        private readonly UsuarioDoctorRepository _usuarioDoctorRepository;
        public DoctorRepository(ConsultorioMedicoContext context) : base(context)
        {
            _usuarioDoctorRepository = new UsuarioDoctorRepository(context);
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

        public async Task<bool> DeleteDoctor(int doctorid)
        {

            try
            {
                await _usuarioDoctorRepository.DeleteDoctorUsuarioId(doctorid);
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
