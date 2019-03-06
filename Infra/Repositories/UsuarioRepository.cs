using Domain.Models.Entities;
using Infra.EntityConfiguration;
using Infra.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _contex;
        public UsuarioRepository(ApplicationDbContext contex) : base(contex)
          => _contex = contex;


        public Usuario GetUserByName(string Nome)
           => (from u in _contex.Usuario
               select new { u }).Where(x => x.u.Nome == Nome).Select(x => x.u).FirstOrDefault();

        public List<Usuario> GetAllUsers()
        => (from u in _contex.Usuario
           .Include(u => u.Telefones)
            select new { u }).Select(x => x.u).ToList();

    }
}