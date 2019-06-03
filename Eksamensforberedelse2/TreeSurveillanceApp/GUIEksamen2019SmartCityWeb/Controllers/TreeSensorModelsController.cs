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
    public class TreeSensorModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreeSensorModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TreeSensorModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TreeSensorModel.ToListAsync());
        }

        // GET: TreeSensorModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeSensorModel = await _context.TreeSensorModel
                .FirstOrDefaultAsync(m => m.SensorID == id);
            if (treeSensorModel == null)
            {
                return NotFound();
            }

            return View(treeSensorModel);
        }

        // GET: TreeSensorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TreeSensorModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SensorID,TreeSort,LocationID,CoordinateIat,CoordinateIon")] TreeSensorModel treeSensorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treeSensorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treeSensorModel);
        }

        // GET: TreeSensorModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeSensorModel = await _context.TreeSensorModel.FindAsync(id);
            if (treeSensorModel == null)
            {
                return NotFound();
            }
            return View(treeSensorModel);
        }

        // POST: TreeSensorModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SensorID,TreeSort,LocationID,CoordinateIat,CoordinateIon")] TreeSensorModel treeSensorModel)
        {
            if (id != treeSensorModel.SensorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treeSensorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreeSensorModelExists(treeSensorModel.SensorID))
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
            return View(treeSensorModel);
        }

        // GET: TreeSensorModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeSensorModel = await _context.TreeSensorModel
                .FirstOrDefaultAsync(m => m.SensorID == id);
            if (treeSensorModel == null)
            {
                return NotFound();
            }

            return View(treeSensorModel);
        }

        // POST: TreeSensorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var treeSensorModel = await _context.TreeSensorModel.FindAsync(id);
            _context.TreeSensorModel.Remove(treeSensorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreeSensorModelExists(string id)
        {
            return _context.TreeSensorModel.Any(e => e.SensorID == id);
        }
    }
}
