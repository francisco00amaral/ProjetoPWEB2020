using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public class Reserva
    {
        [Key]
        public int idReserva { get; set; }
        public int idCarro { get; set; }
        public string email { get; set; }

        [Display(Name = "Data da Reserva")]
        public DateTime DataReserva { get; set; }

        // 1 compra e de um produto, e um produto pode ter multiplas compras
        public virtual Carro Carro { get; set; }
    }
}