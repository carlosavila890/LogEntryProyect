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
        Task<List<LogCategory>> ListarLogCategories();
        Task<entidades.Utils.Response> SeleccionarAsync(string id);
        Task<entidades.Utils.Response> CrearAsync(LogCategory logCategory);
        //Response Existe(LogCategory logCategory);
        Task<entidades.Utils.Response> EditarAsync(string id, LogCategory logCategory);
        Task<entidades.Utils.Response> EliminarAsync(string id);
    }
}
