using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.log.servicios.Interfaces;
using bd.log.entidades.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using bd.log.datos;

namespace bd.log.web.Controllers
{
    public class ConsultasLogsController : Controller
    {
        private readonly ILogEntryService logEntryService;
        private readonly LogDbContext db;

        public ConsultasLogsController(ILogEntryService logEntryService, LogDbContext db)
        {
            this.db = db;
            this.logEntryService = logEntryService;
        }
        public IActionResult Index()
        {
            var log =new  LogEntryViewModel();

            var logEntrys = logEntryService.GetLogEntries("", "");

            log.LogEntrys = db.LogEntries.ToList();
            log.LogLevels = db.LogLevels.ToList();
            ViewData["Id"] = new SelectList(log.LogLevels, "LogLevelId", "ShortName");
            return View(log);
        }
    }
}