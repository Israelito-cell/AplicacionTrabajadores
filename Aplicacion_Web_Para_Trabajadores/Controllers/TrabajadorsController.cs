using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aplicacion_Web_Para_Trabajadores.Data;
using Aplicacion_Web_Para_Trabajadores.Models;

namespace Aplicacion_Web_Para_Trabajadores.Controllers
{
    public class TrabajadorsController : Controller
    {
        private readonly AppDbContext _context;

        public TrabajadorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Trabajadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trabajadores.ToListAsync());
        }

        // GET: Trabajadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trabajadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajador trabajador, IFormFile fotoArchivo)
        {
            if (ModelState.IsValid)
            {
                // Si el usuario sube una foto
                if (fotoArchivo != null && fotoArchivo.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await fotoArchivo.CopyToAsync(ms);
                        trabajador.Foto = ms.ToArray(); // Guarda la imagen como bytes
                    }
                }

                trabajador.FechaCreacion = DateTime.UtcNow; // Registra la fecha de creación automáticamente

                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(trabajador);
        }


        // GET: Trabajadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            return View(trabajador);
        }

        // POST: Trabajadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("Id,Nombres,Apellidos,TipoDocumento,NumeroDocumento,Sexo,FechaNacimiento,Direccion,FechaCreacion")] Trabajador trabajador,
    IFormFile nuevaFoto)
        {
            // Verifica que el id sea válido
            if (id != trabajador.Id)
                return NotFound();

            // Busca el registro original en la BD
            var trabajadorExistente = await _context.Trabajadores.FindAsync(id);
            if (trabajadorExistente == null)
                return NotFound();

            // Actualiza los datos básicos
            trabajadorExistente.Nombres = trabajador.Nombres;
            trabajadorExistente.Apellidos = trabajador.Apellidos;
            trabajadorExistente.TipoDocumento = trabajador.TipoDocumento;
            trabajadorExistente.NumeroDocumento = trabajador.NumeroDocumento;
            trabajadorExistente.Sexo = trabajador.Sexo;
            trabajadorExistente.FechaNacimiento = trabajador.FechaNacimiento;
            trabajadorExistente.Direccion = trabajador.Direccion;

            // Actualiza foto si se sube una nueva
            if (nuevaFoto != null && nuevaFoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await nuevaFoto.CopyToAsync(memoryStream);
                    trabajadorExistente.Foto = memoryStream.ToArray();
                }
            }

            try
            {
                // Guarda cambios
                _context.Update(trabajadorExistente);
                await _context.SaveChangesAsync();

                // Muestra mensaje temporal en la siguiente vista
                TempData["Mensaje"] = "✅ Los datos del trabajador se actualizaron correctamente.";

                // Redirige al Index (listar)
                return RedirectToAction("Index", "Trabajadors");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Trabajadores.Any(e => e.Id == trabajador.Id))
                    return NotFound();
                else
                    throw;
            }
        }



        // GET: Trabajadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // POST: Trabajadors/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = " El registro fue eliminado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }
            private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}
