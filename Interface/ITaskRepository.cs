using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Interface
{
    public interface ITaskRepository
    {
        Task<bool> RegisterAsync(Login l);

        Login login(string Email, string Password);
        Totask GetById(int id);
        Task<IEnumerable<Totask>> GetAll(int UserId);
        void Add(Totask t);
        void Update(Totask t);
        void Delete(Totask t);
    }
}
