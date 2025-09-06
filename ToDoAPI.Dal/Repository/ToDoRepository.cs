using ToDoAPI.Dal.Data;
using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;

namespace ToDoAPI.Dal.Repository
{
    public class ToDoRepository : BaseRepository<TaskEntity>, IToDoRepository // Because it's inherited from base repository, there's no need to implement those
                                                                              // "basic"/"standard" methods independently in each repository again. And it's still
                                                                              // implementing it's own interface (which is implementing base repository interface as well)
                                                                              // to have implemented modern design patterns implemented in whole project
    {
        public ToDoRepository(TodoDbContext context) : base(context)
        {
        }
    }
}
