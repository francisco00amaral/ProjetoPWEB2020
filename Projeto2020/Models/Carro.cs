using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Carro { 
        [Key]
        public int idCarro { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public float preco { get; set; }

        public int km { get; set; }

        public int deposito { get; set; }
    }
}