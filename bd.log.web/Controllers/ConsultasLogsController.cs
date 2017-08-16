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


namespace bd.log.web.Controllers
{
    public class ConsultasLogsController : Controller
    {
        private readonly ILogEntryService logEntryService;
        private readonly ILogLevelService logLevelService;
        private readonly ILogCategoryService logCategoryService;
     

        public ConsultasLogsController(ILogEntryService logEntryService, ILogLevelService logLevelService, ILogCategoryService logCategoryService)
        {
            //this.db = db;
            this.logEntryService = logEntryService;
            this.logLevelService = logLevelService;
            this.logCategoryService = logCategoryService;
        }

        public async Task<IActionResult> ConsultaFiltrada(LogEntryViewModel view)
        {
            var log = new LogEntryViewModel();
            log.LogEntrys = new List<LogEntry>();

            //int pageSize = 10;
            //int pageIndex = 1;
            //pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            //ViewBag.CurrentSort = sortOrder;

            //sortOrder = String.IsNullOrEmpty(sortOrder) ? "LogDate" : sortOrder;

            //IPagedList<LogEntry> logEntrys = null;

            //switch (sortOrder)
            //{
            //    case "LogDate":
            //        if (sortOrder.Equals(CurrentSort))
            //        { 
            view.LogEntrys = await logEntryService.GetLogEntryFiltrado(view);
            //            logEntrys.ToPagedList(pageIndex, pageSize);
            //        }
            //    products = _db.Products.OrderByDescending
            //                (m => m.ProductID).ToPagedList(pageIndex, pageSize);
            //    else
            //        products = _db.Products.OrderBy
            //                (m => m.ProductID).ToPagedList(pageIndex, pageSize);
            //    break;
            //case "ProductName":
            //    if (sortOrder.Equals(CurrentSort))
            //        products = _db.Products.OrderByDescending
            //                (m => m.ProductName).ToPagedList(pageIndex, pageSize);
            //    else
            //        products = _db.Products.OrderBy
            //                (m => m.ProductName).ToPagedList(pageIndex, pageSize);
            /*break*/;

                // Add sorting statements for other columns

                //case "Default":
                //    view.LogEntrys = await logEntryService.GetLogEntryFiltrado(view);
                //    logEntrys.ToPagedList(pageIndex, pageSize);
                //    break;
            //}
            ViewData["ShortName"] =  new SelectList(await logLevelService.GetLogLevels(), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(await logCategoryService.GetLogCategories(), "LogCategoryId", "Name");
           

            //ViewData["ShortName"] =  new SelectList(await logLevelService.GetLogLevels(), "LogLevelId", "ShortName");
            //ViewData["Name"] = new SelectList(await logCategoryService.GetLogCategories(), "LogCategoryId", "Name");
            return View("Index", view);

        }

        public async Task<IActionResult> Index()
        {

            var log = new LogEntryViewModel();
            log.LogEntrys = new List<LogEntry>();
            //log.LogLevels = db.LogLevels.ToList();

            ViewData["ShortName"] = new SelectList(await logLevelService.GetLogLevels(), "LogLevelId", "ShortName");
            ViewData["Name"] = new SelectList(await logCategoryService.GetLogCategories(), "LogCategoryId", "Name");
            //ViewData["ApplicationName"] = new SelectList(logEntryService.GetLogEntriesByParameter("ApplicationName"), "LogLevelId", "ApplicationName");
            //ViewData["MachineIP"] = new SelectList(logEntryService.GetLogEntriesByParameter("MachineIP"), "LogLevelId", "MachineIP");
            //ViewData["UserName"] = new SelectList(logEntryService.GetLogEntriesByParameter("UserName"), "LogLevelId", "UserName");
            //ViewData["MachineName"] = new SelectList(logEntryService.GetLogEntriesByParameter("MachineName"), "LogLevelId", "MachineName");



            return View(log);
        }
    }
}