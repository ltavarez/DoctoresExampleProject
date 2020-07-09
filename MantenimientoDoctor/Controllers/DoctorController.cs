using AutoMapper;
using Database.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ViewModels;

namespace MantenimientoDoctor.Controllers
{

    [Authorize(Roles = "doctor")]
    public class DoctorController : Controller
    {


        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DoctorRepository _repository;
        private readonly EspecialidadRepository _repositoryEspecialidad;
        private readonly DoctorEspecialidadRepository _repositoryDoctorEspecialidad;


        public DoctorController(DoctorRepository repository, EspecialidadRepository repositoryEspecialidad, DoctorEspecialidadRepository repositoryDoctorEspecialidad, IHostingEnvironment hostingEnvironment, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _repositoryEspecialidad = repositoryEspecialidad;
            _repositoryDoctorEspecialidad = repositoryDoctorEspecialidad;
           _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var userEntity = await _userManager.FindByNameAsync(User.Identity.Name);

            var listadoDoctores = new List<Doctor>();

            if (userEntity != null)
            {
                listadoDoctores = await _repository.GetDoctorByUser(userEntity.Id);
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
            var especilidadEntity = await _repositoryEspecialidad.GetAll();

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

            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _repository.GetById(id.Value);

            if (doctor == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<DoctorViewModel>(doctor);

            var especilidadEntity = await _repositoryEspecialidad.GetAll();

            List<EspecialidadViewModel> listEspecialidadesVm = new List<EspecialidadViewModel>();

            especilidadEntity.ForEach(item =>
            {
                var vmEspecialidad = _mapper.Map<EspecialidadViewModel>(item);
                listEspecialidadesVm.Add(vmEspecialidad);
            });

            ViewBag.Especialidades = listEspecialidadesVm;

            var listEspecialidadIds = await _repositoryDoctorEspecialidad.GetDoctorEspecialidadIds(doctor.Id);

            vm.EspecialidadIds = listEspecialidadIds;


            return View(vm);
        }

        //GET
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _repository.GetById(id.Value);

            if (doctor == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<DoctorViewModel>(doctor);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorViewModel vm, IFormFile Photo)
        {


            if (ModelState.IsValid)
            {

                string uniqueName = null;

                if (Photo != null)
                {

                    var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Doctor");

                    uniqueName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

                    var filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null)
                    {
                        var stream = new FileStream(filePath, mode: FileMode.Create);
                        Photo.CopyTo(stream);
                        stream.Flush();
                        stream.Close();
                    }

                }

                var doctorEntity = _mapper.Map<Doctor>(vm);
                doctorEntity.ProfilePhoto = uniqueName;

                await _repository.Add(doctorEntity);
                await _repositoryDoctorEspecialidad.AddDoctorEspecialidades(vm.EspecialidadIds, doctorEntity.Id);

                return RedirectToAction("Index");
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? Id, DoctorViewModel vm, IFormFile Photo)
        {

            if (Id != vm.Id)
            {
                return NotFound();
            }

            var doctor = await _repository.GetById(vm.Id);

            if (doctor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueName = null;

                if (Photo != null)
                {

                    var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Doctor");

                    uniqueName = Guid.NewGuid().ToString() + "_" + Photo.FileName;

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
                        Photo.CopyTo(stream);
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

                await _repository.Update(doctor);

                await _repositoryDoctorEspecialidad.UpdateDoctorEspecialidades(vm.EspecialidadIds, doctor.Id);

                return RedirectToAction("Index");
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? Id)
        {
            //var user = HttpContext.Session.GetString("UserName");
            //if (string.IsNullOrEmpty(user))
            //{
            //    return RedirectToAction("AccesoDenegado", "Home");
            //}

            if (Id == null)
            {
                return NotFound();
            }

            await _repository.DeleteDoctor(Id.Value);
            return RedirectToAction("Index");
        }



    }
}