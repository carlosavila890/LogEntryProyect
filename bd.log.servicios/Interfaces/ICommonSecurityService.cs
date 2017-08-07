using bd.log.entidades;
using bd.log.entidades.ObjectTranfer;
using bd.log.utils;
using System;
using System.Threading.Tasks;

namespace bd.log.servicios.Interfaces
{
    public interface ICommonSecurityService
    {
       
       Task<Response> SaveLogEntry(LogEntryTranfer logEntryTranfer);
    }
}
