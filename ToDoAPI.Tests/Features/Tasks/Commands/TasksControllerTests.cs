using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask;
using ToDoAPI.Bll.Features.Tasks.DTOs;
using ToDoAPI.Bll.Features.Tasks.Queries.GetAllTasks;
using ToDoAPI.Bll.Features.Tasks.Queries.GetTaskById;
using ToDoAPI.Controllers;

namespace ToDoAPI.Tests.Features.Tasks.Commands
{
    public class TasksControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new TasksController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<TaskDto> { new TaskDto { Id = 1, Title = "Test Task" } };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllTasksQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(tasks, result.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WithTaskId()
        {
            // Arrange
            var command = new CreateTaskCommand { Title = "New Task" };
            _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(1);

            // Act
            var result = await _controller.Create(command) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(_controller.GetById), result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithTask()
        {
            // Arrange
            var task = new TaskDto { Id = 1, Title = "Test Task" };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetTaskByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(task);

            // Act
            var result = await _controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(task, result.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFoundResult_WhenTaskNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetTaskByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((TaskDto)null);

            // Act
            var result = await _controller.GetById(1) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContentResult_WhenUpdateSuccessful()
        {
            // Arrange
            var command = new UpdateTaskCommand { Id = 1, Title = "Updated Task" };
            _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, command) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequestResult_WhenIdMismatch()
        {
            // Arrange
            var command = new UpdateTaskCommand { Id = 2, Title = "Updated Task" };

            // Act
            var result = await _controller.Update(1, command) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFoundResult_WhenTaskNotFound()
        {
            // Arrange
            var command = new UpdateTaskCommand { Id = 1, Title = "Updated Task" };
            _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, command) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}