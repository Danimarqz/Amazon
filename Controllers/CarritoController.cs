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
        public async Task<IActionResult> Index()
        {
            if (!CheckSession("User"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (CheckSession("Admin"))
            {
                var carrito = await _carritoRepository.GetCartAdmin();
                return View(carrito);
            } else
            {
                try
                {
                    int cartID = await _carritoRepository.GetCartID(Global.user.UsuarioID);
                    return RedirectToAction("Details", cartID);
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        // GET: CarritoController/Details/5
        public async Task<IActionResult> Details(int cartID)
        {
            var carrito = await _carritoRepository.GetAllCart(cartID);
            var jsonString = JsonSerializer.Serialize(carrito);
            HttpContext.Session.SetString("Carrito", jsonString);
            return View(carrito);
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
                _carritoRepository.EditDetallesProductoCarrito(detallesCarrito);
                var carrito = _carritoRepository.GetAllCart(detallesCarrito.CarritoID);
                return RedirectToAction("Details", carrito);
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Delete/5
        //public ActionResult DeleteProducto(int productoID)
        //{
          //  return View(productoID);
        //}

        // POST: CarritoController/Delete/5
        //Borrar producto del carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductoAsync(int productoID)
        {
            int cartID = await _carritoRepository.GetCartID(Global.user.UsuarioID);
            try
            {
                _carritoRepository.DeleteProducto(productoID, cartID);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Details", cartID);
            }
        }
        protected bool CheckSession(string key)
        {
            return HttpContext.Session.Keys.Contains(key);
        }
    }
}
