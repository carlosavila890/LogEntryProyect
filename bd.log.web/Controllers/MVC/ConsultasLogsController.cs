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
using Newtonsoft.Json;

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
            ViewData["Error"] = string.Empty;
            view.LogEntrys = new List<LogEntry>();
            TimeSpan days = view.LogDateFinish- view.LogDateStart;

            if (days.Days>60)
            {
                ViewData["Error"] = "Entre la fecha de inicio y la fecha fin debe haber máximo 60 días";
                return View("Index", view);
            }

            if (view.LogDateStart.Year==1&& view.LogDateStart.Month==1&& view.LogDateStart.Day==1)
            {
                DateTime hoy = DateTime.Today;
                DateTime dosMesesAtras = hoy.AddDays(-60);

                view.LogDateFinish = new DateTime(hoy.Year, hoy.Month, hoy.Day);
                view.LogDateStart = new DateTime(dosMesesAtras.Year, dosMesesAtras.Month, dosMesesAtras.Day);

            }

            await CargarCombos();

            if (view.LogDateStart > DateTime.Today)
            {
                ViewData["Error"] = "La fecha de inicio no puede ser mayor que la fecha de hoy";
                return View("Index", view);           
            }

            if (view.LogDateFinish > DateTime.Today)
            {
                ViewData["Error"] = "La fecha fin no puede ser mayor que la fecha de hoy";
                return View("Index", view);
            }

            if (view.LogDateStart > view.LogDateFinish)
            {
                ViewData["Error"] = "La fecha de inicio no puede ser mayor que la fecha fin";
                return View("Index", view);
            }


            if (view.ApplicationName != null && view.MachineIP != null && view.MachineName != null && view.UserName != null)
            {
                view.LogEntrys = await apiServicio.Listar<LogEntry>(view, new Uri(WebApp.BaseAddress), "/api/LogEntries/ListaFiltradaLogEntry");  
                return View("Index", view);
            }
            else {
                
                ViewData["Error"] = "Nota: Para realizar la consulta debe ingresar al menos la información de la aplicación, la ip, el nombre de la máquina, la fecha de inicio y el nombre del usuario";
                return View("Index", view);
            }

        }
        
        
        public async Task<IActionResult> Index()
        {
            
            var log = new LogEntryViewModel();
            log.LogEntrys = new List<LogEntry>();
            DateTime hoy = DateTime.Today;
            DateTime dosMesesAtras = hoy.AddDays(-60);

            log.LogDateFinish = new DateTime(hoy.Year, hoy.Month, hoy.Day);
            log.LogDateStart = new DateTime(dosMesesAtras.Year, dosMesesAtras.Month, dosMesesAtras.Day);
            ViewData["Error"] = string.Empty;
            await CargarCombos();
            return View(log);
            
        }

        private async Task CargarCombos()
        {
           
            ViewData["ShortName"] = new SelectList(await apiServicio.Listar<LogLevel>(new Uri(WebApp.BaseAddress), "api/LogLevels/ListarLogLevels"), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(await apiServicio.Listar<LogCategory>(new Uri(WebApp.BaseAddress), "api/LogCategories/ListarLogCategories"), "LogCategoryId", "Name");


        }



        public async Task<ActionResult> Create(string id)

        {
           
            try
            {
                var respuesta = await apiServicio.SeleccionarAsync<Response>(id, new Uri(WebApp.BaseAddress), "/api/LogEntries");
                var resultado = JsonConvert.DeserializeObject<LogEntry>(respuesta.Resultado.ToString());
                if (respuesta.IsSuccess)
                {
                    return PartialView("Create",resultado);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();

            }
            
            return PartialView("Create");
        }
    }
}