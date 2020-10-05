using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFormation.Models
{
    public class Module
    {
        [Key]
        public int Id
        {
            get => default;
            set
            {
            }
        }

        public string Resume
        {
            get => default;
            set
            {
            }
        }

        public string Logo
        {
            get => default;
            set
            {
            }
        }

        public string Prerequis
        {
            get => default;
            set
            {
            }
        }

        public int NbHeures
        {
            get => default;
            set
            {
            }
        }

        public string Objectifs
        {
            get => default;
            set
            {
            }
        }
    }
}