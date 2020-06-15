using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MantenimientoDoctor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Database.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoDoctor.Controllers
{
    public class UserController : Controller
    {

        private readonly ConsultorioMedicoContext _context;

        public UserController(ConsultorioMedicoContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {

            var user = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Doctor");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            var userSession = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userSession))
            {
                return RedirectToAction("Index", "Doctor");
            }

            if (ModelState.IsValid)
            {

                var passwordEncryted = PasswordEncryption(vm.Password);

                var user = await _context.Usuario.FirstOrDefaultAsync(c =>
                    c.NombreUsuario == vm.NombreUsuario && c.Password == passwordEncryted);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserName", vm.NombreUsuario);
                    return RedirectToAction("Index","Doctor");
                }
                else
                {
                    ModelState.AddModelError("UserOrPasswordInvalid","El usuario o la contraseña es invalido");
                }

            }

           return View(vm);
        }

        public IActionResult Register()
        {
            var userSession = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userSession))
            {
                return RedirectToAction("Index", "Doctor");
            }

            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var userSession = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userSession))
            {
                return RedirectToAction("Index", "Doctor");
            }

            if (ModelState.IsValid)
            {
                var usuarioEntity = new Usuario
                {
                    Nombre = vm.Nombre,
                    NombreUsuario = vm.NombreUsuario,
                    Correo = vm.Correo,
                    Password = PasswordEncryption(vm.Password)
                };

                var user = await _context.Usuario.FirstOrDefaultAsync(c => c.NombreUsuario == vm.NombreUsuario);

                if (user != null)
                {
                    ModelState.AddModelError("UserExits", "Este usuario ya esta registrado");
                    return View(vm);
                }


                _context.Add(usuarioEntity);
                await _context.SaveChangesAsync();


                return RedirectToAction("Login");
            }

            return View(vm);
        }

        private string PasswordEncryption(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                foreach (Byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }

           
        }
    }
}