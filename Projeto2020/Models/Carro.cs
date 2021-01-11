using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Carro { 
        [Key]
        public int idCarro { get; set; }

        public string Marca { get; set; }   

        public string Modelo { get; set; }

        [Display(Name="Preço por dia")]
        public float preco { get; set; }

        [Required]
        [Display(Name = "Kilometros do carro")]
        [Range(1, 9999, ErrorMessage = "Km não válidos")]
        public int km { get; set; }

        [Display(Name = "Depósito")]
        [Range(1, 9999, ErrorMessage = "Depósito não valido")]
        public int deposito { get; set; }

        public bool reservado { get; set; }

        // 1 carro tem 1 categoria associada
        [ForeignKey("Categoria")]
        public int idCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        // 1 carro tem 1 empresa associada
        [ForeignKey("Empresa")]

        public int idEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}