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
    [Authorize(Roles ="Funcionários")]
    public class FuncionariosController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Funcionarios
        public ActionResult AreaFunc()
        {
            return View();
        }

        public ActionResult JaReservado()
        {
            return View();
        }

        public ActionResult ConfirmaReserva(int ?id)
        {
            if(id != null)
            {
                CarroViewModel model = new CarroViewModel()
                {
                    CarroId = (int)id,
                };
                return View(model);
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaReserva(CarroViewModel model)
        {
            var currentUserID = User.Identity.GetUserId();
            var carro = db.Carros.Find(model.CarroId);

            // mete os valores do carro com os que o funcionario meteu no form do get
            carro.deposito = model.deposito;
            carro.km = model.km;
            // encontrar a reserva e meter o isEntregue a true;
            var reserva = (from l in db.Reservas
                           where l.idCarro == model.CarroId
                           select l.idReserva).First();

            var encontrado = db.Reservas.Find(reserva);
            encontrado.isEntregue = true;

            db.Entry(carro).State = EntityState.Modified;
            db.Entry(encontrado).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("ReservasPorConfirmar", "Funcionarios");
        }
        
        public ActionResult ReservasConfirmadas()
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas confirmadas(is entregue == true)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == true).ToList();
            return View(reserva);
        }

        public ActionResult ReservasPorConfirmar()
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == false).ToList();
            return View(reserva);
        }

        // reservas recebidas que ainda nao foram recebidas pelo funcionario
        public ActionResult ReservasPorReceber()
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == true && l.isEntregue == true && l.isConcluido == false).ToList();
            return View(reserva);
        }
        // reservas recebidas e que ja foram confirmadas pelo funcionario
        public ActionResult ReservasRecebidas()
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == false && l.isEntregue == true && l.isConcluido == true).ToList();
            return View(reserva);
        }
    }
}