using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

       /* // 1 categoria tem n carros associada
        [ForeignKey("Carro")]
        public int idCarro { get; set; }
        public virtual List<Carro> Carro { get; set; } */
    }
}