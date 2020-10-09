using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationFormation.Models
{
    public class StagiaireVM
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Adresse { get; set; }

        public string Téléphone { get; set; }

        public DateTime DateInscription { get; set; }

        public virtual Session SessionSouhaitee { get; set; }
        [Display(Name ="Session")]
        public virtual int IdSession { get; set; }

        public string Infos { get; set; }
        public string Statut { get; set; }
    }
}