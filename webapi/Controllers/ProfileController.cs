using Domain.Authorize;
using Domain.Models.Entities;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace webapi.Controllers
{

    public class ProfileController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Usuario _usuario;
        public ProfileController(UsuarioRepository usuarioRepository, Usuario usuario) 
        {
             _usuarioRepository = usuarioRepository;
             _usuario = usuario;
        }


        [Authorize("Bearer")]
        [HttpGet("api/v1/[controller]/{id}")]
        public object Get(Guid id)
        {
            var usuario = _usuarioRepository.GetUserById(id);
            var tokenCapturado = TokenCapture.CapturarToken(HttpContext);
            

            if (tokenCapturado != usuario.Token)
            return NoAuthorize.DenyAccess();

            if (tokenCapturado == usuario.Token)
            {           
                var sessaosUario = (DateTime.Now.ToLocalTime() - usuario.DataUltimoLogin.ToLocalTime()).Minutes;
                if (sessaosUario > 30)
                return NoAuthorize.LogOut();
            }

             return Ok(usuario);
        }
    }
}
    

