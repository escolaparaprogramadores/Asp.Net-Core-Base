using Newtonsoft.Json;
using System;

namespace Domain.Models.Entities
{
    public class Telefone
    {
        public Telefone() { }

        public Telefone(string numero, string ddd)
        {
            Id = Guid.NewGuid();
            Numero = numero;
            Ddd = ddd;
        }

        [JsonIgnore]
        public Guid Id { get;  set; }
        public string Numero { get;  set; }
        public string Ddd { get; set; }

    }
}