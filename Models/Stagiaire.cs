using System;
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
        public int Id
        {
            get => default;
            set
            {
            }
        }

        public string Nom
        {
            get => default;
            set
            {
            }
        }

        public string Prenom
        {
            get => default;
            set
            {
            }
        }

        public string Mail
        {
            get => default;
            set
            {
            }
        }

        public string Adresse
        {
            get => default;
            set
            {
            }
        }

        public string Téléphone
        {
            get => default;
            set
            {
            }
        }

        public DateTime DateInscription
        {
            get => default;
            set
            {
            }
        }

        public Session SessionSouhaitee
        {
            get => default;
            set
            {
            }
        }

        public string Infos
        {
            get => default;
            set
            {
            }
        }

        public string Statut
        {
            get => default;
            set
            {
            }
        }
    }
}