using Microsoft.EntityFrameworkCore;
using ToDoAPI.Dal.Entities;

namespace ToDoAPI.Dal.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {

        // Register Task to make possible to create database structure by creating migration
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}