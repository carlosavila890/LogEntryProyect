using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.log.servicios.Interfaces;
using bd.log.entidades.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using bd.log.entidades;

namespace bd.log.web.Controllers
{
    public class ConsultasLogsController : Controller
    {
        private readonly ILogEntryService logEntryService;
        private readonly ILogLevelService logLevelService;
        private readonly ILogCategoryService logCategoryService;
     

        public ConsultasLogsController(ILogEntryService logEntryService, ILogLevelService logLevelService, ILogCategoryService logCategoryService, LogDbContext db)
        {
            this.db = db;
            this.logEntryService = logEntryService;
            this.logLevelService = logLevelService;
            this.logCategoryService = logCategoryService;
        }

        public async Task<IActionResult> ConsultaFiltrada(LogEntryViewModel view)
        {
            var log = new LogEntryViewModel();

            view.LogEntrys = await logEntryService.GetListaFiltradaLogEntry(view.LogLevelId, view.LogCategoryId, view.ApplicationName, view.MachineIP, view.UserName, view.MachineName);
            ViewData["ShortName"] = new SelectList(logLevelService.GetLogLevels(), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(logCategoryService.GetLogCategories(), "LogCategoryId", "Name");
            return View("Index", view);

        }

        public IActionResult Index()
        {
            var log = new LogEntryViewModel();



            log.LogEntrys = new List<LogEntry>();
            //log.LogLevels = db.LogLevels.ToList();
            ViewData["ShortName"] = new SelectList(logLevelService.GetLogLevels(), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(logCategoryService.GetLogCategories(), "LogCategoryId", "Name");
            //ViewData["ApplicationName"] = new SelectList(logEntryService.GetLogEntriesByParameter("ApplicationName"), "LogLevelId", "ApplicationName");
            //ViewData["MachineIP"] = new SelectList(logEntryService.GetLogEntriesByParameter("MachineIP"), "LogLevelId", "MachineIP");
            //ViewData["UserName"] = new SelectList(logEntryService.GetLogEntriesByParameter("UserName"), "LogLevelId", "UserName");
            //ViewData["MachineName"] = new SelectList(logEntryService.GetLogEntriesByParameter("MachineName"), "LogLevelId", "MachineName");



            return View(log);
        }
    }
}