using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.API.Application;
using Tasks.API.Application.Implementations;
using Tasks.API.DTO;
using Tasks.Domain.TestAggregate;

namespace Tasks.Tests.UnitTests.Application
{
    [TestFixture]
    public class TaskServiceTest
    {
        private Mock<ITaskRepository> taskRepositoryMock;
        private ITaskService taskService;

        public TaskServiceTest() { }

        [SetUp]
        public void SetUp()
        {
            taskRepositoryMock = new Mock<ITaskRepository>();
            taskService = new TaskService(taskRepositoryMock.Object, new Mock<ILogger<TaskService>>().Object);
        }

        [Test]
        public async Task GetTasks_by_time_filter_success()
        {
            var filter = new TaskFilter
            {
                StartDate = new DateTime(2020, 12, 1),
                FinishDate = new DateTime(2020, 12, 30)
            };
            var returnedTasks = new List<TaskEntity>
            {
                new TaskEntityBuilder().WithName("not filtered").WithFinishDate(filter.FinishDate.AddDays(1)).WithStartDate(filter.StartDate.AddDays(1)).Build(),
                new TaskEntityBuilder().WithName("filtered").WithFinishDate(filter.FinishDate.AddDays(-1)).WithStartDate(filter.StartDate.AddDays(1)).Build(),
                new TaskEntityBuilder().WithName("not filtered").WithFinishDate(filter.FinishDate.AddDays(1)).WithStartDate(filter.StartDate.AddDays(-1)).Build(),
                new TaskEntityBuilder().WithName("not filtered").WithFinishDate(filter.FinishDate).WithStartDate(filter.StartDate).Build()
            };
            taskRepositoryMock.Setup(r => r.GetAsync()).Returns(Task.FromResult(returnedTasks.AsEnumerable()));

            var tasks = (await taskService.GetTasksAsync(filter)).ToList();

            Assert.AreEqual(1, tasks.Count(), "Number of filtered tasks are incorrect");
        }
    }
}
