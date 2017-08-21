using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.guardar.ObjectTranfer;

namespace bd.log.web.Controllers.MVC
{
    public class LogLevelsController : Controller
    {

        private readonly ILogLevelService logLevelServicio;

        public LogLevelsController(ILogLevelService logLevelServicio)
        {
            this.logLevelServicio = logLevelServicio;
    }

        // GET: LogLevels
        public async Task<IActionResult> Index()
        {

            return View(await logLevelServicio.GetLogLevels());
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
              var response= await logLevelServicio.Crear(logLevel);
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
                //        EntityID =string.Format("{0} {1}","LogLevel", logLevel.LogLevelId),
                //    },new Uri("http://localhost:61615"), "/api/");

                    
                //    return RedirectToAction("Index");
                    
                //}

                ViewData["Error"] = response.Message;

            }
            return View(logLevel);
        }

        // GET: LogLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await logLevelServicio.GetLogLevel(Convert.ToInt32(id)));
        }

        // POST: LogLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogLevelId,Code,Name,ShortName,Description")] LogLevel logLevel)
        {
            if (ModelState.IsValid)
            {
              var response=  await logLevelServicio.Editar(logLevel);
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");

                }

                ViewData["Error"] = response.Message;
            }

            return View(logLevel);
            
        }

        // GET: LogLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var respuesta = await logLevelServicio.Eliminar(Convert.ToInt32(id));
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

        // POST: LogLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var logLevel = await _context.LogLevels.SingleOrDefaultAsync(m => m.LogLevelId == id);
            //_context.LogLevels.Remove(logLevel);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //private bool LogLevelExists(int id)
        //{
        //    //return _context.LogLevels.Any(e => e.LogLevelId == id);
        //}
    }
}
