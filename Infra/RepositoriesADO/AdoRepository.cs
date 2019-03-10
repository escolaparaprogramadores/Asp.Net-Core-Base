using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NewsGPS.Repository.ADO
{
    public class AdoRepository
    {
        public void ConsultarBanco()
        {
            var connectionString = ConfigurationSettings.AppSettings.Get("NGAdminEntities");
            // 1. Instancia a conexão(objeto SqlConnection)
            SqlConnection conn = new SqlConnection(@connectionString);
            //
            // define um SqlDataReader nulo
            SqlDataReader dr = null;
            try
            {
                // 2. Abre a conexão
                conn.Open();
                // 3. Passa conexão para o objeto command
                SqlCommand cmd = new SqlCommand("select * from com_Empresa", conn);
                //
                // 4. Usa conexão
                // obtêm o resultado da consulta
                dr = cmd.ExecuteReader();
                var dt = new DataTable();

                while (dr.Read())
                {
                    dt.Load(dr);
                }

                var teste = "";

            }
            finally
            {
                // fecha o reader
                if (dr != null)
                {
                    dr.Close();
                }
                // 5. Fecha a conexão
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteGradeoperacao (int id)
        {
            //var connectionString = ConfigurationSettings.AppSettings.Get("NGAdminEntities");
            var connectionString = ConfigurationManager.ConnectionStrings["NGAdminEntities"].ConnectionString;
            var inicio = connectionString.IndexOf("data source=tcp");
            var fim = connectionString.IndexOf("application name=EntityFramework") - 1;
            int length = fim - inicio + 1;

            var substringConn = connectionString.Substring(inicio, length);
            connectionString = substringConn;
            //ConfigurationManager.ConnectionStrings["DBWebConfigString"].ConnectionString;
            // 1. Instancia a conexão(objeto SqlConnection)
            SqlConnection conn = new SqlConnection(@connectionString);
            //
            // define um SqlDataReader nulo
            SqlDataReader dr = null;
            try
            {
                // 2. Abre a conexão
                conn.Open();
                // 3. Passa conexão para o objeto command
                SqlCommand cmd = 
                    new SqlCommand("delete from Ope_GradeOperacaoOnibus"+
                    " where  ID = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);
                // 4. Usa conexão
                // executa a query
                cmd.ExecuteNonQuery();


                cmd =
                    new SqlCommand("delete from Ope_GradeOperacao" +
                    " where  ID = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);
                // 4. Usa conexão
                // executa a query
                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

            }
            finally
            {
                // fecha o reader
                if (dr != null)
                {
                    dr.Close();
                }
                // 5. Fecha a conexão
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
    }
}
