using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Repos.Interfaces;
using TaskTracker.Services.Interfaces;

namespace TaskTracker.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly ITaskRepos _taskRepository;
        private static int idCounter = 1;

        public TaskServices(ITaskRepos taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTask(string description)
        {
            var tasks = _taskRepository.GetAllTasks();
            int id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : idCounter;
            TaskDetail task = new TaskDetail
            {
                Id = id,
                Description = description,
                Status = "todo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            tasks.Add(task);
            _taskRepository.SaveTasks(tasks);
            Console.WriteLine($"A new task with taskid - {id} is added ", Console.ForegroundColor = ConsoleColor.Green);
        }

        public void UpdateTask(int id, string description)
        {
            var tasks = _taskRepository.GetAllTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Description = description;
                task.UpdatedAt = DateTime.Now;
                _taskRepository.SaveTasks(tasks);
                Console.WriteLine($"{id} is updated with {description}",Console.ForegroundColor = ConsoleColor.Green);
            }
            else
            {
                throw new ArgumentException("Task Id is not present in JSON");
            }
        }
        public void DeleteTask(int id)
        {
            var tasks = _taskRepository.GetAllTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                _taskRepository.SaveTasks(tasks);
                Console.WriteLine($"{id} is DELETED ", Console.ForegroundColor = ConsoleColor.Green);
            }
            else
            {
                throw new ArgumentException("Task Id is not present in JSON");
            }
        }
        public List<TaskDetail> GetTasksByStatus(string status)
        {
            var tasks = _taskRepository.GetAllTasks();
            return tasks.Where(t => t.Status == status).ToList();
        }

        public List<TaskDetail> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }
        public void MarkTaskInProgress(int id)
        {
            var tasks = _taskRepository.GetAllTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = "in progress";
                task.UpdatedAt = DateTime.Now;
                _taskRepository.SaveTasks(tasks);
            }
            else
            {
                throw new ArgumentException("Task Id is not present in JSON");
            }
        }

        public void MarkTaskDone(int id)
        {
            var tasks = _taskRepository.GetAllTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = "done";
                task.UpdatedAt = DateTime.Now;
                _taskRepository.SaveTasks(tasks);
            }
            else
            {
                throw new ArgumentException("Task Id is not present in JSON");
            }
        }
    }
}
