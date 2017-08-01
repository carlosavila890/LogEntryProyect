using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.log.datos;
using bd.log.entidades;

namespace bd.log.web.Controllers.MVC
{
    public class LogCategoriesController : Controller
    {
        private readonly LogDbContext _context;

        public LogCategoriesController(LogDbContext context)
        {
            _context = context;    
        }

        // GET: LogCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.LogCategories.ToListAsync());
        }

        // GET: LogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logCategory = await _context.LogCategories
                .SingleOrDefaultAsync(m => m.LogCategoryId == id);
            if (logCategory == null)
            {
                return NotFound();
            }

            return View(logCategory);
        }

        // GET: LogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogCategoryId,ParameterValue,Name,Description")] LogCategory logCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logCategory);
        }

        // GET: LogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logCategory = await _context.LogCategories.SingleOrDefaultAsync(m => m.LogCategoryId == id);
            if (logCategory == null)
            {
                return NotFound();
            }
            return View(logCategory);
        }

        // POST: LogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogCategoryId,ParameterValue,Name,Description")] LogCategory logCategory)
        {
            if (id != logCategory.LogCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogCategoryExists(logCategory.LogCategoryId))
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
            return View(logCategory);
        }

        // GET: LogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logCategory = await _context.LogCategories
                .SingleOrDefaultAsync(m => m.LogCategoryId == id);
            if (logCategory == null)
            {
                return NotFound();
            }

            return View(logCategory);
        }

        // POST: LogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logCategory = await _context.LogCategories.SingleOrDefaultAsync(m => m.LogCategoryId == id);
            _context.LogCategories.Remove(logCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LogCategoryExists(int id)
        {
            return _context.LogCategories.Any(e => e.LogCategoryId == id);
        }
    }
}
