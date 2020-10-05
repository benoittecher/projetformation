using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplicationFormation.Models
{
    public class Parcours
    {
        [Key]
        public int Id
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

        public List<Module> Modules
        {
            get => default;
            set
            {
            }
        }
    }
}