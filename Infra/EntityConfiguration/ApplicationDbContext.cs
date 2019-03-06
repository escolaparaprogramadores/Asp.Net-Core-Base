using Domain.EntityConfiguration.Mapping;
using Domain.Models.Entities;
using Infra.EntityConfiguration.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.EntityConfiguration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)   
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioTelefone> UsuarioTelefone { get; set; }
        public DbSet<Telefone> Telefone { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new UsuarioMap());
                modelBuilder.ApplyConfiguration(new UsuarioTelefoneMap());
                modelBuilder.ApplyConfiguration(new TelefoneMap());

        }


    }
}