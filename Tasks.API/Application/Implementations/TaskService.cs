using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.DTO;
using Tasks.Domain.TestAggregate;

namespace Tasks.API.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<TaskService> logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            var entity = taskRepository.Add(ToDomainEntity(task));
            await taskRepository.UnitOfWork.SaveChangesAsync();

            return ToTaskModel(entity);
        }

        public async Task DeleteTaskAsync(long id)
        {
            var task = await taskRepository.GetAsync(id);

            if (task is null)
            {
                return;
            }

            foreach(var subtask in task.SubTasks)
            {
                taskRepository.DeleteTask(subtask.Id);
            }

            taskRepository.DeleteTask(id);
            await taskRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<TaskModel> GetTaskAsync(long id)
        {
            var task = await taskRepository.GetAsync(id);
            return ToTaskModel(task);
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync(TaskFilter filter = null)
        {
            var tasks = await taskRepository.GetAsync();

            if (filter != null)
            {
                tasks = tasks.Where(t => t.StartDate > filter.StartDate && t.FinishDate < filter.FinishDate);
            }

            return tasks.Select(ToTaskModel);
        }

        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            var taskEntity = ToDomainEntity(task);
            taskRepository.Update(taskEntity);
            await taskRepository.UnitOfWork.SaveChangesAsync();

            return ToTaskModel(taskEntity);
        }

        private TaskEntity ToDomainEntity(TaskModel task)
        {
            return new TaskEntity(
                task.Id, 
                task.Name, 
                task.Description, 
                task.StartDate, 
                task.FinishDate, 
                task.TaskStatus,
                task.SubTasks?.Select(ToDomainEntity));
        }

        private TaskModel ToTaskModel(TaskEntity task)
        {
            return new TaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                FinishDate = task.FinishDate,
                TaskStatus = task.TaskStatus,
                ParentTaskId = task.ParentTask?.Id,
                SubTasks = task.SubTasks.Select(ToTaskModel)
            };
        }
    }
}
