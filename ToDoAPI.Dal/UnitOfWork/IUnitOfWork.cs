using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;

namespace ToDoAPI.Dal.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TaskEntity> Tasks { get; }
        Task<int> CommitAsync();
    }
}