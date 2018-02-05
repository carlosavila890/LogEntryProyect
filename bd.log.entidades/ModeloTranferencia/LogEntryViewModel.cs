using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.log.entidades.ViewModels
{
   public class LogEntryViewModel
    {
        [DisplayName("Nivel")]
        public int LogLevelId { get; set;}
        [DisplayName("Path")]
        public string Message { get; set; }

        [DisplayName("Módulo")]
        public string ObjectName { get; set; }
        [DisplayName("Exepción")]
        public string ExceptionTrace { get; set; }
        [DisplayName("Nombre PC")]
        public string MachineName { get; set; }
        [DisplayName("Ip PC")]
        public string MachineIP { get; set; }
        [DisplayName("Usuario")]
        public string UserName { get; set; }
        [DisplayName("Sistema")]
        public string ApplicationName { get; set; }
        [DisplayName("Categoría")]
        public int LogCategoryId { get; set; }
        [DisplayName("Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LogDateStart { get; set; }
        [DisplayName("Fecha Fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LogDateFinish { get; set; }

        public virtual ICollection<LogLevel> LogLevels { get; set; }
        //public virtual PagedList.Core.IPagedList<LogEntry> LogEntrys { get; set; }
        public virtual ICollection<LogEntry> LogEntrys { get; set; }



    }
}
