using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Verificacao
    {
        [Key]
        public int idVerificacao { get; set; }
        public string nome { get; set; }

        // 1 verificacao ta associada a categoria
        [ForeignKey("Categoria")]
        public int idCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}