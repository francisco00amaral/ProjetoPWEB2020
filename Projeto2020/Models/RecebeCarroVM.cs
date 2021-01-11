using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class RecebeCarroVM
    {
        public int CarroViewModelID { get; set; }
        [Required]
        [Display(Name = "Kilometros do carro")]
        [Range(1, 9999, ErrorMessage = "Km não válidos")]

        public int km { get; set; }

        [Display(Name = "Depósito")]
        [Range(1, 9999, ErrorMessage = "Depósito não valido")]
        public int deposito { get; set; }

        public int CarroId { get; set; }
        public List<CheckboxListItem> verifications { get; set;}
        public string imagem { get; set; }
        public RecebeCarroVM()
        {
            verifications = new List<CheckboxListItem>();
        }
    }
}