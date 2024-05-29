using Amazon.Models;
using Amazon.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;
        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        // GET: UsuariosController
        public async Task<IActionResult> Index()
        {
            var Users = await _usuariosRepository.GetAll();
            return View(Users);
        }

        // GET: UsuariosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var UsersDetail = await _usuariosRepository.GetAll();
            return View("Details", UsersDetail);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuarios u)
        {
            if (ModelState.IsValid)
            {
                _usuariosRepository.Add(u);
                return RedirectToAction("Index"); // Redirect to Index after successful creation
            }

            return View(u); // Return the view with the model to display validation errors
        }

        // GET: UsuariosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _usuariosRepository.GetById(id);
            return user == null ? NotFound() : View(user);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios u)
        {
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
            var user = await _usuariosRepository.GetById(id);
            return user == null ? NotFound() : View(user);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _usuariosRepository.GetById(id);
            
            if (user != null)
            {
                _usuariosRepository.Delete(user);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
