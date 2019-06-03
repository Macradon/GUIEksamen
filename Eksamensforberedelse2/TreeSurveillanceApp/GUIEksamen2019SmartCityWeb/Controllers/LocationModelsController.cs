using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GUIEksamen2019SmartCityWeb.Data;
using GUIEksamen2019SmartCityWeb.Models;

namespace GUIEksamen2019SmartCityWeb.Controllers
{
    public class LocationModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocationModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationModel.ToListAsync());
        }

        // GET: LocationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationModel = await _context.LocationModel
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // GET: LocationModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,Name,Address,ListOfTrees")] LocationModel locationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationModel);
        }

        // GET: LocationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationModel = await _context.LocationModel.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }
            return View(locationModel);
        }

        // POST: LocationModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,Name,Address,ListOfTrees")] LocationModel locationModel)
        {
            if (id != locationModel.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationModelExists(locationModel.LocationID))
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
            return View(locationModel);
        }

        // GET: LocationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationModel = await _context.LocationModel
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // POST: LocationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationModel = await _context.LocationModel.FindAsync(id);
            _context.LocationModel.Remove(locationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationModelExists(int id)
        {
            return _context.LocationModel.Any(e => e.LocationID == id);
        }
    }
}
