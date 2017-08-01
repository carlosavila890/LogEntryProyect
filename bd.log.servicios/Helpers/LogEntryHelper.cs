using bd.log.datos;
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bd.log.servicios.Helpers
{
  public static class LogEntryHelper
    {


        #region Methods

        public static string GetAllErrorMsq(Exception e)
        {
            string strError = string.Empty;

            //Mientras la Excepción interior no sea igual a null, se obtiene el mensaje asociado a la misma
            //y se agrega a la lista de mensajes asociados a la Excepciones exteriores
            while (e != null)
            {
                strError += e.Message + Environment.NewLine;
                e = e.InnerException;
            }
            return strError;
        }
        #endregion
    }
}
