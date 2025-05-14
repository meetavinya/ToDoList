using Microsoft.Data.SqlClient;

namespace ToDoList.Interface
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly SqlConnection _connection;

        private ITaskRepository _taskRepository;


        public UnitOfWork(string connectionstring)
        {
            try
            {
                _connection = new SqlConnection(connectionstring);
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing database connection", ex);
            }
        }


        public ITaskRepository meet => _taskRepository ??=new TaskRepository(_connection);


        public void Complete()
        {
            try
            {
             
            }
            catch (Exception ex)
            {
                throw new Exception("Error completing transaction", ex);
            }
        }

        public void Dispose()
        {
            try
            {
                _connection?.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing database connection", ex);
            }
        }


    }
}
