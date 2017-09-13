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

            var listado = await logCategoryServicio.ListarLogCategories();
            if (listado == null)
            {
                return BadRequest();
            }
            return View(listado);
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
                    var response = await logCategoryServicio.CrearAsync(logCategory);
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
                var respuesta = await logCategoryServicio.SeleccionarAsync(id);

                if (respuesta.IsSuccess)
                {
                    return View(respuesta.Resultado);
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
                    var respuesta = await logCategoryServicio.EditarAsync(id, LogCategory);

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
                var respuesta = await logCategoryServicio.EliminarAsync(id);
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
