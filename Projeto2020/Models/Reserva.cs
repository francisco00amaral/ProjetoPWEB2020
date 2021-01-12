using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Reserva
    {
        [Key]
        public int idReserva { get; set; }
        [ForeignKey("Carro")]
        public int idCarro { get; set; }

        [Display(Name = "Data de inicio da reserva")]
        public DateTime InicioReserva { get; set; }

        [Display(Name = "Data do fim da reserva")]
        public DateTime FimReserva { get; set; }

        [Display(Name = "Custo previsto(€)")]
        public decimal CustoPrevisto { get; set; }
        public bool isEntregue { get; set; }

        public bool isConcluido { get; set; }
        public bool isRecebido { get; set; }

        public string imagemDefeito { get; set; }
        public virtual Carro Carro { get; set; }

        // 1 reserva ta associado a 1 user
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}