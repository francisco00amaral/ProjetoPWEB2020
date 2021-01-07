using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Verificacao
    {
        [Key]
        public int idVerificacao { get; set; }
        public string nome { get; set; }
    }
}