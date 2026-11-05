using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AplicationDbContext _context;

        public ProductosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/MasVendido
        public async Task<IActionResult> MasVendido()
        {
            var productoTop = await _context.Productos
                .OrderByDescending(p => p.CantidadVendida)
                .FirstOrDefaultAsync();

            return View(productoTop);
        }

        // GET: Productos/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Productos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("CodigoProducto,Nombre,PrecioVenta,StockMinimo,StockActual")] Producto producto)
        {
            // Asegurar valor por defecto
            producto.CantidadVendida = 0;

            if (ModelState.IsValid)
            {
                // Verificar si el código ya existe
                bool existe = await _context.Productos
                    .AnyAsync(p => p.CodigoProducto == producto.CodigoProducto);

                if (existe)
                {
                    ModelState.AddModelError("CodigoProducto", "Ya existe un producto con este código.");
                    return View(producto);
                }

                try
                {
                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();
                    TempData["MensajeExito"] = "Producto agregado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Error típico de base de datos (tabla/columna incorrecta, conexión, etc.)
                    ModelState.AddModelError(string.Empty,
                        "Error al guardar en la base de datos. Verifica que la tabla 'Productos' exista y coincida con el modelo. Detalle: " +
                        (ex.InnerException?.Message ?? ex.Message));
                    return View(producto);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado: " + ex.Message);
                    return View(producto);
                }
            }

            // Si llega aquí, ModelState no es válido (mostrar errores de validación)
            return View(producto);
        }

        // GET: Productos/Editar/5
        public async Task<IActionResult> Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, [Bind("CodigoProducto,Nombre,PrecioVenta,StockMinimo,StockActual,CantidadVendida")] Producto producto)
        {
            if (id != producto.CodigoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    TempData["MensajeExito"] = "Producto actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.CodigoProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty,
                        "Error al actualizar en la base de datos: " + (ex.InnerException?.Message ?? ex.Message));
                    return View(producto);
                }
            }
            return View(producto);
        }

        // GET: Productos/Eliminar/5
        public async Task<IActionResult> Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(string id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Producto eliminado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
            return _context.Productos.Any(e => e.CodigoProducto == id);
        }
    }
}
