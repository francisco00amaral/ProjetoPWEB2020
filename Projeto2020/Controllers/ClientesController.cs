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
    [Authorize]
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
            var encontra = db.Carros.Find(id);
            if (encontra.reservado == true)
            {
                return RedirectToAction("Index", "Carros");
            }

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

                var carro = db.Carros.Find(model.CarroId);
                carro.reservado = true;
            // arranja o custo do carro
            var custo = (from l in db.Carros
                             where l.idCarro == model.CarroId
                             select l.preco).First();
            
            var totalPreco = (model.dataPretendidaFim - model.dataPretendidaInicio).TotalDays;
            var total = custo * totalPreco;

                Reserva reserva = new Reserva
                {
                    UserId = currentUserID,
                    InicioReserva = model.dataPretendidaInicio,
                    FimReserva = model.dataPretendidaFim,
                    idCarro = model.CarroId,
                    isEntregue = false,
                    isConcluido = false,
                    isRecebido = false,
                    CustoPrevisto = ((decimal)totalPreco),
                };
            db.Entry(carro).State = EntityState.Modified;
            db.Reservas.Add(reserva);
            db.SaveChanges();
             
      
            return RedirectToAction("ListadeReservas","Clientes");
        }

        //get
        public ActionResult Entregar(int? id)
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
        public ActionResult Entregar(ReservaViewModel model)
        {
            // arranja o id do user
            var currentUserID = User.Identity.GetUserId();
            // arranja a reserva associada
            var reserva = (from l in db.Reservas
                           where l.idCarro == model.CarroId
                           select l.idReserva).First();
            var encontrado = db.Reservas.Find(reserva);
            encontrado.isRecebido = true;

            // arranja o custo do carro e acerta-o para os minutos que quis até o decidir entregar
            var custo = (from l in db.Carros
                         where l.idCarro == model.CarroId
                         select l.preco).First();

            // OCMPOR ISTO
            var totalPreco = (DateTime.Now - model.dataPretendidaInicio).TotalDays;
            var total = custo * totalPreco;
            ViewBag.Preco = total;

            encontrado.CustoPrevisto = (decimal)total;

            db.Entry(encontrado).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListadeReservas", "Clientes");
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

            var reserva = db.Reservas.Where(x => x.UserId == currentUserId).Where(l => l.isEntregue == true && l.isRecebido == false).ToList();
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