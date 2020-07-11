using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SessionRepository _repository;
        public TokenController(SessionRepository repository)
        {
            _repository = repository;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _repository.AddToken();
            return NoContent();
        }

    }
}