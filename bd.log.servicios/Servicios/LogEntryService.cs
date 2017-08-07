using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using bd.log.servicios.Interfaces;
using bd.log.entidades;
 
using System.Threading.Tasks;

namespace bd.log.servicios.Servicios
{
    public class LogEntryService : ILogEntryService
    {
        #region Attributes
       
        #endregion

        #region Services
        private readonly ICommonSecurityService commonSecurityService;
        
        #endregion

        #region Constructors

        public LogEntryService(LogDbContext db, ICommonSecurityService commonSecurityService)
        {
            this.db = db;
            this.commonSecurityService = commonSecurityService;
        }

        #endregion

        #region Methods

        public LogEntry GetLogEntry(int logEntryId,string userName,string applicationName)
        {
            LogEntry logEntry = null;
            string logMessage = "Obteniendo un LogEntry";

            try
            {
                logEntry = db.LogEntries.Where(l => l.LogEntryId == logEntryId).Include(lg => lg.LogCategory).Include(log => log.LogLevel).FirstOrDefault();
            }
            catch (Exception ex)
            {
                commonSecurityService.SaveLogEntry("ERR",
                        "Critical",
                        ex,
                        logMessage,
                        logEntryId.ToString(),userName,applicationName
                    );

                logEntry = null;
                return logEntry;
            }
            return logEntry;
        }

        public List<LogEntry> GetLogEntries(string userName, string applicationName)
        {
            List<LogEntry> logEntries = null;
            string logMessage = "Listar todos los LogEntries";

            try
            {
                logEntries = db.LogEntries.Include(p=>p.LogCategory).Include(p=>p.LogLevel).ToList();
            }
            catch (Exception ex)
            {
                commonSecurityService.SaveLogEntry("ERR",
                        "Critical",
                        ex,
                        logMessage,
                        string.Empty,userName, applicationName
                    );

                logEntries = null;
                return logEntries;
            }
            return logEntries;
        }

        public List<LogEntry> GetLogEntries(DateTime startDate, DateTime FechaFin, string userName, string applicationName)
        {
            List<LogEntry> logEntries = null;
            string logMessage = string.Format("Listar los LogEntries comprendidos entre: {0} y {1}", startDate.ToString(), FechaFin.ToString());

            try
            {
                logEntries = db.LogEntries.Where(l => DateTime.Compare(l.LogDate.Date, startDate.Date) <= 0 && DateTime.Compare(FechaFin.Date, l.LogDate.Date) >= 0).Include(lg => lg.LogCategory).Include(log => log.LogLevel).ToList();
            }
            catch (Exception ex)
            {
                commonSecurityService.SaveLogEntry("ERR",
                        "Critical",
                        ex,
                        logMessage,
                        string.Empty,userName, applicationName
                    );

                logEntries = null;
                return logEntries;
            }
            return logEntries;
        }

        public List<LogEntry> GetLogEntriesByParameter(string parametro)
        {
            switch (parametro)
            {
                
                case "ApplicationName":
                    return (new List<LogEntry>(db.LogEntries.GroupBy(x => x.ApplicationName).Select(g => g.First())));
                case "MachineIP":
                    return (new List<LogEntry>(db.LogEntries.GroupBy(x => x.MachineIP).Select(g => g.First())));
                case "UserName":
                    return (new List<LogEntry>(db.LogEntries.GroupBy(x => x.UserName).Select(g => g.First())));
                case "MachineName":
                    return (new List<LogEntry>(db.LogEntries.GroupBy(x => x.MachineName).Select(g => g.First())));
                default:
                    return null;
            }


        }

        public async Task<List<LogEntry>> GetListaFiltradaLogEntry(int LogLevelId, int LogCategoryId, string ApplicationName, string MachineIP, string UserName, string MachineName)
        {
            // return (new List<LogEntry>(db.LogEntries.Where(x => x.LogLevel.LogLevelId  == LogLevelId && x.ApplicationName == ApplicationName && x.MachineIP == MachineIP && x.UserName == UserName && x.MachineName == MachineName)));
             return await ((db.LogEntries.
                 Where(x => (x.LogLevel.LogLevelId  == LogLevelId || LogLevelId== 0)
                 && (x.LogCategoryId == LogCategoryId || LogCategoryId == 0)
                 && (x.ApplicationName == ApplicationName || ApplicationName==null)
                 && (x.MachineIP == MachineIP || MachineIP==null)
                 && (x.UserName == UserName || UserName==null)
                 && (x.MachineName == MachineName || MachineName==null))).ToListAsync());


        }

        #endregion
    }
}
