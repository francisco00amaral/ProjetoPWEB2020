using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class ReservaViewModel : IValidatableObject
    {
        public int ReservaViewModelId { get; set; }
        [Required]
        [Display(Name = "Categoria de Veículo")]
        public int CategoriaVeiculo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataPretendidaInicio { get; set; }

        public int CarroId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Entrega")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataPretendidaFim { get; set; }


        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            //Verifica se a horqa do fim é antes da hora do inicio
            if (dataPretendidaInicio > dataPretendidaFim)
            {
                yield return new ValidationResult("Data do fim deve ser mais tarde ou igual à data do início.");
            }
        }
    }
}