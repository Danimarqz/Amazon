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
        private readonly IProductosRepository _productsRepository;
        public CarritoController(ICarritoRepository carritoRepository, IProductosRepository productosRepository)
        {
            _carritoRepository = carritoRepository;
            _productsRepository = productosRepository;
        }

        // GET: CarritoController
        public ActionResult Index()
        {
            if (!CheckSession("User"))
            {
                return RedirectToAction("Index", "Home");
            }
                var carrito = _carritoRepository.GetCart(Global.user.UsuarioID);
                return View(carrito);
        }

        // GET: CarritoController/Details/5
        public async Task<IActionResult> Details(int cartID)
        {
            var carrito = await _carritoRepository.GetAllCart(id);
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
        public ActionResult Create(DetallesCarrito detallesCarrito)
        {
            DateTime now = DateTime.Now;
            Carrito cart = new Carrito();   
            cart.CarritoID = detallesCarrito.CarritoID;
            cart.UsuarioID = Global.user.UsuarioID;
            cart.FechaCarrito = now;
            cart.totalVenta = detallesCarrito.PrecioTotal;

            try
            {
                _carritoRepository.Add(cart);
                _carritoRepository.AddDetalles(detallesCarrito);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Edit/5
        public async Task<IActionResult> Edit(int cartID)
        {
            var detallesCarrito = await _carritoRepository.GetAllCart(cartID);
            return detallesCarrito == null ? NotFound() : View(detallesCarrito);
        }

        // POST: CarritoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetallesCarrito detallesCarrito)
        {
            try
            {
                _carritoRepository.EditDetallesCarrito(detallesCarrito);
                var carrito = _carritoRepository.GetAllCart(detallesCarrito.CarritoID);
                return RedirectToAction("Details", carrito);
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
        protected bool CheckSession(string key)
        {
            return HttpContext.Session.Keys.Contains(key);
        }
    }
}
