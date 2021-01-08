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
    public class FuncionariosController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Funcionarios
        public ActionResult Index()
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

            // para o funcionario quando vai entregar meter o deposito e o km
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

            return RedirectToAction("ReservasPorConfirmar", "Funcionarios");
        }
        private void atualizaReservas()
        {
            var Reservas = db.Reservas;

            Reserva reserva;

            foreach (var a in Reservas.ToList())
            {
                if (a.FimReserva < DateTime.Now && a.isRecebido == false)
                {
                    //Remove o aluguer que terminou
                    reserva = db.Reservas.Find(a.idReserva);
                    reserva.isRecebido = true;
                    db.Entry(reserva).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
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
    }
}