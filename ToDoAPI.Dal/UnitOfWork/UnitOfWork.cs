using ToDoAPI.Dal.Data;
using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;

namespace ToDoAPI.Dal.UnitOfWork
{
    /// <summary>
    /// Unit of Work allows to make changes in different repositories (tables in database( and commit them in a single transaction at once)
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _context;
        private IBaseRepository<TaskEntity>? _tasks;

        public UnitOfWork(TodoDbContext context)
        {
            _context = context;
        }

        // Register Task repository in UnitOfWork
        public IBaseRepository<TaskEntity> Tasks =>
            _tasks ??= new BaseRepository<TaskEntity>(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}