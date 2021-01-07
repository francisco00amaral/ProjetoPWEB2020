﻿using System;
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
        public int idCarro { get; set; }

        [Display(Name = "Data de inicio do aluguer")]
        public DateTime InicioReserva { get; set; }

        [Display(Name = "Data do fim do aluguer")]
        public DateTime FimReserva { get; set; }

        // 1 compra e de um produto, e um produto pode ter multiplas compras
        public virtual Carro Carro { get; set; }

        [ForeignKey("ApplicationUser")]
        public string idCliente { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}