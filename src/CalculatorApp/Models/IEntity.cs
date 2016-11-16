namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;

    public interface IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}