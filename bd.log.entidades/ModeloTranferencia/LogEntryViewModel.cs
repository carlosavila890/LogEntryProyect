using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.log.entidades.ViewModels
{
   public class LogEntryViewModel
    {
        public int LogLevelId { get; set;}
        public string Message { get; set; }
        public string ObjectName { get; set; }
        public string ExceptionTrace { get; set; }
        public string MachineName { get; set; }
        public string MachineIP { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public int LogCategoryId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LogDateStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LogDateFinish { get; set; }
        public virtual ICollection<LogLevel> LogLevels { get; set; }
        //public virtual PagedList.Core.IPagedList<LogEntry> LogEntrys { get; set; }
        public virtual ICollection<LogEntry> LogEntrys { get; set; }



    }
}
