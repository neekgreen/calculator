namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;
 
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            this.Created = DateTimeOffset.Now;
            this.Id = Guid.NewGuid();
        }


        public ICollection<IDomainEvent> Events { get; private set; }

        public DateTimeOffset Created { get; protected set; }
        public DateTimeOffset? LastUpdated { get; protected set; }
        public Guid Id { get; protected set; }

        public long InternalId { get; protected set; }


        protected void CaptureEvent(params IDomainEvent[] domainEvents)
        {
            if (this.Events == null)
                this.Events = new List<IDomainEvent>();

            if (domainEvents != null)
            {
                foreach (var domainEvent in domainEvents)
                    this.Events.Add(domainEvent);
            }
        }

        protected void OnUpdated(params IDomainEvent[] domainEvents)
        {
            if (domainEvents != null)
                CaptureEvent(domainEvents);

            this.LastUpdated = DateTimeOffset.Now;
        }
    }    
}