using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Model;
using DTO;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<EspecialidadDto>> GetEspecialidadesDtoByIds(List<int> ids)
        {
            var list = await _context.Especialidad.Where(x => ids.Contains(x.Id)).ToListAsync();

            var listDto = new List<EspecialidadDto>();

            list.ForEach(x =>
            {
                var dto = Mapper.Map<EspecialidadDto>(x);

                listDto.Add(dto);
            });

            return listDto;
        }



    }
}
