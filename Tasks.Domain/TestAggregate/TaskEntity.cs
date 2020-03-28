using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.Exceptions;

namespace Tasks.Domain.TestAggregate
{
    public class TaskEntity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime FinishDate { get; private set; }
        public TaskStatus TaskStatus { get; private set; }
        public TaskEntity ParentTask { get; private set; }

        private readonly List<TaskEntity> subTasks = new List<TaskEntity>();

        public IReadOnlyCollection<TaskEntity> SubTasks => subTasks;

        protected TaskEntity(
            long id,
            string name,
            string description,
            DateTime startDate,
            DateTime finishDate,
            TaskStatus taskState)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            TaskStatus = taskState;
        }

        protected TaskEntity() { }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void ChangeState(TaskStatus newState)
        {
            TaskStatus = newState;
        }

        public void ChangeStartDate(DateTime newStartDate)
        {
            if (newStartDate > FinishDate)
            {
                throw new TaskException("Start date can not be bigger than finish date");
            }

            StartDate = newStartDate;
        }
        public void ChangeFinishDate(DateTime newFinishDate)
        {
            if (newFinishDate < StartDate)
            {
                throw new TaskException("Finish date can not be smaller than start date");
            }

            FinishDate = newFinishDate;
        }
    }
}
