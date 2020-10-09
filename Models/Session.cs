using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFormation.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Début")]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime DateDebut { get; set; }
        [Display(Name = "Fin")]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime DateFin { get; set; }
        public string Nom { get; set; }
        [Display(Name="Nombres de places")]        
        public int NbPlacesTotal { get; set; }

        public virtual Parcours Parcours { get; set; }
    }
}