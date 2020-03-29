using System;
using System.Collections.Generic;
using System.Linq;
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
        public TaskEntity ParentTask { get; private set; }
        public TaskStatus TaskStatus 
        { 
            private set => taskStatus = value;
            get
            {
                if (!SubTasks.Any())
                {
                    return taskStatus;
                }

                if (SubTasks.All(t => t.TaskStatus == TaskStatus.Completed))
                {
                    return TaskStatus.Completed;
                }
                else if (SubTasks.Any(t => t.TaskStatus == TaskStatus.InProgress))
                {
                    return TaskStatus.InProgress;
                }
                else
                {
                    return TaskStatus.Planned;
                }
            }
        }

        private TaskStatus taskStatus;
        private readonly List<TaskEntity> subTasks = new List<TaskEntity>();

        public IReadOnlyCollection<TaskEntity> SubTasks => subTasks;

        public TaskEntity(
            long id,
            string name,
            string description,
            DateTime startDate,
            DateTime finishDate,
            TaskStatus taskState,
            IEnumerable<TaskEntity> subTasks)
            : this(id, name, description, startDate, finishDate, taskState)
        {
            if (subTasks != null)
            {
                this.subTasks = new List<TaskEntity>(subTasks);
            }
        }

        public TaskEntity(
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
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new TaskException("Name can not be empty");
            }

            Name = newName.Trim();
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void ChangeStatus(TaskStatus newState)
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

        public void AddSubTask(TaskEntity subTask)
        {
            if (ParentTask != null)
            {
                throw new TaskException("Subtask can not have subtasks");
            }

            if (subTask is null)
            {
                throw new TaskException("Subtask was not provided");
            }

            subTasks.Add(subTask);
        }

        public void ChangeParentTask(TaskEntity parentTask)
        {
            if (subTasks.Any() && parentTask != null)
            {
                throw new TaskException("Task with subtasks cannot be switched to subtask");
            }

            ParentTask = parentTask;
        }
    }
}
