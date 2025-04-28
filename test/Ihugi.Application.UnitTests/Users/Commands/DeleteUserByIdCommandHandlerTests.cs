using FluentAssertions;
using Ihugi.Application.UseCases.Users.Commands.DeleteUserById;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;
using Moq;

namespace Ihugi.Application.UnitTests.Users.Commands;

public class DeleteUserByIdCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly List<User> _testUsers;


    public DeleteUserByIdCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
        _testUsers = GetTestUsers();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureNoContentError_WhenUserDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_testUsers.FirstOrDefault(u => u.Id == id));

        var command = new DeleteUserByIdCommand(id);

        var handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.NoContent);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserExistedAndWasDeleted()
    {
        // Arrange
        var id = _testUsers.FirstOrDefault()!.Id;

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_testUsers.FirstOrDefault(u => u.Id == id));

        var command = new DeleteUserByIdCommand(id);

        var handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }
    
    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenUserDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_testUsers.FirstOrDefault(u => u.Id == id));

        var command = new DeleteUserByIdCommand(id);

        var handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
    
    [Fact]
    public async Task Handle_Should_CallDeleteOnUserRepositoryOnce_WhenUserExists()
    {
        // Arrange
        var id = _testUsers.FirstOrDefault()!.Id;

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(_testUsers.FirstOrDefault(u => u.Id == id));

        var command = new DeleteUserByIdCommand(id);

        var handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Verify(
            x => x.Delete(It.IsAny<User>()),
            Times.Once);
    }
    
    private List<User> GetTestUsers()
    {
        var users = new List<User>
        {
            User.Create(Guid.NewGuid(), "Max", "password", "email@test.com"),
            User.Create(Guid.NewGuid(), "John", "Doe", "j-email@test.com"),
            User.Create(Guid.NewGuid(), "Jane", "Doe", "jane@test.com")
        };
        return users;
    }
}