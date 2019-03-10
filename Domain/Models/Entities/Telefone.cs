using Newtonsoft.Json;
using System;

namespace Domain.Models.Entities
{
    public class Telefone
    {
        public Telefone() { }

        public Telefone(string numero, string ddd, Usuario usuario, Guid usuarioId)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            Ddd = ddd;
            UsuarioId = usuarioId;
            Usuario = usuario;
        }

        [JsonIgnore]
        public Guid Id { get;  set; }
        public string Numero { get;  set; }
        public string Ddd { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}