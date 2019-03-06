using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Entities;
using Infra.Repositories;
using System.Collections.Generic;
using System;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuarioController(UsuarioRepository usuarioRepository) => _usuarioRepository = usuarioRepository;



        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            var obj = _usuarioRepository.Add(usuario);
            foreach (var item in obj.Telefones)
            {
                UsuarioTelefone usuarioTelefoneModel = new UsuarioTelefone
                {
                    UsuarioId = obj.Id,
                    TelefoneId = item.Id,
                };
                usuario.UsuarioTelefones.Add(usuarioTelefoneModel);
            }
                _usuarioRepository.SaveChanges();
                 return Created("", obj);
        }
        
        

        [Authorize("Bearer")]
        [HttpGet]
        public IEnumerable<Usuario> Get() => _usuarioRepository.GetAllUsers();

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public Usuario Get(int id) => _usuarioRepository.GetById(id);

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario != null)
            {
                _usuarioRepository.Remove(usuario);
            }

            return Ok(new { menssagem = "Removido com sucesso!" });
        }

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Usuario obj, int id)
        {

            var usuario = _usuarioRepository.GetById(id);
            usuario.Nome = obj.Nome;
            usuario.Senha = obj.Senha;

            _usuarioRepository.Update(usuario);
            return Ok(usuario);
        }


    }
}