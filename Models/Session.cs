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
        public int Id
        {
            get => default;
            set
            {
            }
        }

        public DateTime DateDebut
        {
            get => default;
            set
            {
            }
        }

        public DateTime DateFin
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

        public int NbInscrits
        {
            get => default;
            set
            {
            }
        }

        public Parcours Parcours
        {
            get => default;
            set
            {
            }
        }
    }
}