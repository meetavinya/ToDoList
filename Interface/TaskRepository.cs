using Dapper;
using Microsoft.Data.SqlClient;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
namespace ToDoList.Interface

{
    public class TaskRepository:ITaskRepository
    {
    private readonly SqlConnection  _connection ;

        public TaskRepository(SqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<bool> RegisterAsync(Login l)
        {
            try
            {
                string sql = "insert into Users(Username,Email,Password) values(@Username,@Email,@password)";
                await _connection.ExecuteAsync(sql, l);
                return true;

            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Message.Contains("UNIQUE")) // SQL Server unique constraint violation
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering user", ex);
            }
        }

        public  Login login(string Email,string Password)
        {
            try
            {
                string sql = "select * from Users where Email=@Email and Password=@Password";
                var user = _connection.QueryFirstOrDefault<Login>(sql, new { Email, Password });
                if (user == null)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error logging in user", ex);
            }
            
        }

        public Totask GetById(int Id)
        {
            try
            {
                string sql = "select * from Tasks where Id=@Id";
                return _connection.QueryFirstOrDefault<Totask>(sql, new { ID = Id });
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving task by ID", ex);
            }
        }


        public async Task <IEnumerable<Totask>> GetAll(int UserId)
        {
            try
            {
                string sql = "select * from Tasks where flag!='1' and UserId=@UserId ";
                var result =  await _connection.QueryAsync<Totask>(sql, new {UserId});
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all tasks", ex);
            }
        }


        public async void Add(Totask t)
        {
            try
            {
                string sql = "insert into Tasks(Title,Description,DueDate,Priority,Status,flag,UserId) values(@Title,@Description,@DueDate,@Priority,@Status,'0',@UserId)";
                await _connection.ExecuteAsync(sql, t);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding task", ex);
            }
        }

        public async void Update(Totask t)
        {
            try
            {
                string sql = "update Tasks set Title=@Title,Description=@Description,DueDate=@DueDate,Priority=@Priority,Status=@Status where Id=@Id";
                await _connection.ExecuteAsync(sql, t);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task", ex);
            }
        }


        public async void Delete(Totask t)
        {
            try
            {
                string sql = "update Tasks set flag='1' where Id=@ID";
                await _connection.ExecuteAsync(sql, t);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting task", ex);
            }
        }

    }
}
