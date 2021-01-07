using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto2020.Models;

namespace Projeto2020.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Reservas
        public ActionResult Index()
        {
            // isto n é assim vou ter de  compor...
            return View(db.Reservas.ToList());
        }

        // so lista as reservas feita a empresa dele
        public ActionResult IndexFuncionarios()
        {
            var currentUserID = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserID
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user

            var resEmp = db.Reservas.Where(i => i.Carro.idEmpresa == empresaId);

            return View(resEmp.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Reservas(int? id)
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

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReserva,idCarro,nome")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reserva);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategoria,nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
            db.SaveChanges();
            return RedirectToAction("Index");
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
