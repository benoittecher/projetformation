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

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }
        public string Nom { get; set; }

        public int NbInscrits { get; set; }

        public Parcours Parcours { get; set; }
    }
}