using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto2020.Models
{
    public static class Managers
    {
        public static List<Verificacao> GetAll()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            {
                return db.Verificacaos.OrderBy(x => x.nome).ToList();
            }
        }
    }
}