using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Database.Model;
using Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace MantenimientoDoctor.Controllers
{
    public class AccountController : Controller
    {

        private readonly ConsultorioMedicoContext _context;

        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AccountController(ConsultorioMedicoContext context,
            IEmailSender emailSender,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _emailSender = emailSender;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Doctor");
            }

            var user = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Doctor");
            }


            return View();
        }


        public async Task<IActionResult> AccessDenied()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (await _userManager.IsInRoleAsync(user, "doctor"))
            {
                return RedirectToAction("Index", "Doctor");
            }

            if (await _userManager.IsInRoleAsync(user, "especialista"))
            {
                return RedirectToAction("Index", "Especialidad");
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Doctor");
            }

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(vm.NombreUsuario, vm.Password, false, false);

                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(vm.NombreUsuario);

                    if (await _userManager.IsInRoleAsync(user, "doctor"))
                    {
                        return RedirectToAction("Index", "Doctor");
                    }

                    if (await _userManager.IsInRoleAsync(user, "especialista"))
                    {
                        return RedirectToAction("Index", "Especialidad");
                    }

                }

                ModelState.AddModelError("UserOrPasswordInvalid", "El usuario o la contraseña es invalido");

            }
            return View(vm);
        }

        public async Task<IActionResult> Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Doctor");
            }

            var roles = await _roleManager.Roles?.ToListAsync();

            var rolesVm = new List<RolViewModel>();

            roles.ForEach(c =>
            {
                rolesVm.Add(new RolViewModel
                {
                    Name = c.Name,
                    RoleName = c.NormalizedName
                });
            });



            var vm = new RegisterViewModel {Roles = rolesVm};


            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Doctor");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = vm.NombreUsuario, Email = vm.Correo };
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    var resultRol = await _userManager.AddToRoleAsync(user, vm.SelectedRol);

                    if (resultRol.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Doctor");
                    }
                   
                }

                AddErrors(result);
            }

            return View(vm);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


    }
}