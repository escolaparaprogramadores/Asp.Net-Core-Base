using Domain.Authorize;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace webapi.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProfileController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public ProfileController(UsuarioRepository usuarioRepository) => _usuarioRepository = usuarioRepository;


     
        [HttpGet("{id}")]
        public object Get(Guid id)
        {
            var usuario = _usuarioRepository.GetUserById(id);
            var tokenCapturado = TokenCapture.CapturarToken(HttpContext);
            
            if(usuario != null)
            {
                if (tokenCapturado != usuario.Token || string.IsNullOrEmpty(tokenCapturado))
                    return NoAuthorize.DenyAccess();

                if (tokenCapturado == usuario.Token)
                {
                    var sessaosUario = (DateTime.Now.ToLocalTime() - usuario.DataUltimoLogin.ToLocalTime()).Minutes;
                    if (sessaosUario > 30)
                        return NoAuthorize.LogOut();
                }
            }
            

             return Ok(usuario);
        }
    }
}
    

