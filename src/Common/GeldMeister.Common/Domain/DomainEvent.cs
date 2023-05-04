using MediatR;

namespace GeldMeister.Common.Domain;

public record DomainEvent(Guid Id) : INotification;