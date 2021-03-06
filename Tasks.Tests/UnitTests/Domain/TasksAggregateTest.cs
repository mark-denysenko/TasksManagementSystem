﻿using NUnit.Framework;
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

        [Test]
        public void Check_TaskStatus_All_subtasks_completed()
        {
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.Completed).Build());
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.Completed).Build());

            var result = task.TaskStatus;

            Assert.AreEqual(TaskStatus.Completed, result);
        }

        [Test]
        public void Check_TaskStatus_Any_subtask_inProgress()
        {
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.Completed).Build());
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.InProgress).Build());

            var result = task.TaskStatus;

            Assert.AreEqual(TaskStatus.InProgress, result);
        }

        [Test]
        public void Check_TaskStatus_No_subtask_inProgress_and_any_planned_and_any_completed()
        {
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.Completed).Build());
            task.AddSubTask(new TaskEntityBuilder().WithStatus(TaskStatus.Planned).Build());

            var result = task.TaskStatus;

            Assert.AreEqual(TaskStatus.Planned, result);
        }

        [TestCase(TaskStatus.Planned)]
        [TestCase(TaskStatus.InProgress)]
        [TestCase(TaskStatus.Completed)]
        public void Check_TaskStatus_no_subtasks(TaskStatus taskStatus)
        {
            task.ChangeStatus(taskStatus);

            Assert.AreEqual(taskStatus, task.TaskStatus, "Tasks status incorrect");
        }
    }
}
