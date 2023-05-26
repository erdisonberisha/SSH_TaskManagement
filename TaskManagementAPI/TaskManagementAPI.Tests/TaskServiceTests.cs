using Moq;
using Nest;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services.Interfaces;
using TaskManagementAPI.Services;
using TaskManagementAPI.Data.Repository.Intefaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class TaskServiceTests
{
    private TaskService _taskService;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ICategoryService> _mockCategoryService;
    private Mock<INotificationService> _mockNotificationService;
    private Mock<ITaskRepository> _mockTaskRepository;
    private Mock<ISharedTaskRepository> _mockSharedTaskRepository;

    public TaskServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCategoryService = new Mock<ICategoryService>();
        _mockNotificationService = new Mock<INotificationService>();
        _mockTaskRepository = new Mock<ITaskRepository>();
        _mockSharedTaskRepository = new Mock<ISharedTaskRepository>();

        _mockUnitOfWork.Setup(uow => uow.TaskRepository).Returns(_mockTaskRepository.Object);
        _mockUnitOfWork.Setup(uow => uow.SharedTaskRepository).Returns(_mockSharedTaskRepository.Object);

        _taskService = new TaskService(
            _mockUnitOfWork.Object,
            _mockCategoryService.Object,
            _mockNotificationService.Object
        );
    }

    [Fact]
    public async Task ApproveInvite_ValidInput_ShouldUpdateSharedTask()
    {
        // Arrange
        var userId = 1;
        var taskId = 1;
        var sharedTask = new SharedTask { UserId = userId, TaskId = taskId, Approved = false };

        _mockSharedTaskRepository
            .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<SharedTask, bool>>>()))
            .Returns(new List<SharedTask> { sharedTask }.AsQueryable());

        // Act
        await _taskService.ApproveInvite(userId, taskId);

        // Assert
        _mockSharedTaskRepository.Verify(repo => repo.Update(sharedTask), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.True(sharedTask.Approved);
    }

    [Fact]
    public async Task ApproveInvite_InvalidInput_ThrowsInvalidOperationException()
    {
        // Arrange
        var userId = 1;
        var taskId = 1;

        _mockSharedTaskRepository
            .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<SharedTask, bool>>>()))
            .Returns(Enumerable.Empty<SharedTask>().AsQueryable());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.ApproveInvite(userId, taskId));
    }

    [Fact]
    public async Task GetPendingInvitesAsync_ValidUserId_ReturnsPendingInvites()
    {
        // Arrange
        var userId = 1;
        var sharedTasks = new List<SharedTask>
        {
            new SharedTask { UserId = userId, Approved = false, Task = new TaskEntity() },
            new SharedTask { UserId = userId, Approved = false, Task = new TaskEntity() }
        };

        _mockSharedTaskRepository
            .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<SharedTask, bool>>>()))
            .Returns(sharedTasks.AsQueryable());

        // Act
        var result = await _taskService.GetPendingInvitesAsync(userId);

        // Assert
        Assert.Equal(sharedTasks.Count, result.Count());
        Assert.Equal(sharedTasks, result);
    }

    [Fact]
    public async Task Delete_ExistingTask_ReturnsTrueAndDeletesTask()
    {
        // Arrange
        var id = 1;
        var userId = 1;
        var task = new TaskEntity { Id = id, UserId = userId };

        _mockTaskRepository
            .Setup(repo => repo.GetById(It.IsAny<Expression<Func<TaskEntity, bool>>>()))
            .Returns((IQueryable<TaskEntity>)task);

        // Act
        var result = await _taskService.Delete(id, userId);

        // Assert
        _mockTaskRepository.Verify(repo => repo.Delete(task), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_NonexistentTask_ReturnsFalse()
    {
        // Arrange
        var id = 1;
        var userId = 1;

        _mockTaskRepository
            .Setup(repo => repo.GetById(It.IsAny<Expression<Func<TaskEntity, bool>>>()))
            .Returns((IQueryable<TaskEntity>)null);

        // Act
        var result = await _taskService.Delete(id, userId);

        // Assert
        Assert.False(result);
    }


}
