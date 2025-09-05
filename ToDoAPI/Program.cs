using Microsoft.EntityFrameworkCore;
using ToDoAPI.Dal.Data;
using ToDoAPI.Dal.UnitOfWork;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Bll.Features.Tasks.Mappings;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register controllers and swagger auto generation
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure MySQL database and register DbContext
            builder.Services.AddDbContext<TodoDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // Register UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommand).Assembly));

            // Register Automapper
            builder.Services.AddAutoMapper(new Action<AutoMapper.IMapperConfigurationExpression>(cfg =>
            {
                cfg.AddProfile(new TaskMappingProfile());
            }));

            var app = builder.Build();

            // Configure swagger if project is compiled in development mode
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseRouting();

            app.Run();
        }
    }
}
