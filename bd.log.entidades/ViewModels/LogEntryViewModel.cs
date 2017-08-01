using System;
using System.Collections.Generic;
using System.Text;

namespace bd.log.entidades.ViewModels
{
   public class LogEntryViewModel
    {
        public int LogLevelId { get; set;}
        public virtual ICollection<LogLevel> LogLevels { get; set; }
        public virtual ICollection<LogEntry> LogEntrys { get; set; }

    }
}
