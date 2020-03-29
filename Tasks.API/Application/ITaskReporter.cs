using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.DTO;

namespace Tasks.API.Application
{
    public interface ITaskReporter
    {
        Task<byte[]> GetReportAsync(IEnumerable<TaskModel> tasks);
    }
}
