using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Dal.Data;
using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;

namespace ToDoAPI.Dal.Repository
{
    public class ToDoRepository : BaseRepository<TaskEntity>, IToDoRepository
    {
        public ToDoRepository(TodoDbContext context) : base(context)
        {
        }
    }
}
