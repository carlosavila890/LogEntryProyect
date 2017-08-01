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
   public class LogLevelService: ILogLevelService
    {
        #region Atributos

        private readonly LogDbContext db;


        #endregion

        #region Servicios

        #endregion

        #region Constructores

        public LogLevelService(LogDbContext db)
        {
            this.db = db;

        }

        #endregion

        #region Metodos

        public Response Crear(LogLevel logLevel)
        {
            try
            {
                var respuesta = Existe(logLevel);
                if (!respuesta.IsSuccess)
                {

                    db.Add(logLevel);
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

        public Response Editar(LogLevel logLevel)
        {
            try
            {
                var respuesta = Existe(logLevel);
                if (!respuesta.IsSuccess)
                {
                    var respuestalogCategory = (LogLevel)respuesta.Resultado;
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

        public Response Eliminar(int LogLevelId)
        {
            try
            {
                var respuestalogCategory = db.LogLevels.Find(LogLevelId);
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

        public Response Existe(LogLevel logLevel)
        {
            var respuestaPais = db.LogLevels.Where(p => p.Name == logLevel.Name).FirstOrDefault();
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
                Resultado = db.LogLevels.Where(p => p.LogLevelId == logLevel.LogLevelId).FirstOrDefault(),
            };
        }

        public LogLevel GetLogLevel(int LogLevelId)
        {
            return db.LogLevels.Where(c => c.LogLevelId == LogLevelId).FirstOrDefault();
        }

        public List<LogLevel> GetLogLevels()
        {
            return db.LogLevels.OrderBy(p => p.Name).ToList();
        }

        #endregion
    }
}
