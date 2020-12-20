using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Carros { 
        [Key]
        public int idCarro { get; set; }
        public string Nome { get; set; }
        public float preco { get; set; }
        public int km { get; set; }
        public int deposito { get; set; }
    }
}