using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schotland.Models;

namespace Schotland.Controllers
{
    public class PlacesController : Controller
    {
        private readonly gitosContext _context;

        public PlacesController(gitosContext context)
        {
            _context = context;
        }

        // GET: Places
        public async Task<IActionResult> Index()
        {
            return View(await _context.Places.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .SingleOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Info,Lat,Lon")] Places places, IFormFile Picture)
        {
         if (Picture != null && Picture.Length > 0)
            {

                using (var ms = new MemoryStream())
                {
                    Picture.CopyTo(ms);
                    var image = Controllers.ImagesHelper.byteArrayToImage(ms.ToArray());
                    var sizedimage = Controllers.ImagesHelper.Resize(image, 500, 500);
                    places.Picture = Controllers.ImagesHelper.ToByteArray(sizedimage);

                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(places);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(places);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places.SingleOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }
            return View(places);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Info,Lat,Lon")] Places places, IFormFile Image)
        {
            if (id != places.Id)
            {
                return NotFound();
            }
            if (Image != null && Image.Length > 0)
            {

                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    var image = Controllers.ImagesHelper.byteArrayToImage(ms.ToArray());
                    var sizedimage = Controllers.ImagesHelper.Resize(image, 500, 500);
                    places.Picture = Controllers.ImagesHelper.ToByteArray(sizedimage);

                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(places);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacesExists(places.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(places);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .SingleOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var places = await _context.Places.SingleOrDefaultAsync(m => m.Id == id);
            _context.Places.Remove(places);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacesExists(int id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
    }
}
