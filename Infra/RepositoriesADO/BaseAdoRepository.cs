using System;


namespace NewsGPS.Repository.ADO
{  
   public class BaseAdoRepository<T> : IDisposable where T : class
   {
       public readonly string StringConexao;

       protected readonly FabricaRepository Repositorios;

       public BaseAdoRepository()
       {
       }

       public BaseAdoRepository(string stringConexao)
       {
            if (stringConexao == string.Empty)
                throw new Exception("A string de conexão não pode ser em branco ou conter um valor nulo.");

            StringConexao = stringConexao;

            Repositorios = new FabricaRepository();
        }
     
       public virtual void Inserir(T objeto)
       {
           throw new NotImplementedException();
       }

       protected virtual void Alterar(T objeto)
       {
           throw new NotImplementedException();
       }

       protected virtual void Deletar(T objeto)
       {
           throw new NotImplementedException();
       }

       protected virtual T ConsultarTodos()
       {
           throw new NotImplementedException();
       }

       protected virtual T ConsultarPorId(int id)
       {
           throw new NotImplementedException();
       }

       public void Dispose()
       {
           GC.SuppressFinalize(this);
       }
   }   
}
