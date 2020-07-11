using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;

namespace Repository.Repository
{
    public class SessionRepository : RepositoryBase<Session, ConsultorioMedicoContext>
    {

        public SessionRepository(ConsultorioMedicoContext context) : base(context)
        {

        }

        public async Task AddToken()
        {

            var token = new Session
            {
                Token = Guid.NewGuid().ToString(),
                UserId = "23e05d36-2d3c-4512-8cac-d3499843e2a9",
                Expirable = true,
                DateCreated = DateTime.Now,
                LastActivity = DateTime.Now
            };

            await Add(token);
        }

        public Session UpdateNotAsync(Session entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public Session GetToken(string token)
        {
            var tokenSession = _context.Sessions.FirstOrDefault(x => x.Token == token);

            return tokenSession;
        }

      

    }
}
