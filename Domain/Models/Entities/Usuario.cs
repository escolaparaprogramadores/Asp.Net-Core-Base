using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models.Entities
{
    public class Usuario 
    {
        public Usuario() => UsuarioTelefones = new Collection<UsuarioTelefone>();

        public void GravarDataAtualização() => DataAtualizacao = DateTime.Now.ToLocalTime();

        public void GravarDataUltimoLogin() => DataUltimoLogin = DateTime.Now.ToLocalTime();

        public void GravarDataCriacao() => DataCriacao = DateTime.Now.ToLocalTime();

        public void AdicionarTelefone(Usuario obj, Usuario usuario)
        {
            foreach (var item in obj.Telefones)
            {
                UsuarioTelefone usuarioTelefoneModel = new UsuarioTelefone
                {
                    UsuarioId = obj.Id,
                    TelefoneId = item.Id,
                };
                usuario.UsuarioTelefones.Add(usuarioTelefoneModel);
            }
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataUltimoLogin { get; set; }
        public virtual ICollection<Telefone> Telefones { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsuarioTelefone> UsuarioTelefones { get; set; }

    }
  
}