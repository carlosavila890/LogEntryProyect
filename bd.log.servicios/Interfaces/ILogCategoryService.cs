using bd.log.entidades;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bd.log.servicios.Interfaces
{
   public interface ILogCategoryService
    {
        Task<List<LogCategory>> GetLogCategories();
        Task<LogCategory> GetLogCategory(int idLogCategory);
        Task<Response> Crear(LogCategory logCategory);
        //Response Existe(LogCategory logCategory);
        Task<Response> Editar(LogCategory logCategory);
        Task<Response> Eliminar(int idLogCategory);
    }
}
