 
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bd.log.servicios.Servicios
{
    public class LogCategoryService : ILogCategoryService
    {
        #region Atributos




        #endregion

        #region Servicios

        #endregion

        #region Constructores

        public LogCategoryService()
        {

        }

        #endregion

        #region Metodos

        public async Task<Response> Crear(LogCategory logCategory)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(logCategory);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogCategories/InsertarLogCategory";
                    var respuesta = await cliente.PostAsync(url, content);

                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(resultado);
                    if (response.IsSuccess)
                    {

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

        public async Task<Response> Editar(LogCategory logCategory)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(logCategory);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogCategories/EditarLogCategory";
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

        public async Task<Response> Eliminar(int LogCategoryId)
        {
            var loglevel = new LogCategory();
            try
            {

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogCategories/" + LogCategoryId;
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



        public async Task<LogCategory> GetLogCategory(int LogCategoryId)
        {
            var loglevel = new LogCategory();
            try
            {

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogCategories/" + LogCategoryId;
                    var respuesta = await cliente.GetAsync(url);


                    var result = await respuesta.Content.ReadAsStringAsync();
                    loglevel = JsonConvert.DeserializeObject<LogCategory>(result);

                    return loglevel;


                }
            }
            catch (Exception)
            {
                return loglevel;
                throw;
            }

        }

        public async Task<List<LogCategory>> GetLogCategories()
        {
            var lista = new List<LogCategory>();
            try
            {

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:61615");

                    var url = "/api/LogCategories/ListarLogCategories";
                    var respuesta = await cliente.GetAsync(url);


                    var result = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<LogCategory>>(result);

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
