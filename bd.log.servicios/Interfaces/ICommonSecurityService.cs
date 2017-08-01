using bd.log.entidades;
using bd.log.utils;
using System;

namespace bd.log.servicios.Interfaces
{
    public interface ICommonSecurityService
    {
        string GetAllErrorMsq(Exception e);
        Response SaveLogEntry(string logLevelShortName, string logCategoryParametre, Exception exceptionTrace, string message, string entityID,string userName,string applicationName);
    }
}
