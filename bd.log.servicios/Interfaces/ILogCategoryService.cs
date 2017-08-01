using bd.log.entidades;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.log.servicios.Interfaces
{
   public interface ILogCategoryService
    {
        List<LogCategory> GetLogCategories();
        LogCategory GetLogCategory(int idLogCategory);
        Response Crear(LogCategory logCategory);
        Response Existe(LogCategory logCategory);
        Response Editar(LogCategory logCategory);
        Response Eliminar(int idLogCategory);
    }
}
