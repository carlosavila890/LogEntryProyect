using bd.log.datos;
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bd.log.servicios.Servicios
{
   public class LogCategoryService: ILogCategoryService
    {
        #region Atributos

        private readonly LogDbContext db;


        #endregion

        #region Servicios

        #endregion


        #region Constructores

        public LogCategoryService(LogDbContext db)
        {
            this.db = db;

        }

        #endregion

        #region Metodos

        public Response Crear(LogCategory logCategory)
        {
            try
            {
                var respuesta = Existe(logCategory);
                if (!respuesta.IsSuccess)
                {
                 
                    db.Add(logCategory);
                    db.SaveChanges();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe una Brigada de Salud y Seguridad Ocupacional con igual nombre...",
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public Response Editar(LogCategory logCategory)
        {
            try
            {
                var respuesta = Existe(logCategory);
                if (!respuesta.IsSuccess)
                {
                    var respuestalogCategory = (LogCategory)respuesta.Resultado;
                    db.Update(respuestalogCategory);
                    db.SaveChanges();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                    };

                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe una Brigada de Salud y Seguridad Ocupacional con igual nombre...",
                    };
                }
            }

            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public Response Eliminar(int logCategoryId)
        {
            try
            {
                var respuestalogCategory = db.LogCategories.Find(logCategoryId);
                if (respuestalogCategory != null)
                {
                    db.Remove(respuestalogCategory);
                    db.SaveChanges();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                    };
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encontró la Brigada de Salud y Seguridad Ocupacional",
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "La Brigada de Salud y Seguridad Ocupacional no se puede eliminar, existen releciones que dependen de él...",
                };
            }
        }

        public Response Existe(LogCategory logCategory)
        {
            var respuestaPais = db.LogCategories.Where(p => p.Name == logCategory.Name).FirstOrDefault();
            if (respuestaPais != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una categoría de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Message = "No existe país...",
                Resultado = db.LogCategories.Where(p => p.LogCategoryId == logCategory.LogCategoryId).FirstOrDefault(),
            };
        }

        public LogCategory GetLogCategory(int logCategoryId)
        {
            return db.LogCategories.Where(c => c.LogCategoryId == logCategoryId).FirstOrDefault();
        }

        public List<LogCategory> GetLogCategories()
        {
            return db.LogCategories.OrderBy(p => p.Name).ToList();
        }

        #endregion
    
}
}
