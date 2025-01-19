using NUnit.Framework;
using TaskTracker.Models;
using TaskTracker.Repos.Interfaces;
using TaskTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Services;

namespace TaskTracker.Tests
{
    public class TaskServiceTests
    {
        private ITaskRepos _taskRepository;
        private ITaskServices _taskService;

        [SetUp]
        public void Setup()
        {
            _taskRepository = new InMemoryTaskRepository();
            _taskService = new TaskServices(_taskRepository);
        }

        [Test]
        public void AddTask_ShouldAddTask()
        {
            _taskService.AddTask("Test Task");
            var tasks = _taskService.GetAllTasks();
            Assert.Equals(1, tasks.Count);
            Assert.Equals("Test Task", tasks[0].Description);
        }

        [Test]
        public void UpdateTask_ShouldUpdateTask()
        {
            _taskService.AddTask("Initial Task");
            var tasks = _taskService.GetAllTasks();
            int taskId = tasks[0].Id;

            _taskService.UpdateTask(taskId, "Updated Task");
            tasks = _taskService.GetAllTasks();
            Assert.Equals("Updated Task", tasks[0].Description);
        }

        [Test]
        public void DeleteTask_ShouldRemoveTask()
        {
            _taskService.AddTask("Task to be deleted");
            var tasks = _taskService.GetAllTasks();
            int taskId = tasks[0].Id;

            _taskService.DeleteTask(taskId);
            tasks = _taskService.GetAllTasks();
            Assert.Equals(0, tasks.Count);
        }

        [Test]
        public void MarkTaskInProgress_ShouldUpdateStatus()
        {
            _taskService.AddTask("Task to be in progress");
            var tasks = _taskService.GetAllTasks();
            int taskId = tasks[0].Id;

            _taskService.MarkTaskInProgress(taskId);
            tasks = _taskService.GetAllTasks();
            Assert.Equals("in progress", tasks[0].Status);
        }

        [Test]
        public void MarkTaskDone_ShouldUpdateStatus()
        {
            _taskService.AddTask("Task to be done");
            var tasks = _taskService.GetAllTasks();
            int taskId = tasks[0].Id;

            _taskService.MarkTaskDone(taskId);
            tasks = _taskService.GetAllTasks();
            Assert.Equals("done", tasks[0].Status);
        }

        [Test]
        public void GetTasksByStatus_ShouldReturnCorrectTasks()
        {
            _taskService.AddTask("Task 1");
            _taskService.AddTask("Task 2");
            var tasks = _taskService.GetAllTasks();
            tasks[0].Status = "done";
            tasks[1].Status = "todo";
            _taskRepository.SaveTasks(tasks);

            var doneTasks = _taskService.GetTasksByStatus("done");
            Assert.Equals(1, doneTasks.Count);
            Assert.Equals("done", doneTasks[0].Status);

            var todoTasks = _taskService.GetTasksByStatus("todo");
            Assert.Equals(1, todoTasks.Count);
            Assert.Equals("todo", todoTasks[0].Status);
        }

        public class InMemoryTaskRepository : ITaskRepos
        {
            private List<TaskDetail> _tasks = new List<TaskDetail>();

            public List<TaskDetail> GetAllTasks()
            {
                return _tasks;
            }

            public void SaveTasks(List<TaskDetail> tasks)
            {
                _tasks = tasks;
            }
        }
    }
}
