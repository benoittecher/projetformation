using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationFormation.Models
{
    public class SessionVM
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Nombres de places")]
        public int NbPlacesTotal { get; set; }

        public Parcours Parcours { get; set; }
        public int IdParcours { get; set; }
    }
}