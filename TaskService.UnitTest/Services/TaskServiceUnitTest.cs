using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Domain.Models;
using TaskService.Domain.Services;
using TaskService.Middlewares;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskService.UnitTest.Services
{
    public class TaskServiceUnitTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBaseRepository<TaskModel>> _taskRepositoryMock;
        private readonly TasksService _sut;
        private readonly Mock<ILogger<TasksService>> _loggerMock;

        public TaskServiceUnitTest()
        {
            _fixture = new Fixture();
            _taskRepositoryMock = _fixture.Freeze<Mock<IBaseRepository<TaskModel>>>();
            _sut = new TasksService(_taskRepositoryMock.Object);
            _loggerMock = _fixture.Freeze<Mock<ILogger<TasksService>>>();
        }

        [Fact]
        public async void CreateNewTask_ShouldCreateNewTask_ReturnDataIfSuccessul()
        {
            //Arrange
            var mockTask = _fixture.Create<TaskModel>();
            _taskRepositoryMock.Setup(x => x.AddAsync(mockTask)).ReturnsAsync(mockTask);

            //Act
            var result = await _sut.CreateNewTask(mockTask);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<TaskModel>();
            _taskRepositoryMock.Verify(x => x.AddAsync(mockTask), Times.Once());
        }

        [Fact]
        public async void GetAllTask_ShouldReturnAllData_WhenDataFound()
        {
            //Arrange
            var mockTask = _fixture.Create<IEnumerable<TaskModel>>();
            _taskRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockTask);

            //Act
            var result = await _sut.GetAllTasks();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeAssignableTo<IEnumerable<TaskModel>>();
            _taskRepositoryMock.Verify(x => x.GetAllAsync());
        }

        [Fact]
        public async void GetAllTask_ShouldReturnEmpty_WhenNoDataFound()
        {
            //Arrange
            IEnumerable<TaskModel> mockTask = Enumerable.Empty<TaskModel>();
            _taskRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockTask);

            //Act
            var result = await _sut.GetAllTasks();

            //Assert
            result.Should().BeEmpty();
            _taskRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void UpdateTask_ShouldUpdateTask_ReturnDataIfSuccessfull()
        {
            //Arrange
            var mockTaskUpdate = _fixture.Create<TaskModel>();
            var rowVersion = _fixture.Create<byte[]>();
            var id = _fixture.Create<int>();
            _taskRepositoryMock.Setup(x => x.UpdateAsync(mockTaskUpdate, rowVersion)).Returns(Task.CompletedTask);

            //Act
            var result = await _sut.UpdateTask(id, mockTaskUpdate);

            //Assert
            result.TaskName.Should().Be(mockTaskUpdate.TaskName);
        }
    }
}
