using System;
using System.Net.Http;
using System.Threading.Tasks;
using bd.log.guardar.Inicializar;
using bd.log.entidades.Utils;
using Newtonsoft.Json;
using bd.log.entidades.ModeloTranferencia;

namespace bd.log.servicios.Servicios
{
    public class InicializarWebApp
    {
       
        #region Methods

        public static async Task Inicializar(string id,Uri baseAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = string.Format("{0}/{1}", "/api/Adscsists", id);
                    var uri = string.Format("{0}{1}", baseAddress, url);
                    var respuesta = await client.GetAsync(new Uri(uri));
                    
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(resultado);
                    var sistema = JsonConvert.DeserializeObject<Adscsist>(response.Resultado.ToString());
                    WebApp.BaseAddress = sistema.AdstHost;
                    AppGuardarLog.BaseAddress= sistema.AdstHost;
                }
               
            }
            catch (Exception ex)
            {

            }

        }

        #endregion
    }
}
