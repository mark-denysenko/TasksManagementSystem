using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.DTO;

namespace Tasks.API.Application.Implementations
{
    public class CsvTaskReporter : ITaskReporter
    {
        public Task<byte[]> GetReportAsync(IEnumerable<TaskModel> tasks)
        {
            return Task.FromResult(new byte[0]);
        }
    }
}
