using Domain.Models.Entities;
using Domain.SecurityHash;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class SingUpController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Usuario _usuario;
        public SingUpController(UsuarioRepository usuarioRepository, Usuario usuario)
        {
            _usuarioRepository = usuarioRepository;
            _usuario = usuario;
        }
           

        [HttpPost]
        public object Post(
            [FromBody] Usuario usuario,
            [FromServices]SigningConfigurations signingConfigurations, 
            [FromServices]TokenConfiguration tokenConfigurations)

        {

            var iSExist = _usuarioRepository.ValidarEmail(usuario.Email);
            if (!iSExist)
            {
                var senhaCriptografada = string.Empty;

                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    senhaCriptografada = new Hash(new SHA512Managed()).CriptografarSenha(usuario.Senha);
                    usuario.Senha = senhaCriptografada;
                }

                usuario.GravarDataCriacao();
                usuario.GravarDataAtualização();
                usuario.GravarDataUltimoLogin();

                var obj = _usuarioRepository.Add(usuario);

                _usuario.AdicionarTelefone(obj, usuario);
                usuario.Token = GerarToken(obj, tokenConfigurations, signingConfigurations);

                _usuarioRepository.SaveChanges();


                return Created("", obj);
            }
            else
            {
                var naoAutorizado = new ObjectResult(new { statusCode = 401, message = "Não autorizado!" }) 
                {
                    StatusCode = 401
                };

                return naoAutorizado;
            }
          

        }
 

        private string GerarToken(Usuario usuario, TokenConfiguration tokenConfigurations, SigningConfigurations signingConfigurations)
        {

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(usuario.Nome, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Nome)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return token;

        }

    }
}