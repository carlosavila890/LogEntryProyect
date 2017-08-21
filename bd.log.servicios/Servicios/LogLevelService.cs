using bd.log.entidades;
using bd.log.guardar.Servicios;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bd.log.servicios.Servicios
{
    public class LogLevelService: ILogLevelService
    {
        #region Atributos




        #endregion

        #region Servicios
        
        #endregion

        #region Constructores

        public LogLevelService( )
        {
            
        }

        #endregion

        #region Metodos

        public async Task<Response>  Crear(LogLevel logLevel)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(logLevel);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogLevels/InsertarLogLevel";
                    var respuesta = await cliente.PostAsync(url, content);

                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(resultado);
                    if (response.IsSuccess)
                    {
                        await GuardarLogService.SaveLogEntry(
                                                   new guardar.ObjectTranfer.LogEntryTranfer
                                                   {
                                                       ApplicationName = "LogEntryProyect",
                                                       EntityID = "",
                                                       ExceptionTrace = new Exception(),
                                                       LogCategoryParametre = "Critical",
                                                       LogLevelShortName = "ERR",
                                                       Message = "se ha insertado",
                                                       UserName = "Paul",
                                                   });

                    }
                    return response;

                }
            }
            catch (Exception ex)
            {
             

                return new Response
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }

        public async Task<Response> Editar(LogLevel logLevel)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(logLevel);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogLevels/EditarLogLevel";
                    var respuesta = await cliente.PutAsync(url, content);


                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(resultado);
                    return response;

                }     
            }
             catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }

        public async Task<Response> Eliminar(int LogLevelId)
        {
            var loglevel = new LogLevel();
            try
            {

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogLevels/" + LogLevelId;
                    var respuesta = await cliente.DeleteAsync(url);


                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(resultado);

                    return response;
                 

                }
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }

        }



        public async Task<LogLevel> GetLogLevel(int LogLevelId)
        {
            var loglevel = new LogLevel();
            try
            {

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogLevels/" + LogLevelId;
                    var respuesta = await cliente.GetAsync(url);


                    var result = await respuesta.Content.ReadAsStringAsync();
                    loglevel = JsonConvert.DeserializeObject<LogLevel>(result);

                    return loglevel;


                }
            }
            catch (Exception)
            {
                return loglevel;
                throw;
            }

        }

        public async Task<List<LogLevel>> GetLogLevels()
        {
            var lista = new List<LogLevel>();
            try
            {
               
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogLevels/ListarLogLevels";
                    var respuesta = await cliente.GetAsync(url);

                   
                    var result = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<LogLevel>>(result);

                    return lista;
                   

                }
            }
            catch (Exception)
            {
                return lista;
                throw;
            }
        }

        #endregion
    }
}
