using API.Domains.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates
{
    public class Entity : IEntity
    {
        protected List<INotification> domainEvents;
        public IReadOnlyList<INotification> DomainEvents => domainEvents;

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents ??= new List<INotification>();
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }
    }
}
