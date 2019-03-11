using System;

namespace Domain.Models.Entities
{
    public class UsuarioTelefone
    {
        public UsuarioTelefone() { }

        public UsuarioTelefone(Guid usuarioId, Guid telefoneId, Usuario usuario, Telefone telefone)
        {
            UsuarioId = usuarioId;
            TelefoneId = telefoneId;
            Usuario = usuario;
            Telefone = telefone;
        }

        public Guid TelefoneId { get; set; }
        public Telefone Telefone { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}