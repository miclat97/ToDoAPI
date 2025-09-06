using AutoMapper;
using Moq;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Dal.Entities;
using ToDoAPI.Dal.Repositories;
using ToDoAPI.Dal.UnitOfWork;

namespace ToDoAPI.Tests.Features.Tasks.Commands
{
    /// <summary>
    /// Unit Tests only for create todo task directly in handler
    /// </summary>
    public class CreateTaskCommandHandlerTests
    {
        // Mocks
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<TaskEntity>> _mockTaskRepository;
        private readonly CreateTaskCommandHandler _handler;

        public CreateTaskCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockTaskRepository = new Mock<IBaseRepository<TaskEntity>>();

            _mockUnitOfWork.Setup(uow => uow.Tasks).Returns(_mockTaskRepository.Object);
            _handler = new CreateTaskCommandHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateTask_AndReturnId()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Title = "Test Task",
                Description = "Test Description",
            };

            _mockMapper.Setup(mapper => mapper.Map<TaskEntity>(command))
                       .Returns(new TaskEntity { Title = command.Title, Description = command.Description });
            _mockTaskRepository.Setup(repo => repo.AddAsync(It.IsAny<TaskEntity>()))
                               .Returns(Task.CompletedTask)
                               .Callback<TaskEntity>(task => task.Id = 1);

            _mockUnitOfWork.Setup(uow => uow.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result > 0);
            _mockTaskRepository.Verify(repo => repo.AddAsync(It.IsAny<TaskEntity>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }
    }
}