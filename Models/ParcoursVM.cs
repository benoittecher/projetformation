using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationFormation.Models
{
    public class ParcoursVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre d'heures")]
        public int NbHeures { get; set; }

        public string Designation { get; set; }
        [Display(Name ="Module")]
        public int IdModule { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}