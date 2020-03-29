using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.API.DTO
{
    public class TaskFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
