using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.Model;
using MantenimientoDoctor.ViewModels;
using Microsoft.AspNetCore.Http;

namespace MantenimientoDoctor.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly ConsultorioMedicoContext _context;

        public EspecialidadController(ConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Especialidad
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var listEntity = await _context.Especialidad.ToListAsync();

            List<EspecialidadViewModel> vms = new List<EspecialidadViewModel>();

            listEntity.ForEach(item =>
            {
                vms.Add(new EspecialidadViewModel
                {
                    Id = item.Id,
                    Nombre = item.Nombre
                });
            });

            return View(vms);
        }

        // GET: Especialidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

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

        // GET: Especialidad/Create
        public IActionResult Create()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            return View();
        }

        // POST: Especialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspecialidadViewModel vm)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (ModelState.IsValid)
            {

                var entity = new Especialidad
                {
                    Id = vm.Id,
                    Nombre = vm.Nombre
                };

                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Especialidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var especialidad = await _context.Especialidad.FindAsync(id);
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

        // POST: Especialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EspecialidadViewModel vm)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

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
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

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
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

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
