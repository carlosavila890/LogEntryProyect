using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.log.servicios.Interfaces;
using bd.log.entidades.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using bd.log.entidades;
using System;
using PagedList.Core;
using PagedList.Core.Mvc;
using bd.log.entidades.Utils;

namespace bd.log.web.Controllers
{
    public class ConsultasLogsController : Controller
    {
       
        private readonly IApiServicio apiServicio;


        public ConsultasLogsController( IApiServicio apiServicio)
        {   
            this.apiServicio = apiServicio;
        }

        public async Task<IActionResult> ConsultaFiltrada(LogEntryViewModel view)
        {
            var log = new LogEntryViewModel();
            log.LogEntrys = new List<LogEntry>();


            // view.LogEntrys = await logEntryService.GetLogEntryFiltrado(view);
            view.LogEntrys = await apiServicio.Listar<LogEntry>(view, new Uri(WebApp.BaseAddress), "/api/LogEntries/ListaFiltradaLogEntry");
            await CargarCombos();
            return View("Index", view);
        }

        public async Task<IActionResult> Index()
        {

            var log = new LogEntryViewModel();
            log.LogEntrys = new List<LogEntry>();
            await CargarCombos();
            return View(log);
        }

        private async Task CargarCombos()
        {
            ViewData["ShortName"] = new SelectList(await apiServicio.Listar<LogLevel>(new Uri(WebApp.BaseAddress), "/api/LogLevels/ListarLogLevels"), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(await apiServicio.Listar<LogCategory>(new Uri(WebApp.BaseAddress), "/api/LogCategories/ListarLogCategories"), "LogCategoryId", "Name");
        }
    }
}