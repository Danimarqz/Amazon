using Amazon.Models;
using Amazon.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Controllers
{
    public class VentaController : Controller
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaController(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }
        public ActionResult Index()
        {
            if (!CheckSession("User"))
            {
                return RedirectToAction("Home", "Index");
            }
            if (CheckSession("Admin"))
            {
                var ventas = _ventaRepository.GetVentasAdmin();
                return View(ventas);
            } else
            {
                return RedirectToAction("Details");
            }
        }
        public async Task<IActionResult> Details(int? ventaID)
        {
            int venta = ventaID ?? await _ventaRepository.GetVentaID(Global.user.UsuarioID);
            var ventaDetalles = await _ventaRepository.GetDetallesVenta(venta);
            return View(ventaDetalles);
        }
        public async Task<IActionResult> Edit(int ventaID)
        {
            if (!CheckSession("Admin"))
            {
                return RedirectToAction("Home", "Index");
            }
            var detallesVenta = await _ventaRepository.GetDetallesVenta(ventaID);
            return detallesVenta == null ? NotFound() : View(detallesVenta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetallesVenta detallesVenta)
        {
            try
            {
                _ventaRepository.EditVenta(detallesVenta);
                var venta = _ventaRepository.GetDetallesVenta(detallesVenta.VentaID);
                return RedirectToAction("Details", venta);
            } catch
            {
                return View(detallesVenta.VentaID);
            }
        }
        public ActionResult Delete(int ventaID)
        {
            return View(ventaID);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int productoID)
        {
            int ventaID = await _ventaRepository.GetVentaID(Global.user.UsuarioID);
            try
            {
                await _ventaRepository.RmVenta(productoID, Global.user.UsuarioID);
                return RedirectToAction("Index");
            } catch{
                return RedirectToAction("Details", ventaID);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVenta(int productID)
        {
            int userID = Global.user.UsuarioID;
            await _ventaRepository.AddVenta(productID, userID);
            return RedirectToAction("Index");
        }
        protected bool CheckSession(string key)
        {
            return HttpContext.Session.Keys.Contains(key);
        }
    }
}
