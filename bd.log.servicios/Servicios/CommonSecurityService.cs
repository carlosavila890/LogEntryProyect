using bd.log.datos;
using bd.log.entidades;
using bd.log.servicios.Interfaces;
using bd.log.utils;
using System;
using System.Linq;

namespace bd.log.servicios.Servicios
{
    public class CommonSecurityService : ICommonSecurityService
    {
        #region Attributes

        LogDbContext db;
        private readonly INetworkService networkService;

        #endregion

        #region Services



        #endregion

        #region Constructors

        public CommonSecurityService(LogDbContext db, INetworkService networkService)
        {
            this.db = db;
            this.networkService = networkService;
        }

        #endregion

        #region Methods

        public string GetAllErrorMsq(Exception e)
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

        public Response SaveLogEntry(string logLevelShortName, string logCategoryParametre, Exception exceptionTrace, string message, string entityID,string userName,string applicationName)
        {
            Response response;
            try
            {
                var logLevelID = db.LogLevels.FirstOrDefault(l => l.ShortName.Contains(logLevelShortName)).LogLevelId;
                var logCategoryID = db.LogCategories.FirstOrDefault(l => l.ParameterValue.Contains(logCategoryParametre)).LogCategoryId;
                
                db.Add(new LogEntry
                {
                    UserName = userName,
                    ApplicationName=applicationName,
                    ExceptionTrace = (exceptionTrace != null) ? GetAllErrorMsq(exceptionTrace) : null,
                    LogCategoryId = logCategoryID,
                    LogLevelId = logLevelID,
                    LogDate = DateTime.Now,
                    MachineIP = networkService.GetRemoteIpClientAddress(),
                    MachineName = networkService.GetClientMachineName(),
                    Message = message,
                    ObjEntityId = entityID
                });

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                response = new Response
                {
                    IsSuccess = true,
                    Message = ex.Message,
                };
                return response;
            }

            response = new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };

            return response;
        }

        #endregion
    }
}
