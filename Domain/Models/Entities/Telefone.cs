using Newtonsoft.Json;
using System;

namespace Domain.Models.Entities
{
    public class Telefone
    {
        [JsonIgnore]
        public Guid Id { get;  set; }
        public string Numero { get;  set; }
        public string Ddd { get; set; }
      
    }
}