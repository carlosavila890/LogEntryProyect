using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using System;
using bd.log.guardar.ObjectTranfer;
using bd.log.entidades.Utils;
using bd.log.guardar.Servicios;
using Newtonsoft.Json;
using bd.log.entidades.Enumeradores;

namespace bd.log.web.Controllers.MVC
{
    public class LogCategoriesController : Controller
    {
        private readonly IApiServicio apiServicio;


        public LogCategoriesController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;
    }

        // GET: LogCategorys
        public async Task<IActionResult> Index()
        {

            try
            {
                var ListaAdscgrp = await apiServicio.Listar<LogCategory>(new Uri(WebApp.BaseAddress), "api/LogCategories/ListarLogCategories");
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
        // GET: LogCategorys/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LogCategory logCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await apiServicio.InsertarAsync(logCategory, new Uri(WebApp.BaseAddress), "/api/LogCategories/InsertarLogCategory");
                    if (response.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }

                    ViewData["Error"] = response.Message;
                }
                return View(logCategory);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        // GET: LogCategorys/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var respuesta = await apiServicio.SeleccionarAsync<Response>(id, new Uri(WebApp.BaseAddress), "/api/LogCategories");
                var resultado = JsonConvert.DeserializeObject<LogCategory>(respuesta.Resultado.ToString());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LogCategory LogCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respuesta = await apiServicio.EditarAsync(id, LogCategory,new Uri( WebApp.BaseAddress), "/api/LogCategories");

                    if (respuesta.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }

                    ViewData["Error"] = respuesta.Message;
                }
                return View(LogCategory);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                var respuesta = await apiServicio.EliminarAsync(id,new Uri(WebApp.BaseAddress), "/api/LogCategories");
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
    }
}
