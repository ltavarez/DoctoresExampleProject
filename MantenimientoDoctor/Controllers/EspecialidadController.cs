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
using ViewModels;

namespace MantenimientoDoctor.Controllers
{
    [Authorize(Roles = "especialista,doctor")]
    public class EspecialidadController : Controller
    {
        private readonly ConsultorioMedicoContext _context;
        private readonly IMapper _mapper;

        public EspecialidadController(ConsultorioMedicoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Especialidad
        public async Task<IActionResult> Index()
        {
           var listEntity = await _context.Especialidad.ToListAsync();

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

            var especialidad = await _context.Especialidad
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(entity);
                await _context.SaveChangesAsync();
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

            var especialidad = await _context.Especialidad.FindAsync(id);
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

                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadExists(vm.Id))
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

            var especialidad = await _context.Especialidad
                .FirstOrDefaultAsync(m => m.Id == id);
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
           var especialidad = await _context.Especialidad.FindAsync(id);
            _context.Especialidad.Remove(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadExists(int id)
        {
            return _context.Especialidad.Any(e => e.Id == id);
        }
    }
}
