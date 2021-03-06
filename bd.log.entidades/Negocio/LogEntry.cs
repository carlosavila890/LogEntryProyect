﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.log.entidades
{
    public class LogEntry
    {
        public int LogEntryId { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime LogDate { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Id Entidad")]
        public string ObjectName { get; set; }

        [Display(Name = "Objeto Anterior")]
        public string ObjectPrevious { get; set; }

 
        [Display(Name = "Objeto Nuevo")]
        public string ObjectNext { get; set; }

        [StringLength(4096)]
        [Display(Name = "Excepcion")]
        public string ExceptionTrace { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Nombre Dispositivo")]
        public string MachineName { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "IP Dispositivo")]
        public string MachineIP { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Nombre Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(1024)]
        [Display(Name = "Aplicacion")]
        public string ApplicationName { get; set; }

        [Required]
        public int LogLevelId { get; set; }

        [Required]
        public int LogCategoryId { get; set; }

       

        public virtual LogLevel LogLevel { get; set; }

        public virtual LogCategory LogCategory { get; set; }
    }
}
