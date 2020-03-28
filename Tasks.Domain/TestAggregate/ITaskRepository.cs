using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Domain.TestAggregate
{
    public interface ITaskRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
