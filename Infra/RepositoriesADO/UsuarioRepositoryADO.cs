using Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using NewsGPS.Repository.ADO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infra.RepositoriesADO
{
    public class UsuarioRepositoryADO
    {
        public UsuarioRepositoryADO(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Usuario AddUsuario(Usuario usuario)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            try
            {
                using (AdoConexao adoConexao = new AdoConexao(Configuration.GetConnectionString("ConnectionString")))
                {
                    string comando = string.Empty;

                    if (usuario != null)
                    {
                        comando = @"insert into [dbo].[Usuario] (Id,Nome,Senha,Email,DataAtualizacao,DataCriacao,DataUltimoLogin,Token)
                                            VALUES(@Id,@Nome,@Senha,@Email,@DataAtualizacao,@DataCriacao,@DataUltimoLogin,@Token)";
                                            

                        parametros.Clear();
                        parametros.Add("@Id", usuario.Id);
                        parametros.Add("@Nome", usuario.Nome);
                        parametros.Add("@Senha", usuario.Senha);
                        parametros.Add("@Email", usuario.Email);
                        parametros.Add("@DataAtualizacao", usuario.DataAtualizacao);
                        parametros.Add("@DataCriacao", usuario.DataCriacao);
                        parametros.Add("@DataUltimoLogin", usuario.DataUltimoLogin);
                        parametros.Add("@Token", usuario.Token);


                        adoConexao.ExecuteScalar(comando, parametros);
                    }
                }

                return usuario;
            }
            catch
            {
                return usuario;
            }
        }

        public void AddTelefone(Telefone telefone, Guid usuarioId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            try
            {
                using (AdoConexao adoConexao = new AdoConexao(Configuration.GetConnectionString("ConnectionString")))
                {
                    string comando = string.Empty;


                    if (telefone !=  null)
                    {
                            comando = string.Empty;
                            comando = @"insert into [dbo].[Telefone] (Id,Numero,Ddd,UsuarioId)
                                            VALUES(@Id,@Numero,@Ddd,@UsuarioId)";
                        var UsuarioId = usuarioId;
                            parametros.Clear();
                            parametros.Add("@Id", telefone.Id);
                            parametros.Add("@Numero", telefone.Numero);
                            parametros.Add("@Ddd", telefone.Ddd);
                            parametros.Add("@UsuarioId", UsuarioId);

                            adoConexao.ExecuteScalar(comando, parametros);
                    }
                }
            }
                catch
                {

                }
        }

        public void AddTelefoneUsuario(UsuarioTelefone usuarioTelefone)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            try
            {
                using (AdoConexao adoConexao = new AdoConexao(Configuration.GetConnectionString("ConnectionString")))
                {
                    string comando = string.Empty;

                    if (usuarioTelefone != null)
                    {
                        comando = string.Empty;
                        comando = @"insert into [dbo].[UsuarioTelefones] (UsuarioId,TelefoneId)
                                            VALUES(@UsuarioId,@TelefoneId)";


                        parametros.Clear();
                        parametros.Add("@UsuarioId", usuarioTelefone.UsuarioId);
                        parametros.Add("@TelefoneId", usuarioTelefone.TelefoneId);

                        adoConexao.ExecuteScalar(comando, parametros);
                    }
                }
            }
            catch
            {
            }
        }

        public bool ValidarEmail(string Email)
        {
            var iSExist = false;

            try
            {
                using (AdoConexao adoConexao = new AdoConexao(Configuration.GetConnectionString("ConnectionString")))
                {
                    string comando = string.Empty;
                    Dictionary<string, object> parametros = new Dictionary<string, object>();


                    comando = @"SELECT Email FROM [dbo].[Usuario]
                                           WHERE Email = @Email";


                        parametros.Clear();      
                        parametros.Add("@Email", Email);



                    SqlDataReader result =   adoConexao.ExecutarConsulta(comando, parametros);
                    var teste = result.HasRows;
                    if (result.HasRows)
                        iSExist = true;


                }
            

                return iSExist;
            }
            catch
            {
                return iSExist;
            }
        }


    }
}
