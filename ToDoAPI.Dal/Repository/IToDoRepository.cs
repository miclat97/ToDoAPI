using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;

namespace ToDoAPI.Dal.Repository
{
    public interface IToDoRepository : IBaseRepository<TaskEntity>
    {
    }
}
