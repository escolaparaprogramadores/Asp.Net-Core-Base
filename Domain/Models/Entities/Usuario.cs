using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models.Entities
{
    public class Usuario 
    {

        public Usuario() => UsuarioTelefones = new Collection<UsuarioTelefone>();

        public Usuario(Usuario usuario, string senhaCriptografada, string token)
        {
            Id = Guid.NewGuid();
            Nome = usuario.Nome;
            Senha = senhaCriptografada;
            Email = usuario.Email;
            Token = token;
            DataCriacao = AddDataCriacao();
            DataAtualizacao = AddDataAtualização();
            DataUltimoLogin = AddDataUltimoLogin();
            Telefones =  usuario.Telefones;
            UsuarioTelefones = AdicionarUsuarioTelefone(usuario);

        }

        public DateTime AddDataAtualização() => DataAtualizacao = DateTime.Now.ToLocalTime();

        public DateTime AddDataUltimoLogin() => DataUltimoLogin = DateTime.Now.ToLocalTime();

        public DateTime AddDataCriacao() => DataCriacao = DateTime.Now.ToLocalTime();

        public List<UsuarioTelefone> AdicionarUsuarioTelefone(Usuario usuario)
        {
            List<UsuarioTelefone> telefones = new List<UsuarioTelefone>();
            foreach (var item in usuario.Telefones)
            {
                Telefone telefone = new Telefone(item.Numero, item.Ddd, usuario, usuario.Id);
                UsuarioTelefone usuarioTelefoneModel = new UsuarioTelefone(Id, telefone.Id);
                telefones.Add(usuarioTelefoneModel);
            }

            return telefones;

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