using Amazon.Models;
using Amazon.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace Amazon.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ICarritoRepository _carritoRepository;
        public UsuariosController(IUsuariosRepository usuariosRepository, ICarritoRepository carritoRepository)
        {
            _usuariosRepository = usuariosRepository;
            _carritoRepository = carritoRepository;
        }

        // GET: UsuariosController
        public async Task<IActionResult> Index()
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var Users = await _usuariosRepository.GetAll();
            return View(Users);

        }

        // GET: UsuariosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var UsersDetail = await _usuariosRepository.GetById(id);
            return View("Details", UsersDetail);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            if (!CheckSession("Admin"))
            {
                return View("Create");
            } else
            {
                return View("CreateAdmin");
            }
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Usuarios u)
        {
            await _usuariosRepository.Add(u);
            await DoLogin(u);
            return RedirectToAction("Index");
        }

        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _usuariosRepository.GetById(id);
            return user == null ? NotFound() : View(user);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios u)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _usuariosRepository.Update(u);
                return RedirectToAction("Index");
            }
            return View(u);
        }

        // GET: UsuariosController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _usuariosRepository.GetById(id);
            return user == null ? NotFound() : View(user);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _usuariosRepository.GetById(id);
            if (user != null)
            {
                try
                {
                    await _usuariosRepository.Delete(id);
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index");
                }
            }
                return View("Delete", id);
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        //POST: Login
        public async Task<IActionResult> DoLogin(Usuarios u)
        {
            try {
                var user = await _usuariosRepository.GetByEmail(u.Email);
            if (user.Contrasena == u.Contrasena)
            {
                string jsonString = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString("User", jsonString);
                Global.user = user;
                Global.carritoCantidad = await _carritoRepository.ProductosCarrito(Global.user.UsuarioID);
                if (user.userType == "administrador")
                {
                    HttpContext.Session.SetString("Admin", jsonString);
                }
                return RedirectToAction("Index");
            }
            } catch {
                return View("Login");
            }
            return View("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Global.user = null;
            return RedirectToAction("Login", "Usuarios");
        }
        private bool CheckSession(string key)
        {
            return HttpContext.Session.Keys.Contains(key);
        }
    }
}
