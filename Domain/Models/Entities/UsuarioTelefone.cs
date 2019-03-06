using System;

namespace Domain.Models.Entities
{
    public class UsuarioTelefone
    {
        public Guid TelefoneId { get; set; }
        public Telefone Telefone { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}