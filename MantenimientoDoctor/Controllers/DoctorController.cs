using Database.Model;
using MantenimientoDoctor.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MantenimientoDoctor.Controllers
{
    public class DoctorController : Controller
    {

        private readonly ConsultorioMedicoContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper _mapper;

        public DoctorController(ConsultorioMedicoContext context, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var userEntity = await _context.Usuario.FirstOrDefaultAsync(c => c.NombreUsuario == user);

            var idsUserDoctor = new List<int?>();

            if (userEntity != null)
            {
                idsUserDoctor = await _context.UsuarioDoctor.Where(c => c.UsuarioId == userEntity.Id).Select(s => s.DoctorId).ToListAsync();
            }


            var listadoDoctores = new List<Doctor>();

            if (idsUserDoctor.Count != 0)
            {
                listadoDoctores = await _context.Doctor.Where(s=> idsUserDoctor.Contains(s.Id)).ToListAsync();
            }

            List<DoctorViewModel> vms = new List<DoctorViewModel>();

            listadoDoctores.ForEach(item =>
            {
                var vm = _mapper.Map<DoctorViewModel>(item);
                vms.Add(vm);
            });

            return View(vms);
        }

        //GET
        public async Task<IActionResult> Create()
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var especilidadEntity = await _context.Especialidad.ToListAsync();

            List<EspecialidadViewModel> listEspecialidadesVm = new List<EspecialidadViewModel>();

            especilidadEntity.ForEach(item =>
            {
                var vm = _mapper.Map<EspecialidadViewModel>(item);
                listEspecialidadesVm.Add(vm);
            });

            ViewBag.Especialidades = listEspecialidadesVm;

            return View();
        }

        //GET
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

            var doctor = await _context.Doctor.FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<DoctorViewModel>(doctor);

            var especilidadEntity = await _context.Especialidad.ToListAsync();

            List<EspecialidadViewModel> listEspecialidadesVm = new List<EspecialidadViewModel>();

            especilidadEntity.ForEach(item =>
            {
                var vmEspecialidad = _mapper.Map<EspecialidadViewModel>(item);
                listEspecialidadesVm.Add(vmEspecialidad);
            });

            ViewBag.Especialidades = listEspecialidadesVm;

            var listEspecialidadIds = await _context.DoctorEspecialidad.Select(s => s.IdEspecialidad).ToListAsync();

            vm.EspecialidadIds = listEspecialidadIds;


            return View(vm);
        }

        //GET
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

            var doctor = await _context.Doctor.FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<DoctorViewModel>(doctor);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorViewModel vm)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (ModelState.IsValid)
            {

                string uniqueName = null;

                if (vm.Photo != null)
                {

                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/Doctor");

                    uniqueName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;

                    var filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null)
                    {
                        var stream = new FileStream(filePath, mode: FileMode.Create);
                        vm.Photo.CopyTo(stream);
                        stream.Flush();
                        stream.Close();
                    }

                }

                var doctorEntity = _mapper.Map<Doctor>(vm);
                doctorEntity.ProfilePhoto = uniqueName;

                _context.Add(doctorEntity);
                await _context.SaveChangesAsync();

                foreach (var especialidadId in vm.EspecialidadIds)
                {
                    var doctorEspecilidad = new DoctorEspecialidad
                    {
                        IdDoctor = doctorEntity.Id,
                        IdEspecialidad = especialidadId
                    };

                    _context.Add(doctorEspecilidad);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? Id, DoctorViewModel vm)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (Id != vm.Id)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FirstOrDefaultAsync(w => w.Id == vm.Id);

            if (doctor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueName = null;

                if (vm.Photo != null)
                {

                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/Doctor");

                    uniqueName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;

                    var filePath = Path.Combine(folderPath, uniqueName);


                    if (!string.IsNullOrEmpty(doctor.ProfilePhoto))
                    {

                        var filePathDelete = Path.Combine(folderPath, doctor.ProfilePhoto);

                        if (System.IO.File.Exists(filePathDelete))
                        {
                            var fileInfo = new System.IO.FileInfo(filePathDelete);
                            fileInfo.Delete();
                        }
                    }

                    if (filePath != null)
                    {
                        var stream = new FileStream(filePath, mode: FileMode.Create);
                        vm.Photo.CopyTo(stream);
                        stream.Flush();
                        stream.Close();

                    }

                }


                doctor.Id = vm.Id;
                doctor.Nombre = vm.Nombre;
                doctor.Correo = vm.Correo;
                doctor.CodigoPostal = vm.CodigoPostal;
                doctor.FechaNacimiento = vm.FechaNacimiento;
                doctor.Telefono = vm.Telefono;
                doctor.ProfilePhoto = uniqueName;

                _context.Update(doctor);
                await _context.SaveChangesAsync();

                var especilidadesDoctor = await _context.DoctorEspecialidad.Where(w => w.IdDoctor == doctor.Id).ToListAsync();

                foreach (var item in especilidadesDoctor)
                {
                    _context.DoctorEspecialidad.Remove(item);
                }

                await _context.SaveChangesAsync();

                foreach (var especialidadId in vm.EspecialidadIds)
                {
                    var doctorEspecilidad = new DoctorEspecialidad
                    {
                        IdDoctor = doctor.Id,
                        IdEspecialidad = especialidadId
                    };

                    _context.Add(doctorEspecilidad);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? Id)
        {
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var doctor = await _context.Doctor.FirstOrDefaultAsync(x => x.Id == Id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}