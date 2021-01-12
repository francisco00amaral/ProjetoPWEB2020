using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Projeto2020.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Empresa")]
        public int? idEmpresa { get; set; }
        public Empresa Empresa { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       public ApplicationDbContext()
            : base("AMinhaDb", throwIfV1Schema: false)
        {
          
        }

        // fazer aqui o dbset dos carros, estes vao ser os nomes gerados nas tabelas da bd
        public virtual DbSet<Carro> Carros { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }

        public virtual DbSet<Verificacao> Verificacaos { get; set; }
        public virtual DbSet<CheckboxListItem> CheckboxListItems { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}