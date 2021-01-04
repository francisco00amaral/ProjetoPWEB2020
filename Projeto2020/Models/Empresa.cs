using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Empresa
    {
        [Key]
        public int idEmpresa { get; set; }
        public string nome { get; set; }

        [ForeignKey("ApplicationUser")]

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}   