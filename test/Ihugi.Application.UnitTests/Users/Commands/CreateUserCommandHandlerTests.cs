using FluentAssertions;
using Ihugi.Application.UseCases.Users.Commands.CreateUser;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;
using Moq;

namespace Ihugi.Application.UnitTests.Users.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    private const string DummyName = "first";
    private const string DummyPassword = "password";
    private const string DummyEmail = "email@test.com";

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(
                x => x.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateUserCommand(DummyName, DummyPassword, DummyEmail);

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.EmailAlreadyInUse);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(
                x => x.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateUserCommand(DummyName, DummyPassword, DummyEmail);

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_CallAddAsyncOnRepositoryOnce_WhenEmailIsUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(
                x => x.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateUserCommand("first", "first", "email@test.com");

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Value.Should().NotBeNull();
        _userRepositoryMock.Verify(
            x => x.AddAsync(
                It.Is<User>(u => u.Id == result.Value!.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallAddAsyncOnRepository_WhenEmailIsNotUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(
                x => x.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateUserCommand("first", "first", "email@test.com");

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Verify(
            x => x.AddAsync(
                It.Is<User>(u => u.Id == result.Value!.Id),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(
                x => x.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateUserCommand("first", "first", "email@test.com");

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}