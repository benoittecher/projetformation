using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace WebApplicationFormation.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Nom du module")]
        public string Resume { get; set; }

        public string Logo { get; set; }
        [Display(Name = "Prérequis")]
        public string Prerequis { get; set; }
        [Display(Name = "Nombre d'heures")]
        public int NbHeures { get; set; }

        public string Objectifs { get; set; }
        public virtual ICollection<Parcours> Parcours { get; set; }
    }
}