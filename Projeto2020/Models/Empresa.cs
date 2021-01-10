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

        [Display(Name = "Empresa")]
        public string nome { get; set; }

        //cada empresa tem uma lista de verificacoes diferentes
        public List<Verificacao> Verificacoes { get; set; }

        public Empresa()
        {
            Verificacoes = new List<Verificacao>();
        }

    }
}   