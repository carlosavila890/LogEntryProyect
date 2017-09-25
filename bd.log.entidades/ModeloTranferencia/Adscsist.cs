using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bd.log.entidades.ModeloTranferencia
{
    public partial class Adscsist
    {
        public string AdstSistema { get; set; }

        public string AdstDescripcion { get; set; }

        public string AdstTipo { get; set; }

        public string AdstHost { get; set; }

        public string AdstBdd { get; set; }
    }
}
