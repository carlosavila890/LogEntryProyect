
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Servicios;
using bd.log.servicios.Enumeradores;

namespace bd.log.servicios.Servicios
{
    public class LogCategoryService : ILogCategoryService
    {
        #region Atributos




        #endregion

        #region Servicios
        private readonly IApiServicio apiservicio;
        #endregion

        #region Constructores

        public LogCategoryService(IApiServicio apiservicio)
        {
            this.apiservicio = apiservicio;
        }

        #endregion

        #region Metodos

        public async Task<entidades.Utils.Response> CrearAsync(LogCategory logCategory)
        {

            entidades.Utils.Response response = new entidades.Utils.Response();
            try
            {
                response = await apiservicio.InsertarAsync(logCategory,
                                                             new Uri("http://localhost:50237"),
                                                             "/api/LogCategories/InsertarLogCategory");
                if (response.IsSuccess)
                {

                    var responseLog = await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                    {
                        ApplicationName = Convert.ToString(Aplicacion.Logs),
                        ExceptionTrace = null,
                        Message = "Se ha creado una categoría",
                        UserName = "Usuario 1",
                        LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Create),
                        LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ADV),
                        EntityID = string.Format("{0} {1}", "Base de Datos:", logCategory.LogCategoryId),
                    });
                }

                return response;

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.Logs),
                    Message = "Creando Categoría",
                    ExceptionTrace = ex,
                    LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Create),
                    LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ERR),
                    UserName = "Usuario App Logs"
                });

                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }


            //try
            //{
            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        var request = JsonConvert.SerializeObject(logCategory);
            //        var content = new StringContent(request, Encoding.UTF8, "application/json");

            //        cliente.BaseAddress = new Uri("http://localhost:50237");

            //        var url = "/api/LogCategories/InsertarLogCategory";
            //        var respuesta = await cliente.PostAsync(url, content);

            //        var resultado = await respuesta.Content.ReadAsStringAsync();
            //        var response = JsonConvert.DeserializeObject<Response>(resultado);
            //        if (response.IsSuccess)
            //        {

            //        }
            //        return response;

            //    }
            //}
            //catch (Exception ex)
            //{

            //    return new Response
            //    {
            //        IsSuccess = false,
            //        Message = "Error",
            //    };
            //}
        }

        public async Task<entidades.Utils.Response> EditarAsync(string id,LogCategory logCategory)
        {
            entidades.Utils.Response response = new entidades.Utils.Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiservicio.EditarAsync(id, logCategory, new Uri("http://localhost:50237"),
                                                                 "/api/LogCategories");

                    if (response.IsSuccess)
                    {
                        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                        {
                            ApplicationName = Convert.ToString(Aplicacion.Logs),
                            EntityID = string.Format("{0} : {1}", "Base de Datos", id),
                            LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Edit),
                            LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ADV),
                            Message = "Se ha actualizado un registro",
                            UserName = "Usuario 1"
                        });
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.Logs),
                    Message = "Editando una categoría",
                    ExceptionTrace = ex,
                    LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Edit),
                    LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ERR),
                    UserName = "Usuario APP Logs"
                });
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }

            //try
            //{
            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        var request = JsonConvert.SerializeObject(logCategory);
            //        var content = new StringContent(request, Encoding.UTF8, "application/json");

            //        cliente.BaseAddress = new Uri("http://localhost:50237");

            //        var url = "/api/LogCategories/EditarLogCategory";
            //        var respuesta = await cliente.PutAsync(url, content);


            //        var resultado = await respuesta.Content.ReadAsStringAsync();
            //        var response = JsonConvert.DeserializeObject<entidades.Utils.Response>(resultado);
            //        return response;

            //    }
            //}
            //catch (Exception)
            //{
            //    return new entidades.Utils.Response
            //    {
            //        IsSuccess = false,
            //        Message = "Error",
            //    };
            //}
        }

        public async Task<entidades.Utils.Response> EliminarAsync(string id)
        {
            entidades.Utils.Response response = new entidades.Utils.Response();
            try
            {
                response = await apiservicio.EliminarAsync(id,
                                                              new Uri("http://localhost:50237"),
                                                              "/api/LogCategories");
                if (response.IsSuccess)
                {
                    await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                    {
                        ApplicationName = Convert.ToString(Aplicacion.Logs),
                        EntityID = string.Format("{0} : {1}", "BaseDatos", id),
                        Message = "Registro eliminado",
                        LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Delete),
                        LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ADV),
                        UserName = "Usuario APP Logs"
                    });
                }
                return response;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.Logs),
                    Message = "Eliminar Categoría",
                    ExceptionTrace = ex,
                    LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.Delete),
                    LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ERR),
                    UserName = "Usuario APP Logs"
                });
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }

            //var loglevel = new LogCategory();
            //try
            //{

            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        cliente.BaseAddress = new Uri("http://localhost:50237");

            //        var url = "/api/LogCategories/" + LogCategoryId;
            //        var respuesta = await cliente.DeleteAsync(url);


            //        var resultado = await respuesta.Content.ReadAsStringAsync();
            //        var response = JsonConvert.DeserializeObject<entidades.Utils.Response>(resultado);

            //        return response;


            //    }
            //}
            //catch (Exception ex)
            //{

            //    return new entidades.Utils.Response
            //    {
            //        IsSuccess = false,
            //        Message = "Error",
            //    };
            //}

        }



        public async Task<entidades.Utils.Response> SeleccionarAsync(string id)
        {

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var respuesta = await apiservicio.SeleccionarAsync<entidades.Utils.Response>(id, new Uri("http://localhost:50237"),
                                                                  "/api/LogCategories");


                    respuesta.Resultado = JsonConvert.DeserializeObject<LogCategory>(respuesta.Resultado.ToString());
                    if (respuesta.IsSuccess)
                    {
                        return respuesta;
                    }

                }

                return new entidades.Utils.Response
                {
                    IsSuccess = false,
                    Message = "Id no válido",
                };
            }
            catch (Exception ex)
            {
                return new entidades.Utils.Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }


            //var loglevel = new LogCategory();
            //try
            //{

            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        cliente.BaseAddress = new Uri("http://localhost:50237");

            //        var url = "/api/LogCategories/" + LogCategoryId;
            //        var respuesta = await cliente.GetAsync(url);


            //        var result = await respuesta.Content.ReadAsStringAsync();
            //        loglevel = JsonConvert.DeserializeObject<LogCategory>(result);

            //        return loglevel;


            //    }
            //}
            //catch (Exception)
            //{
            //    return loglevel;
            //    throw;
            //}

        }

        public async Task<List<LogCategory>> ListarLogCategories()
        {
            var lista = new List<LogCategory>();
            try
            {

                lista = await apiservicio.Listar<LogCategory>(new Uri("http://localhost:50237"), "/api/LogCategories/ListarLogCategories");
                return lista;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.Logs),
                    Message = "Listando Categorias",
                    ExceptionTrace = ex,
                    LogCategoryParametre = Convert.ToString(guardar.Enumeradores.LogCategoryParameter.NetActivity),
                    LogLevelShortName = Convert.ToString(guardar.Enumeradores.LogLevelParameter.ERR),
                    UserName = "Usuario APP Logs"
                });
                return lista = null;
            }
            //var lista = new List<LogCategory>();
            //try
            //{

            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        cliente.BaseAddress = new Uri("http://localhost:50237");

            //        var url = "/api/LogCategories/ListarLogCategories";
            //        var respuesta = await cliente.GetAsync(url);


            //        var result = await respuesta.Content.ReadAsStringAsync();
            //        lista = JsonConvert.DeserializeObject<List<LogCategory>>(result);

            //        return lista;


            //    }
            //}
            //catch (Exception)
            //{
            //    return lista;
            //    throw;
            //}
        }

        #endregion

    }
}
