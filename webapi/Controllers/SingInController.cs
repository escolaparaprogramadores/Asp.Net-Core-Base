
using Domain.Authorize;
using Domain.Models.Entities;
using Domain.SecurityHash;
using Domain.TokenGenerator;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;

namespace webapi.Controllers
{
    [Route("api/v1/[controller]")]
    public class SingInController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public SingInController(UsuarioRepository usuarioRepository) => _usuarioRepository = usuarioRepository;
   


        [HttpPost]
        public object Post([FromBody]Usuario usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfiguration tokenConfigurations)
        {
            Usuario usuarioBase;

            if (!string.IsNullOrEmpty(usuario.Email) && !string.IsNullOrEmpty(usuario.Senha))
            {
                var senhaCriptografada = string.Empty;
                senhaCriptografada = new Hash(new SHA512Managed()).CriptografarSenha(usuario.Senha);
                usuario.Senha = senhaCriptografada;               

                usuarioBase = _usuarioRepository.GetUserByEmail(usuario.Email);
                if (usuarioBase == null)
                    return NoAuthorize.DenyAccess();
                if (usuario.Email == usuarioBase.Email && senhaCriptografada != usuarioBase.Senha)
                    return NoAuthorize.DenyAccess();
            }
            else
                return NoAuthorize.DenyAccess();

                usuarioBase.Token = TokenGenerator.GetToken (usuarioBase, tokenConfigurations, signingConfigurations);
                usuarioBase.DataUltimoLogin = DateTime.Now.ToLocalTime();
                _usuarioRepository.Update(usuarioBase);
                return Ok(usuarioBase);
        }

    }
}