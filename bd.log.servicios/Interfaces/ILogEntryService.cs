using bd.log.entidades;
using bd.log.entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bd.log.servicios.Interfaces
{
    public interface ILogEntryService
    {

        //Task<LogEntry> GetLogEntry(int logEntryId,string userName, string applicationName);
        //Task<List<LogEntry>> GetLogEntries(string userName, string applicationName);
        //Task<List<LogEntry>> GetLogEntries(DateTime startDate, DateTime FechaFin, string userName, string applicationName);
        //Task<List<LogEntry>> GetLogEntriesByParameter(string parametro);
        //Task<List<LogEntry>> GetListaFiltradaLogEntry(int LogLevelId, int LogCategoryId, string ApplicationName, string MachineIP, string UserName, string MachineName);
        Task<List<LogEntry>> GetLogEntryFiltrado(LogEntryViewModel logentryviewmodel);

    }
}
