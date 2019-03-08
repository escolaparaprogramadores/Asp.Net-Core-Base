using Domain.Authorize;
using Domain.Models.Entities;
using Domain.SecurityHash;
using Domain.TokenGenerator;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace webapi.Controllers
{
    [Route("api/v1/[controller]")]
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
                usuario.Token = TokenGenerator.GetToken(obj, tokenConfigurations, signingConfigurations);

                _usuarioRepository.SaveChanges();


                return Created("", obj);
            }
            else
               return NoAuthorize.DenyAccess();

        }

    }
}