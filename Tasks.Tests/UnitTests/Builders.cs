using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.TestAggregate;

namespace Tasks.Tests.UnitTests
{
    public class TaskEntityBuilder
    {
        private readonly TaskEntity task;

        public TaskEntityBuilder()
        {
            task = new TaskEntity(1, "name" ,"description", new DateTime(), new DateTime(), TaskStatus.Planned);
        }

        public TaskEntityBuilder WithStatus(TaskStatus status)
        {
            task.ChangeStatus(status);

            return this;
        }

        public TaskEntityBuilder WithFinishDate(DateTime date)
        {
            task.ChangeFinishDate(date);

            return this;
        }

        public TaskEntityBuilder WithStartDate(DateTime date)
        {
            task.ChangeStartDate(date);

            return this;
        }

        public TaskEntityBuilder WithName(string name)
        {
            task.ChangeName(name);

            return this;
        }

        public TaskEntityBuilder WithSubTask(TaskEntity subtask)
        {
            task.AddSubTask(subtask);

            return this;
        }

        public TaskEntity Build()
        {
            return task;
        }
    }
}
