using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplicationFormation.Models
{
    public class Parcours
    {
        [Key]
        public int Id { get; set; }

        public int NbHeures { get; set; }

        public string Designation { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}