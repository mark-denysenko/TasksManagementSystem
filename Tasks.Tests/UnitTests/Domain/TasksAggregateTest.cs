using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.Exceptions;
using Tasks.Domain.TestAggregate;

namespace Tasks.Tests.UnitTests.Domain
{
    [TestFixture]
    public class TasksAggregateTest
    {
        private TaskEntity task;
        public TasksAggregateTest() { }

        [SetUp]
        public void SetUp()
        {
            task = new TaskEntity(
                1, 
                "task", 
                "description", 
                new DateTime(2020, 03, 1), 
                new DateTime(2020, 03, 31), 
                TaskStatus.Planned);
        }

        [Test]
        public void Change_StartDate_success()
        {
            var expected = task.StartDate.AddDays(-1);

            task.ChangeStartDate(expected);

            Assert.AreEqual(expected, task.StartDate, "StartDate was not set to needed");
        }

        [Test]
        public void Change_StartDate_throws_exception()
        {
            var newStartDate = task.FinishDate.AddDays(1);

            Assert.Catch<TaskException>(() => task.ChangeStartDate(newStartDate), "StartDate did not throw the exception");
        }

        [Test]
        public void Change_FinishDate_success()
        {
            var expected = task.FinishDate.AddDays(1);

            task.ChangeFinishDate(expected);

            Assert.AreEqual(expected, task.FinishDate, "FinishDate was not set to needed");
        }

        [Test]
        public void Change_FinishDate_throws_exception()
        {
            var newStartDate = task.StartDate.AddDays(-1);

            Assert.Catch<TaskException>(() => task.ChangeFinishDate(newStartDate), "FinishDate did not throw the exception");
        }
    }
}
