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

            view.LogEntrys = new List<LogEntry>();
            await CargarCombos();

            if (view.ApplicationName != null && view.MachineIP != null && view.MachineName != null && view.UserName != null&&view.LogDateStart!=null)
            {
                view.LogDateFinish = view.LogDateStart.AddDays(60);


                // view.LogEntrys = await logEntryService.GetLogEntryFiltrado(view);
                view.LogEntrys = await apiServicio.Listar<LogEntry>(view, new Uri(WebApp.BaseAddress), "/api/LogEntries/ListaFiltradaLogEntry");
               
                return View("Index", view);
            }
            else {

                
                ViewData["Error"] = "Nota: Para realizar la consulta debe ingresar al menos la información de la aplicación, la ip, el nombre de la máquina, la fecha de inicio y el nombre del usuario";
                return View("Index", view);
            }
        }
        
        
        public async Task<IActionResult> Index(int id)
        {
            
            var log = new LogEntryViewModel(); 
            
            if (id == 0)
            {

                log = new LogEntryViewModel();
                log.LogEntrys = new List<LogEntry>();
                await CargarCombos();
                return View(log);

            }
            else
            {
                log = new LogEntryViewModel();
                log.LogEntrys = new List<LogEntry>();

                var respuesta = await apiServicio.SeleccionarAsync<Response>(id.ToString(), new Uri(WebApp.BaseAddress), "/api/LogEntries");
                var resultado = JsonConvert.DeserializeObject<LogEntry>(respuesta.Resultado.ToString());
                if (respuesta.IsSuccess)
                {


                    var view = new LogEntryViewModel
                    {
                        
                            MachineName=resultado.MachineName,
                            UserName= resultado.UserName,
                            MachineIP =resultado.MachineIP,
                            ApplicationName=resultado.ApplicationName,
                            LogCategoryId=resultado.LogCategoryId,
                           

                     };


                    view.LogEntrys = await apiServicio.Listar<LogEntry>(view, new Uri(WebApp.BaseAddress), "/api/LogEntries/ListaFiltradaLogEntry");
                    await CargarCombos();
                    return View("Index", view);

                }
                

            }
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