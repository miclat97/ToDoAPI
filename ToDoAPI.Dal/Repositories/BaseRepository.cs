using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoAPI.Dal.Data;

namespace ToDoAPI.Dal.Repositories
{
    /// <summary>
    /// Base repository pattern allows to implement basic/common (I mean those which are generally doing repatable things) CRUD operations for all repositories which will be implementing IBaseRepository
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class BaseRepository<TObject> : IBaseRepository<TObject> where TObject : class
    {
        protected readonly TodoDbContext _context;
        private readonly DbSet<TObject> _dbSet;

        public BaseRepository(TodoDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TObject>();
        }

        public async Task<TObject?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TObject>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TObject>> FindAsync(Expression<Func<TObject, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task AddAsync(TObject entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TObject entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TObject entity)
        {
            _dbSet.Remove(entity);
        }
    }
}