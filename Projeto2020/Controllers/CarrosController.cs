using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Projeto2020.Models;

namespace Projeto2020.Controllers
{
    public class CarrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carros
        public ActionResult Index()
        {

            atualizaReservas();
            // da-me as carros reservados que ainda nao tao entregues
            var carrinhos = (from l in db.Carros
                             where l.reservado == false
                             select l);
            
            var carros = db.Carros.Include(c => c.Categoria).Include(c => c.Empresa);
            return View(carrinhos.ToList());
        }

        private void atualizaReservas()
        {
            var Reservas = db.Reservas;

            Reserva reserva;
            Carro carro;

            foreach (var a in Reservas.ToList())
            {
                if (a.FimReserva < DateTime.Now && a.isConcluido == false && a.isRecebido == false)
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


        [Authorize(Roles ="Empresa,Funcionário")]
        public ActionResult IndexEmpresa()
        {
            string currentUserID = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserID
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            //var carros = db.Carros.Where(c=> c.idEmpresa == empresaId).Include(c => c.Categoria).Include(c => c.idEmpresa);
            var carros = db.Carros.Where(u => u.idEmpresa == empresaId);
            return View(carros.ToList());
        }

        // GET: Carros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carros.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        [Authorize(Roles = "Empresa")]
        // GET: Carros/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome");
            return View();
        }

        // POST: Carros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCarro,Marca,Modelo,preco,km,deposito,idCategoria")] Carro carro,int id = 0)
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas por confirmar(is entregue == false)            if (ModelState.IsValid)
            carro.idEmpresa = (int)empresaId;
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome", carro.idCategoria);
                if (ModelState.IsValid)
                {

                    db.Carros.Add(carro);
                    db.SaveChanges();
                    if (id == 1)
                    {
                        return RedirectToAction("IndexEmpresa");
                    }
                    return RedirectToAction("Index");
                }
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "nome", carro.idEmpresa); 
            return View(carro);
        }

           
            

        // GET: Carros/Edit/5
        [Authorize(Roles = "Empresa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carros.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome", carro.idCategoria);
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "nome", carro.idEmpresa);
            return View(carro);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCarro,Marca,Modelo,preco,km,deposito,idCategoria,idEmpresa")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome", carro.idCategoria);
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "nome", carro.idEmpresa);
            return View(carro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
