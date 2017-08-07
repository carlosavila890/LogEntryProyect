using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using bd.log.entidades;
using bd.log.entidades.ObjectTranfer;
using bd.log.servicios.Interfaces;
using bd.log.servicios;
using Newtonsoft.Json.Linq;
using bd.log.servicios.Helpers;

namespace bd.log.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/LogEntries")]
    public class LogEntriesController : Controller
    {
        private readonly LogDbContext db;

        private readonly ICommonSecurityService commonSecurityService;

        public LogEntriesController(LogDbContext db)
        {
          
            this.db = db;
        }

        // GET: api/LogEntries
        [HttpGet]
        public IEnumerable<LogEntry> GetLogEntries()
        {
            return db.LogEntries;
        }

        // GET: api/LogEntries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logEntry = await db.LogEntries.SingleOrDefaultAsync(m => m.LogEntryId == id);

            if (logEntry == null)
            {
                return NotFound();
            }

            return Ok(logEntry);
        }

        // PUT: api/LogEntries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogEntry([FromRoute] int id, [FromBody] LogEntry logEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logEntry.LogEntryId)
            {
                return BadRequest();
            }

            db.Entry(logEntry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LogEntries
        [HttpPost]
        public async Task<IActionResult> PostLogEntry([FromBody] JObject form)
        {
            //logEntry.ExceptionTrace = new Exception();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                dynamic logEntryForm = form;

                var logEntry = new LogEntryTranfer
                               {
                               
                                LogCategoryParametre= logEntryForm.LogCategoryParametre,
                                LogLevelShortName= logEntryForm.LogLevelShortName,
                                };

                var logLevelID = db.LogLevels.FirstOrDefault(l => l.ShortName.Contains(logEntry.LogLevelShortName)).LogLevelId;
                var logCategoryID = db.LogCategories.FirstOrDefault(l => l.ParameterValue.Contains(logEntry.LogCategoryParametre)).LogCategoryId;

                if (logLevelID.Equals(null) || logCategoryID.Equals(null))
                {
                    return BadRequest();
                }

                db.Add(new LogEntry
                {
                    UserName = logEntryForm.UserName,
                    ApplicationName = logEntryForm.ApplicationName,
                    ExceptionTrace = (logEntry.ExceptionTrace != null) ? LogEntryHelper.GetAllErrorMsq(logEntry.ExceptionTrace) : null,
                    LogCategoryId = logCategoryID,
                    LogLevelId = logLevelID,
                    LogDate = DateTime.Now,
                    MachineIP = LogNetworkHelper.GetRemoteIpClientAddress(),
                    MachineName = LogNetworkHelper.GetClientMachineName(),
                    Message = logEntryForm.Message,
                    ObjEntityId = logEntryForm.EntityID
                });
                await db.SaveChangesAsync();
                return Ok("Ok");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
             
        }

        // DELETE: api/LogEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logEntry = await db.LogEntries.SingleOrDefaultAsync(m => m.LogEntryId == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            db.LogEntries.Remove(logEntry);
            await db.SaveChangesAsync();

            return Ok(logEntry);
        }

        private bool LogEntryExists(int id)
        {
            return db.LogEntries.Any(e => e.LogEntryId == id);
        }
    }
}