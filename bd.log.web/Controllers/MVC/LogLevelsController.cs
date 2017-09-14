using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Servicios;
using bd.log.servicios.Enumeradores;
using bd.log.entidades.Utils;
using Newtonsoft.Json;

namespace bd.log.web.Controllers.MVC
{
    public class LogLevelsController : Controller
    {

        private readonly IApiServicio apiServicio;

        public LogLevelsController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;
    }

        // GET: LogLevels
        public async Task<IActionResult> Index()
        {

            try
            {
                var ListaAdscgrp = await apiServicio.Listar<LogLevel>(new Uri(WebApp.BaseAddress), "/api/LogLevels/ListarLogLevels");
                return View(ListaAdscgrp);
            }
            catch (Exception ex)
            {

                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.WebAppLogEntry),
                    Message = "Listando Categorial",
                    ExceptionTrace = ex,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.NetActivity),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "Usuario Log "
                });
                return BadRequest();
            }
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
        public async Task<IActionResult> Create(LogLevel logLevel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await apiServicio.InsertarAsync(logLevel, new Uri(WebApp.BaseAddress), "/api/LogLevels/InsertarLogLevel");
                    if (response.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }

                    ViewData["Error"] = response.Message;
                }
                return View(logLevel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: LogLevels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var respuesta = await apiServicio.SeleccionarAsync<Response>(Convert.ToString(id), new Uri(WebApp.BaseAddress), "/api/LogLevels");
                var resultado = JsonConvert.DeserializeObject<LogLevel>(respuesta.Resultado.ToString());
                if (respuesta.IsSuccess)
                {
                    return View(resultado);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        // POST: LogLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,LogLevel logLevel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respuesta = await apiServicio.EditarAsync(Convert.ToString(id), logLevel, new Uri(WebApp.BaseAddress), "/api/LogLevels");

                    if (respuesta.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }

                    ViewData["Error"] = respuesta.Message;
                }
                return View(logLevel);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: LogLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                var respuesta = await apiServicio.EliminarAsync(Convert.ToString(id), new Uri(WebApp.BaseAddress), "/api/LogLevels");
                if (!respuesta.IsSuccess)
                {
                    return BadRequest();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest();
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
