﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFormation.Models
{
    public class Stagiaire
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Adresse { get; set; }

        public string Téléphone { get; set; }

        public DateTime DateInscription { get; set; }

        public Session SessionSouhaitee { get; set; }

        public string Infos { get; set; }
        public string Statut { get; set; }
    }
}