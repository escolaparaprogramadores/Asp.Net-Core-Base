using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models.Entities
{
    public class Usuario
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime UltimoLogin { get; set; }

        public virtual ICollection<Telefone> Telefones { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsuarioTelefone> UsuarioTelefones { get; set; }

        public Usuario() => UsuarioTelefones = new Collection<UsuarioTelefone>();

        public Usuario(DateTime dataCriacao, DateTime dataAtualizacao, DateTime ultimoLogin)
        {
            DataCriacao = DateTime.UtcNow;
        }
    }
  
}