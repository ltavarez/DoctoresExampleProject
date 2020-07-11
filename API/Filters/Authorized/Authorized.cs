using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Repository;

namespace API.Filters.Authorized
{
    public class Authorized : ActionFilterAttribute
    {
        private readonly SessionRepository _repository;


        public Authorized()
        {
            var context = new ConsultorioMedicoContext();
            _repository = new SessionRepository(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers.FirstOrDefault(c => c.Key == "Auth");

            if (token.Key == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            var session = GetToken(token.Value);

            if (session == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            if (session.Expirable)
            {
                if (session.IsExpired)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                    return;
                }

                var minutes = Math.Round((DateTime.Now - session.LastActivity).TotalMinutes);

                if (minutes > 5)
                {
                    session.IsExpired = true;
                    _repository.UpdateNotAsync(session);
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                    return;

                }

            }

            session.LastActivity = DateTime.Now;
            _repository.UpdateNotAsync(session);

            context.HttpContext.Request.Headers.Add("Applicant", session.Token);
            context.HttpContext.Request.Headers.Add("LoggedUser", session.UserId);

            return;
        }

        private Session GetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var sesion = _repository.GetToken(token);

            return sesion;
        }

    }
}
