using System;

namespace ToDoList.Interface
{
    public interface IUnitOfWork
    {
        ITaskRepository meet { get; }

        void Complete();

    }
}
