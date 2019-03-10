using Domain.Authorize;
using Domain.Models.Entities;
using Domain.SecurityHash;
using Domain.TokenGenerator;
using Infra.Repositories;
using Infra.RepositoriesADO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace webapi.Controllers
{
    [Route("api/")]
    public class SingUpController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly UsuarioRepositoryADO _usuarioRepositoryADO;    
        public SingUpController(UsuarioRepository usuarioRepository, UsuarioRepositoryADO usuarioRepositoryADO)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioRepositoryADO = usuarioRepositoryADO;  
        }

        [HttpPost("v1/[controller]")]
        public object Post(
        [FromBody] Usuario usuario,
        [FromServices]SigningConfigurations signingConfigurations,
        [FromServices]TokenConfiguration tokenConfigurations)
        {
            var iSExist = _usuarioRepository.ValidarEmail(usuario.Email);
            if (!iSExist)
            {
                var obj = new Usuario(usuario, new Hash(new SHA512Managed()).CriptografarSenha(usuario.Senha), TokenGenerator.GetToken(usuario, tokenConfigurations, signingConfigurations));
                _usuarioRepository.Add(obj);

                return Created("", obj);
            }
            else
                return NoAuthorize.DenyAccess();
        }


        [HttpPost("v2/[controller]")]
        public object PostADO(
        [FromBody] Usuario usuario,
        [FromServices]SigningConfigurations signingConfigurations,
        [FromServices]TokenConfiguration tokenConfigurations)
        {

            var iSExist = _usuarioRepositoryADO.ValidarEmail(usuario.Email);
            if (!iSExist)
            {
                 var obj = new Usuario(usuario, new Hash(new SHA512Managed()).CriptografarSenha(usuario.Senha), TokenGenerator.GetToken(usuario, tokenConfigurations, signingConfigurations));
                    _usuarioRepositoryADO.AddUsuario(obj);

                foreach (var item in usuario.Telefones)
                {
                    Telefone telefone = new Telefone(item.Numero, item.Ddd, obj, obj.Id);
                    _usuarioRepositoryADO.AddTelefone(telefone);

                    UsuarioTelefone usuarioTelefone = new UsuarioTelefone(obj.Id, telefone.Id);
                    _usuarioRepositoryADO.AddTelefoneUsuario(usuarioTelefone);
                }

                return Created("", obj);
            }
            else
                return NoAuthorize.DenyAccess();

        }

    }
}