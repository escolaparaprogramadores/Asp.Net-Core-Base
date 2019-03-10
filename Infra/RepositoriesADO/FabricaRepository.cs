using System.Configuration;


namespace NewsGPS.Repository.ADO
{
    public class FabricaRepository
    {
        private readonly string _stringConexao;

        public FabricaRepository()
        {
            _stringConexao = ConfigurationManager.ConnectionStrings["NGAdminEntities"].ConnectionString;
        }
    }
}
