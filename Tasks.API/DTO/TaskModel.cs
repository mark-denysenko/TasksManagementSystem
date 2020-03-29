using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.API.DTO
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public long? ParentTaskId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public IEnumerable<TaskModel> SubTasks { get; set; }

        public string Status => TaskStatus.ToString();
    }
}
