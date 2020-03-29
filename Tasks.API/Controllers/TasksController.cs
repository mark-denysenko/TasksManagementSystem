using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.API.Application;
using Tasks.API.DTO;
using Tasks.Domain.TestAggregate;

namespace Tasks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;
        private readonly ITaskService taskService;
        private readonly ITaskReporter taskReporter;

        public TasksController(ILogger<TasksController> logger, ITaskService taskService, ITaskReporter taskReporter)
        {
            this.logger = logger;
            this.taskService = taskService;
            this.taskReporter = taskReporter;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            return await taskService.GetTasksAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskAsync(long id)
        {
            var taskEntity = await taskService.GetTaskAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return taskEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAsync(long id, TaskModel task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            await taskService.UpdateTaskAsync(task);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntity>> PostTaskAsync(TaskModel task)
        {
            var taskModel = await taskService.CreateTaskAsync(task);

            return CreatedAtAction("GetTaskAsync", new { id = taskModel.Id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> DeleteTaskAsync(long id)
        {
            var taskEntity = await taskService.GetTaskAsync(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            await taskService.DeleteTaskAsync(id);

            return taskEntity;
        }

        [HttpGet("Csv/{startDate}/{finishDate}")]
        public async Task<FileResult> GetCsvReportAsync(DateTime startDate, DateTime finishDate)
        {
            var reportTasks = await taskService.GetTasksAsync(new TaskFilter 
            { 
                StartDate = startDate, 
                FinishDate = finishDate 
            }); 

            var report = await taskReporter.GetReportAsync(reportTasks);

            return File(report, "text/csv", "tasks.csv");
        }
    }
}
