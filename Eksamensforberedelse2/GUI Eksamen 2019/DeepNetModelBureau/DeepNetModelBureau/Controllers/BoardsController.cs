using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeepNetModelBureau.Data;
using DeepNetModelBureau.Models;
using Microsoft.AspNetCore.Authorization;

namespace DeepNetModelBureau.Controllers
{

    public class BoardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int today = 20190104;

        public BoardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boards
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Board> boards = new List<Board>();
            var tasks = _context.Board.ToListAsync();
            foreach (var task in tasks.Result)
            {
                if (task.Date + task.NoOfDays < today)
                {
                    boards.Add(task);
                }
            }
            return View(boards);
        }

        [Authorize]
        public async Task<IActionResult> Index2()
        {
            List<Board> boards = new List<Board>();
            var tasks = _context.Board.ToListAsync();
            foreach (var task in tasks.Result)
            {
                if (task.Date + task.NoOfDays < today)
                {
                    boards.Add(task);
                }
            }
            return View(boards);
        }

        // GET: Boards
        [Authorize]
        public async Task<IActionResult> Future()
        {
            List<Board> boards = new List<Board>();
            var tasks = _context.Board.ToListAsync();
            foreach (var task in tasks.Result)
            {
                if (task.Date + task.NoOfDays > today)
                {
                    boards.Add(task);
                }
            }
            return View(boards);
        }

        // GET: Boards/Details/5
        [Authorize]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.BoardId == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // GET: Boards/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BoardId,Name,Date,NoOfDays,Location,NoOfModels,Comments")] Board board)
        {
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }



        // GET: Boards/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("BoardId,Name,Date,NoOfDays,Location,NoOfModels,Comments")] Board board)
        {
            if (id != board.BoardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.BoardId))
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
            return View(board);
        }

        // GET: Boards/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.BoardId == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var board = await _context.Board.FindAsync(id);
            _context.Board.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(long id)
        {
            return _context.Board.Any(e => e.BoardId == id);
        }
    }
}
