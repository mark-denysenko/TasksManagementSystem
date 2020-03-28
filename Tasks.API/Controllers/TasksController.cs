using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Domain.TestAggregate;
using Tasks.Infrastructure;

namespace Tasks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksContext _context;

        public TasksController(TasksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetTaskEntity(long id)
        {
            var taskEntity = await _context.Tasks.FindAsync(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return taskEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskEntity(long id, TaskEntity taskEntity)
        {
            if (id != taskEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntity>> PostTaskEntity(TaskEntity taskEntity)
        {
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskEntity", new { id = taskEntity.Id }, taskEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskEntity>> DeleteTaskEntity(long id)
        {
            var taskEntity = await _context.Tasks.FindAsync(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskEntity);
            await _context.SaveChangesAsync();

            return taskEntity;
        }

        [HttpGet("Csv")]
        public FileResult GetCsvReport(DateTime startDate, DateTime endDate)
        {
            return File(new byte[0], "text/csv", "tasks.csv");
        }

        private bool TaskEntityExists(long id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
