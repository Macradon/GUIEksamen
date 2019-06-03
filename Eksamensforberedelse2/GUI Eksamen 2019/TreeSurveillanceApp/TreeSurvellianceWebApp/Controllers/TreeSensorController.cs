using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TreeSurvellianceWebApp.Data;
using TreeSurvellianceWebApp.Models;

namespace TreeSurvellianceWebApp.Controllers
{
    public class TreeSensorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreeSensorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sensors
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TreeSensor.ToListAsync());
        }

        // GET: Sensors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.TreeSensor
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Sensors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("LocationId,TreeSort,SensorID,Coordinates")] TreeSensor sensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        // GET: Sensors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.TreeSensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("LocationId,TreeSort,SensorID,Coordinates")] TreeSensor sensor)
        {
            if (id != sensor.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.LocationID))
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
            return View(sensor);
        }

        // GET: Sensors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Int16? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.TreeSensor
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Int16 id)
        {
            var sensor = await _context.TreeSensor.FindAsync(id);
            _context.TreeSensor.Remove(sensor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorExists(Int16 id)
        {
            return _context.TreeSensor.Any(e => e.LocationID == id);
        }
    }
}