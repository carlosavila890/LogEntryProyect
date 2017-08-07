 
using bd.log.entidades;
using bd.log.entidades.ObjectTranfer;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bd.log.servicios.Servicios
{
    public class CommonSecurityService : ICommonSecurityService
    {
        #region Attributes
        #endregion

        #region Services



        #endregion

        #region Constructors

        public CommonSecurityService( )
        {
          
        }

        #endregion

        #region Methods

        public async Task<Response> SaveLogEntry(LogEntryTranfer logEntryTranfer)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(logEntryTranfer);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    cliente.BaseAddress = new Uri("http://localhost:58471");

                    var url = "/api/LogEntries/InsertarLonEntry";
                    var respuesta = await cliente.PostAsync(url, content);

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

        #endregion
    }
}
