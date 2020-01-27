using PushFive.Core.DomainObjects;
using System;

namespace PushFive.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
