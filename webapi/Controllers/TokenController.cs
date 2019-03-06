
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Domain.Models.Entities;
using Infra.Repositories;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public TokenController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        public object Post(
             [FromBody]Usuario usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfiguration tokenConfigurations)
        {

            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrEmpty(usuario.Nome))
            {
                var usuarioBase = _usuarioRepository.GetUserByName(usuario.Nome);
                credenciaisValidas = (usuarioBase != null &&
                    usuario.Nome == usuarioBase.Nome &&
                    usuario.Senha == usuarioBase.Senha);
            }

            if (credenciaisValidas)
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

                return new
                {
                    statusCode = 200,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Falha ao autenticar"
                });
            }
        }
    }
}