using bd.log.entidades;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bd.log.servicios.Interfaces
{
    public interface ILogLevelService
    {
        Task<List<LogLevel>> GetLogLevels();
        Task<LogLevel> GetLogLevel(int idLogLevel);
        Task<Response> Crear(LogLevel LogLevel);
        //Response Existe(LogLevel LogLevel);
        Task<Response> Editar(LogLevel LogLevel);
        Task<Response> Eliminar(int idLogLevel);
    }
}
