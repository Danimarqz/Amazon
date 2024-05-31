using Amazon.Models;
using Amazon.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Amazon.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoRepository _carritoRepository;
        public CarritoController(ICarritoRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        // GET: CarritoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarritoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var carrito = await _carritoRepository.GetCart(id);
            var jsonString = JsonSerializer.Serialize(carrito);
            HttpContext.Session.SetString("Carrito", jsonString);
            return View(carrito);
        }

        // GET: CarritoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carrito cart)
        {
            try
            {
                _carritoRepository.Add(cart);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarritoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Carrito cart)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarritoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
