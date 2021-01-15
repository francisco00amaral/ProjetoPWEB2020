using Microsoft.AspNet.Identity;
using Projeto2020.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto2020.Controllers
{
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
        private void atualizaReservas()
        {
            var Reservas = db.Reservas;

            Reserva reserva;
            Carro carro;

            foreach (var a in Reservas.ToList())
            {
                if (a.FimReserva < DateTime.Now && a.isConcluido == false)
                {
                    //Remove o aluguer que terminou
                    reserva = db.Reservas.Find(a.idReserva);
                    reserva.isEntregue = true;
                    reserva.isRecebido = true;
                    reserva.isConcluido = true;
                    //Encontra o carro e da reset a flag para o deixar reservar again
                    carro = db.Carros.Find(a.idCarro);
                    carro.reservado = false;

                    db.Entry(carro).State = EntityState.Modified;

                    db.Entry(reserva).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public ActionResult ConfirmaReserva(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id != null)
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserID = User.Identity.GetUserId();
            var carro = db.Carros.Find(model.CarroId);

            // mete os valores do carro com os que o funcionario meteu no form do get
            carro.deposito = model.deposito;
            carro.km = model.km;
            // encontrar a reserva e meter o isEntregue a true;
            var reserva = (from l in db.Reservas
                           where l.idCarro == model.CarroId && l.isConcluido == false && l.isEntregue == false
                           select l.idReserva).First();

            var encontrado = db.Reservas.Find(reserva);
            encontrado.isEntregue = true;

            db.Entry(carro).State = EntityState.Modified;
            db.Entry(encontrado).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("ReservasPorConfirmar", "Funcionarios");
        }

        // get
        public ActionResult Recebe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id != null)
            {
                RecebeCarroVM model = new RecebeCarroVM()
                {
                    CarroId = (int)id,
                };
                // arranja-me o id da categoria deste veiculo
                var categoria = (from l in db.Carros
                                where l.idCarro == id
                                select l.idCategoria).First();

                var currentUserID = User.Identity.GetUserId();
                // selecionar o id da empresa que corresponde com este user
                var empresaId = (from l in db.Users
                                 where l.Id == currentUserID
                                 select l.idEmpresa).First();
                // da-me a lista de verificacoes para esta categoria
                var lista = (from l in db.Empresas
                             where l.idEmpresa == empresaId
                             select l.Verificacoes.Where(f => f.Categoria.idCategoria == categoria)).SingleOrDefault();

                var checkBoxListItems = new List<CheckboxListItem>();

                foreach (var verifs in lista)
                {
                    checkBoxListItems.Add(new CheckboxListItem()
                    {
                        ID = verifs.idVerificacao,
                        Display = verifs.nome,
                        IsChecked = false   //On the add view, no genres are selected by default
                    });
                }
                model.verifications = checkBoxListItems;
                return View(model);
            }

            return RedirectToAction("ReservasPorReceber", "Funcionarios");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recebe(RecebeCarroVM model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUserID = User.Identity.GetUserId();
            var carro = db.Carros.Find(model.CarroId);

            // mete os valores do carro com os que o funcionario meteu no form do get
            carro.deposito = model.deposito;
            carro.km = model.km;
            // falta aqui o upload da foto...

            // encontrar a reserva e meter o isEntregue a true;
            var reserva = (from l in db.Reservas
                           where l.idCarro == model.CarroId && l.isConcluido == false
                           select l.idReserva).First();

            carro.reservado = false;

            var encontrado = db.Reservas.Find(reserva);
            string nomeDoFicheiroAguardar = "P_" + encontrado.idReserva.ToString();

            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    nomeDoFicheiroAguardar += Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/uploads"), nomeDoFicheiroAguardar));
                    encontrado.imagemDefeito = nomeDoFicheiroAguardar;

                }
            }
            encontrado.isRecebido = true;
            encontrado.isConcluido = true;

            db.Entry(carro).State = EntityState.Modified;
            db.Entry(encontrado).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("ReservasRecebidas", "Funcionarios");
        }

        public ActionResult ReservasConfirmadas()
        {
            atualizaReservas();

            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas confirmadas(is entregue == true)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == true && l.isRecebido == false).ToList();
            return View(reserva);
        }

        public ActionResult ReservasPorConfirmar()
        {
            atualizaReservas();

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
            atualizaReservas();

            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == true && l.isRecebido == true && l.isConcluido == false).ToList();
            return View(reserva);
        }
        // reservas recebidas e que ja foram confirmadas pelo funcionario
        public ActionResult ReservasRecebidas()
        {
            atualizaReservas();

            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)

            var reserva = db.Reservas.Where(x => x.Carro.idEmpresa == empresaId).Where(l => l.isEntregue == true && l.isRecebido == true && l.isConcluido == true).ToList();
            return View(reserva);
        }

        // GET: Funcioarios/Details/5

        public ActionResult DetalhesConfirmadas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }
    }
}