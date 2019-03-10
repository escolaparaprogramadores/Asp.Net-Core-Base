using Domain.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Telefone> Telefones { get; set; }
 
    }
}
