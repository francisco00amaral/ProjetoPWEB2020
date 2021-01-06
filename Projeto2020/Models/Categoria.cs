using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }

        [Display(Name ="Categoria")]
        public string nome { get; set; }
    }
}