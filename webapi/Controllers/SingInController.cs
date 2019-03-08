
using Domain.Models.Entities;
using Domain.SecurityHash;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class SingInController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public SingInController(UsuarioRepository usuarioRepository) => _usuarioRepository = usuarioRepository;
   


        [HttpPost]
        public object Post([FromBody]Usuario usuario)
        {
            Usuario usuarioBase;

            if (!string.IsNullOrEmpty(usuario.Email) && !string.IsNullOrEmpty(usuario.Senha))
            {
                var senhaCriptografada = string.Empty;
                senhaCriptografada = new Hash(new SHA512Managed()).CriptografarSenha(usuario.Senha);
                usuario.Senha = senhaCriptografada;               

                usuarioBase = _usuarioRepository.GetUserByEmail(usuario.Email);
                if (usuarioBase == null)
                {
                    var naoAutorizado = new ObjectResult(new { statusCode = 401, message = "Usuário e/ou senha inválidos!" })
                    {
                        StatusCode = 401
                    };
                    return naoAutorizado;
                }
                if (usuario.Email == usuarioBase.Email && senhaCriptografada != usuarioBase.Senha)
                {
                    var naoAutorizado = new ObjectResult(new { statusCode = 401, message = "Usuário e/ou senha inválidos!" })
                    {
                        StatusCode = 401
                    };
                    return naoAutorizado;
                }          
            }
            else
            {
                var naoAutorizado = new ObjectResult(new { statusCode = 401, message = "Usuário e/ou senha inválidos!" })
                {
                    StatusCode = 401 
                };
                return naoAutorizado;
            }


                usuarioBase.DataUltimoLogin = DateTime.Now.ToLocalTime();
                _usuarioRepository.Update(usuarioBase);
                return Ok(usuarioBase);
        }
    }
}