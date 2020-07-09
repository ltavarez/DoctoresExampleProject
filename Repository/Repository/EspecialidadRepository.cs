using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.RepositoryBase;

namespace Repository.Repository
{
    public class EspecialidadRepository : RepositoryBase<Especialidad, ConsultorioMedicoContext>
    {

        public EspecialidadRepository(ConsultorioMedicoContext context) : base(context)
        {

        }

        public async Task<bool> AnyEspecialidad(int id)
        {
            var list = await GetAll();
            var isAny = list.AsQueryable().Any(x => x.Id == id);
            return isAny;
        } 



    }
}
