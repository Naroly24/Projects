using Microsoft.AspNetCore.Mvc;
using AppCRUD.Models;
using AppCRUD.Data;
using Microsoft.EntityFrameworkCore;

namespace AppCRUD.Controllers
{
    public class EventoController : Controller
    {
        private readonly AppDBContext _context;

        public EventoController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lista()
        {
            var eventos = await _context.Eventos.ToListAsync();
            return View(eventos);
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo([Bind("IdEvento,Nombre,Descripcion,Fecha,Ubicacion,CapacidadMaxima")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }
            return View(evento);
        }
        public async Task<IActionResult> Editar(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdEvento,Nombre,Descripcion,Fecha,Ubicacion,CapacidadMaxima")] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.IdEvento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Lista));
            }
            return View(evento);
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.IdEvento == id);
        }

        // Nueva acción para eliminar un evento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Lista));
        }
    }
}
