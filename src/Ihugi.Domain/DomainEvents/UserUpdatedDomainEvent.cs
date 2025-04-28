using Ihugi.Domain.Abstractions;

namespace Ihugi.Domain.DomainEvents;

// TODO: XML docs
public sealed record UserUpdatedDomainEvent(Guid userId) : IDomainEvent;