using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly DoctorRepository _repository;

        #region RestFul

        public DoctorController(DoctorRepository repository)
        {
            _repository = repository;
        }


        // Get api/Doctor
        /// <summary>
        /// Obtiene todos los doctores de la aplicacion
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<ActionResult<List<DoctorDto>>> Get()
        {

                var list = await _repository.GetAllDto();

                if (list.Count == 0)
                {
                    return NotFound();
                }

                return list;
        }

        // Get api/Doctor/{id}
        /// <summary>
        /// Obtiene todos los doctores de la aplicacion
        /// </summary>
        /// <param name="id"> Id del doctor </param>
        /// <returns> DoctorDto </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> Get(int id)
        {
            var doctor = await _repository.GetByIdDto(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;

        }

        [HttpPost]
        public async Task<ActionResult> Post(DoctorDtoUpdate dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _repository.Add(dto);

                if (response)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }
                
            }

            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,DoctorDtoUpdate dto)
        {

            var doctor = await _repository.GetById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _repository.Update(id, dto);

                if (response)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }
              
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var doctor = await _repository.GetById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var respose = await _repository.DeleteDoctor(id);

            if (respose)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }

            

        }


        #endregion

        #region "Custom Endpoint"

        [HttpGet]
        [Route("ByName")]
        public async Task<ActionResult<List<DoctorDto>>> GetDoctorByName(string name)
        {

            var list = await _repository.GetAllDtoByName(name);

            if (list.Count == 0)
            {
                return NotFound();
            }

            return list;
        }

        #endregion

    }
}