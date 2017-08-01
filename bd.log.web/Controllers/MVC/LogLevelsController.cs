using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bd.log.datos;
using bd.log.entidades;

namespace bd.log.web.Controllers.MVC
{
    public class LogLevelsController : Controller
    {
        private readonly LogDbContext _context;

        public LogLevelsController(LogDbContext context)
        {
            _context = context;    
        }

        // GET: LogLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.LogLevels.ToListAsync());
        }

        // GET: LogLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logLevel = await _context.LogLevels
                .SingleOrDefaultAsync(m => m.LogLevelId == id);
            if (logLevel == null)
            {
                return NotFound();
            }

            return View(logLevel);
        }

        // GET: LogLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogLevelId,Code,Name,ShortName,Description")] LogLevel logLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logLevel);
        }

        // GET: LogLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logLevel = await _context.LogLevels.SingleOrDefaultAsync(m => m.LogLevelId == id);
            if (logLevel == null)
            {
                return NotFound();
            }
            return View(logLevel);
        }

        // POST: LogLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogLevelId,Code,Name,ShortName,Description")] LogLevel logLevel)
        {
            if (id != logLevel.LogLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogLevelExists(logLevel.LogLevelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(logLevel);
        }

        // GET: LogLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logLevel = await _context.LogLevels
                .SingleOrDefaultAsync(m => m.LogLevelId == id);
            if (logLevel == null)
            {
                return NotFound();
            }

            return View(logLevel);
        }

        // POST: LogLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logLevel = await _context.LogLevels.SingleOrDefaultAsync(m => m.LogLevelId == id);
            _context.LogLevels.Remove(logLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LogLevelExists(int id)
        {
            return _context.LogLevels.Any(e => e.LogLevelId == id);
        }
    }
}
