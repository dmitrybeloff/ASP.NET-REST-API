using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Interfaces
{
    public interface IEntity
    {
        IReadOnlyList<INotification> DomainEvents { get; }
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
    }
}
