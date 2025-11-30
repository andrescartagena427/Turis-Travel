using Microsoft.AspNetCore.Mvc;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class DestinosController : Controller
    {
        // LISTA TEMPORAL (tu "base de datos" mientras no uses EF)
        private static List<Destino> destinos = new List<Destino>
        {
            new Destino {
                Id = 1,
                Nombre = "Montañas Andes",
                Ciudad = "Medellín",
                Pais = "Colombia",
                Descripcion = "Paisajes impresionantes para caminatas.",
                Precio = 690000,
                ImagenUrl = "/img/destinos/Bali - Indonesia.jpg"
            },
            new Destino {
                Id = 2,
                Nombre = "Hotel Caribe Deluxe",
                Ciudad = "Cartagena",
                Pais = "Colombia",
                Descripcion = "Alojamiento frente al mar con excelente servicio.",
                Precio = 850000,
                ImagenUrl = "/img/destinos/Cancun - Mexico.jpeg"
            }
        };

        // ============================================
        // GET: Destinos/Index
        // ============================================
        public IActionResult Index()
        {
            return View(destinos);
        }

        // ============================================
        // GET: Destinos/Create
        // ============================================
        public IActionResult Create()
        {
            return View();
        }

        // ============================================
        // POST: Destinos/Create
        // ============================================
        [HttpPost]
        public IActionResult Create(Destino destino)
        {
            if (!ModelState.IsValid)
            {
                return View(destino);
            }

            destino.Id = destinos.Count == 0 ? 1 : destinos.Max(d => d.Id) + 1;
            destinos.Add(destino);

            return RedirectToAction("Index");
        }

        // ============================================
        // GET: Destinos/Edit/ID
        // ============================================
        public IActionResult Edit(int id)
        {
            var destino = destinos.FirstOrDefault(d => d.Id == id);

            if (destino == null)
                return NotFound();

            return View(destino);
        }

        // ============================================
        // POST: Destinos/Edit
        // ============================================
        [HttpPost]
        public IActionResult Edit(Destino destinoEditado)
        {
            if (!ModelState.IsValid)
            {
                return View(destinoEditado);
            }

            var destino = destinos.FirstOrDefault(d => d.Id == destinoEditado.Id);

            if (destino == null)
                return NotFound();

            // Actualizar campos
            destino.Nombre = destinoEditado.Nombre;
            destino.Ciudad = destinoEditado.Ciudad;
            destino.Pais = destinoEditado.Pais;
            destino.Descripcion = destinoEditado.Descripcion;
            destino.Precio = destinoEditado.Precio;
            destino.ImagenUrl = destinoEditado.ImagenUrl;

            return RedirectToAction("Index");
        }

        // ============================================
        // GET: Destinos/Delete/ID
        // ============================================
        public IActionResult Delete(int id)
        {
            var destino = destinos.FirstOrDefault(d => d.Id == id);

            if (destino == null)
                return NotFound();

            destinos.Remove(destino);
            return RedirectToAction("Index");
        }
    }
}



