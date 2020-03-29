using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.API.DTO;

namespace Tasks.API.Application.Implementations
{
    public class CsvTaskReporter : ITaskReporter
    {
        private const char delimiter = ',';

        public Task<byte[]> GetReportAsync(IEnumerable<TaskModel> tasks)
        {
            if (!tasks.Any())
            {
                return Task.FromResult(new byte[0]);
            }


            var csv = new StringBuilder();
            csv.AppendJoin(delimiter, GetColumnNames());

            var flattenTasks = tasks.Union(tasks.SelectMany(t => t.SubTasks));
            foreach(var task in flattenTasks)
            {
                csv.AppendLine();
                csv.AppendJoin(delimiter, GetValues(task));
            }

            using (var s = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString())))
                return Task.FromResult(s.ToArray());
        }

        private string[] GetColumnNames()
        {
            return new[] 
            {
                "Name",
                "Description",
                "StartDate",
                "FinishDate",
                "TaskStatus"
            };
        }

        private string[] GetValues(TaskModel product)
        {
            return new[]
            {
                product.Name,
                product.Description,
                product.StartDate.ToString(),
                product.FinishDate.ToString(),
                product.TaskStatus.ToString()
            };
        }
    }
}
