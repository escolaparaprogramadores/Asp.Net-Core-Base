using Domain.Models.Entities;
using Infra.EntityConfiguration;
using Infra.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _contex;
        public UsuarioRepository(ApplicationDbContext contex) : base(contex)
          => _contex = contex;


        public Usuario GetUserByEmail(string email)
          => (from u in _contex.Usuario
              .Include(u => u.Telefones)
              select new { u }).Where(x => x.u.Email == email).Select(x => x.u).FirstOrDefault();

        public Usuario GetUserById(Guid id)
         => (from u in _contex.Usuario
             .Include(u => u.Telefones)
             select new { u }).Where(x => x.u.Id == id).Select(x => x.u).FirstOrDefault();

        public List<Usuario> GetAllUsers()
          => (from u in _contex.Usuario
                .Include(u => u.Telefones)
                select new { u }).Select(x => x.u).ToList();

        public bool ValidarEmail(string email)
            => (from u in _contex.Usuario
                select new { u }).Where(x => x.u.Email == email).Select(x => x.u != null ? true : false).FirstOrDefault();


    }
}