using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Repository.Repository;
using ViewModels;

namespace MantenimientoDoctor.Controllers
{
    [Authorize(Roles = "especialista,doctor")]
    public class EspecialidadController : Controller
    {
      private readonly IMapper _mapper;
        private readonly EspecialidadRepository _repository;

        public EspecialidadController( IMapper mapper, EspecialidadRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: Especialidad
        public async Task<IActionResult> Index()
        {
            var listEntity = await _repository.GetAll();

            List<EspecialidadViewModel> vms = new List<EspecialidadViewModel>();

            listEntity.ForEach(item =>
            {
                var vm = _mapper.Map<EspecialidadViewModel>(item);
                vms.Add(vm);
            });

            return View(vms);
        }

      

        // GET: Especialidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           if (id == null)
            {
                return NotFound();
            }

            var especialidad = await _repository.GetById(id.Value);
            if (especialidad == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<EspecialidadViewModel>(especialidad);

            return View(vm);
        }

        // GET: Especialidad/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Especialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspecialidadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Especialidad>(vm);
                await _repository.Add(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Especialidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidad = await _repository.GetById(id.Value);
            if (especialidad == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<EspecialidadViewModel>(especialidad);
            return View(vm);
        }

        // POST: Especialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EspecialidadViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Especialidad
                    {
                        Id = vm.Id,
                        Nombre = vm.Nombre
                    };

                    await _repository.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {

                    var isExists = await EspecialidadExists(vm.Id);
                    if (!isExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Especialidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var especialidad = await _repository.GetById(id.Value);
            if (especialidad == null)
            {
                return NotFound();
            }

            var vm = new EspecialidadViewModel
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre
            };

            return View(vm);
        }

        // POST: Especialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EspecialidadExists(int id)
        {
            return await _repository.AnyEspecialidad(id);
        }
    }
}
