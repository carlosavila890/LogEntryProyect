using System;
using System.Collections.Generic;
using System.Text;

namespace bd.log.entidades.ViewModels
{
    public class ObjectViewModel
    {
        public DateTime? LogDateStart { get; set; }
        public DateTime? LogDateFinish { get; set; }
        public LogEntry LogEntry { get; set; }
    }
}
