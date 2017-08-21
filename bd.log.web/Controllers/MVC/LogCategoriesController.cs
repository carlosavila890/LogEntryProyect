using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using System;
using bd.log.guardar.ObjectTranfer;

namespace bd.log.web.Controllers.MVC
{
    public class LogCategoriesController : Controller
    {
        private readonly ILogCategoryService logCategoryServicio;


        public LogCategoriesController(ILogCategoryService logCategoryServicio)
        {
            this.logCategoryServicio = logCategoryServicio;
    }

        // GET: LogCategorys
        public async Task<IActionResult> Index()
        {

            return View(await logCategoryServicio.GetLogCategories());
        }

   

        // GET: LogCategorys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogCategorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogCategoryId,Description,Name,ParameterValue")] LogCategory logCategory)
        {
            if (ModelState.IsValid)
            {
              var response= await logCategoryServicio.Crear(logCategory);
                //if (response.IsSuccess)
                //{
                //    var responseLog = await commonSecurityService.SaveLogEntry(new LogEntryTranfer
                //    {
                //        ApplicationName = "LogEntry",
                //        ExceptionTrace = null,
                //        Message = "Se ha actualizado un Log Entry",
                //        UserName = "Usuario 1",
                //        LogCategoryParametre = "Edit",
                //        LogLevelShortName = "ADV",
                //        EntityID =string.Format("{0} {1}","LogCategory", logCategory.LogCategoryId),
                //    },new Uri("http://localhost:61615"), "/api/LogEntry");

                    
                //    return RedirectToAction("Index");
                    
                //}

                ViewData["Error"] = response.Message;

            }
            return View(logCategory);
        }

        // GET: LogCategorys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await logCategoryServicio.GetLogCategory(Convert.ToInt32(id)));
        }

        // POST: LogCategorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogCategoryId,ParameterValue,Name,Description")] LogCategory logCategory)
        {
            if (ModelState.IsValid)
            {
              var response=  await logCategoryServicio.Editar(logCategory);
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");

                }

                ViewData["Error"] = response.Message;
            }

            return View(logCategory);
            
        }

        // GET: LogCategorys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var respuesta = await logCategoryServicio.Eliminar(Convert.ToInt32(id));
                if (respuesta.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
              
                //ViewData["Error"] = respuesta.Message;
            }
            catch (Exception ex)
            {

                ViewData["Error"] = ex.Message;
                return NotFound();
            }


        }

        // POST: LogCategorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var logCategory = await _context.LogCategorys.SingleOrDefaultAsync(m => m.LogCategoryId == id);
            //_context.LogCategorys.Remove(logCategory);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //private bool LogCategoryExists(int id)
        //{
        //    //return _context.LogCategorys.Any(e => e.LogCategoryId == id);
        //}
    }
}
