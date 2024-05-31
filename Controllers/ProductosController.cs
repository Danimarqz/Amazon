using Amazon.Models;
using Amazon.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductosRepository _productosRepository;
        public ProductosController(IProductosRepository productosRepository)
        {
            _productosRepository = productosRepository;
        }

        // GET: ProductosController
        public async Task<IActionResult> IndexAsync()
        {
            var productos = await _productosRepository.GetAll();
            return View(productos);
        }

        // GET: ProductosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _productosRepository.GetById(id);
            return View(producto);
        }

        // GET: ProductosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Productos productos)
        {
            try
            {
                _productosRepository.Create(productos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductosController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var producto = await _productosRepository.GetById(id);
            return producto == null ? NotFound() : View(producto);
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Productos productos)
        {
            if (ModelState.IsValid)
            {
                _productosRepository.Update(productos);
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: ProductosController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var producto = await _productosRepository.GetById(id);
            return producto == null ? NotFound() : View(producto);
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productosRepository.GetById(id);
            try
            {
                _productosRepository.Delete(producto);
                return RedirectToAction("Index");
            } catch(InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Delete", producto);
            }
        }
    }
}
