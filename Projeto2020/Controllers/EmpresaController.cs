using Microsoft.AspNet.Identity;
using Projeto2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto2020.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class EmpresaController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }


        // GET
        public ActionResult NovaVerificacao()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaVerificacao([Bind(Include ="idVerificacao,nome,idCategoria")] Verificacao verificacao)
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            var empresa = db.Empresas.Find(empresaId); // encontra-me na db esta empresa
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nome");

            empresa.Verificacoes.Add(verificacao);
            db.Verificacaos.Add(verificacao);
            db.SaveChanges();

            return RedirectToAction("MyVerificacao");
        }
        public ActionResult MyVerificacao()
        {
            string currentUserId = User.Identity.GetUserId();
            var empresaId = (from l in db.Users
                             where l.Id == currentUserId
                             select l.idEmpresa).First(); // selecionar o id da empresa que corresponde com este user
            // seleciona-me todas as reservas confirmadas(is entregue == true)

            // da-me todas as verificacoes desta empresa
            List<Verificacao> verificacoes = (from l in db.Empresas
                                              where l.idEmpresa == empresaId
                                              select l.Verificacoes).SingleOrDefault();
            return View(verificacoes);
        }


    }
}