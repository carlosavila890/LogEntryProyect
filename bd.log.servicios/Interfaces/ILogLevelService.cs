using bd.log.entidades;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.log.servicios.Interfaces
{
   public interface ILogLevelService
    {
        List<LogLevel> GetLogLevels();
        LogLevel GetLogLevel(int idLogLevel);
        Response Crear(LogLevel LogLevel);
        Response Existe(LogLevel LogLevel);
        Response Editar(LogLevel LogLevel);
        Response Eliminar(int idLogLevel);
    }
}
