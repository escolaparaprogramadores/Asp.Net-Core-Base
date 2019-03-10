using System;

namespace Domain.Models.Entities
{
    public class UsuarioTelefone
    {
        public UsuarioTelefone() { }

        public UsuarioTelefone(Guid usuarioId, Guid telefoneId)
        {
            UsuarioId = usuarioId;
            TelefoneId = telefoneId;
        }

        public Guid TelefoneId { get; set; }
        public Telefone Telefone { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}