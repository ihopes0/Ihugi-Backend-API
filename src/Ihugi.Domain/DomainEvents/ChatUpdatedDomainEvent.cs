using Ihugi.Domain.Abstractions;

namespace Ihugi.Domain.DomainEvents;

/// <summary>
/// Domain event that is raised when Chat entity is updated
/// </summary>
/// <param name="ChatId">Chat Id that has been updated</param>
/// <param name="NewName">Changed name of the chat</param>
public sealed record ChatUpdatedDomainEvent(Guid ChatId, string NewName) : IDomainEvent;