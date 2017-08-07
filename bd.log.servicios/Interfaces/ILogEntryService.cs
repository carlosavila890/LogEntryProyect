using bd.log.entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bd.log.servicios.Interfaces
{
    public interface ILogEntryService
    {
        LogEntry GetLogEntry(int logEntryId,string userName, string applicationName);
        List<LogEntry> GetLogEntries(string userName, string applicationName);
        List<LogEntry> GetLogEntries(DateTime startDate, DateTime FechaFin, string userName, string applicationName);
        List<LogEntry> GetLogEntriesByParameter(string parametro);
        Task<List<LogEntry>> GetListaFiltradaLogEntry(int LogLevelId, int LogCategoryId, string ApplicationName, string MachineIP, string UserName, string MachineName);
    }
}
