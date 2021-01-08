using Microsoft.AspNet.Identity;
using Projeto2020.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto2020.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MyReservas()
        {
            return View();
        }

        //get
        public ActionResult Reservar(int? id)
        {
            if (id != null)
            {
                ReservaViewModel model = new ReservaViewModel()
                {
                    CarroId = (int)id,
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservar(ReservaViewModel model)
        {
                // arranja o id do user
                var currentUserID = User.Identity.GetUserId();
                // arranja o custo do carro
                var custo = (from l in db.Carros
                             where l.idCarro == model.CarroId
                             select l.preco).First();

            var totalPreco = (model.dataPretendidaFim - model.dataPretendidaInicio).TotalMinutes;
            var total = custo * totalPreco;

                Reserva reserva = new Reserva
                {
                    UserId = currentUserID,
                    InicioReserva = model.dataPretendidaInicio,
                    FimReserva = model.dataPretendidaFim,
                    idCarro = model.CarroId,
                    isEntregue = false,
                    isRecebido = false,
                    CustoPrevisto = ((decimal)totalPreco),
                };
            db.Reservas.Add(reserva);
            db.SaveChanges();
             
      
            return RedirectToAction("ListadeReservas","Clientes");
        }

        // todas as reservas feitas por o user, confirmadas e nao confirmadas
        public ActionResult ListadeReservas()
        {
            string currentUserId = User.Identity.GetUserId();
            var reserva = db.Reservas.Where(x => x.UserId == currentUserId).ToList();

            return View(reserva);
        }
        // todas as reservas feitas pelo user confirmadas
        public ActionResult ReservasYes()
        {
            string currentUserId = User.Identity.GetUserId();

            var reserva = db.Reservas.Where(x => x.UserId == currentUserId).Where(l => l.isEntregue == true).ToList();
            return View(reserva);
        }
        // todas as reservas feitas pelo user ainda nao confirmadas
        public ActionResult ReservasNo()
        {
            string currentUserId = User.Identity.GetUserId();

            var reserva = db.Reservas.Where(x => x.UserId == currentUserId).Where(l => l.isEntregue == false).ToList();
            return View(reserva);
        }
    }
}