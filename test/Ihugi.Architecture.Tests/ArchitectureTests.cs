using FluentAssertions;
using NetArchTest.Rules;

namespace Ihugi.Architecture.Tests;

// TODO: XML docs
public class ArchitectureTests
{
    private const string DomainNamespace = "Ihugi.Domain";
    private const string ApplicationNamespace = "Ihugi.Application";
    private const string InfrastructureNamespace = "Ihugi.Infrastructure";
    private const string PresentationNamespace = "Ihugi.Presentation";
    private const string WebNamespace = "Ihugi.WebApi";

    [Fact]
    public void Domain_Should_Not_Have_DependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Ihugi.Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_Have_DependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Ihugi.Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_Have_DependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Ihugi.Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_Have_DependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Ihugi.Presentation.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}