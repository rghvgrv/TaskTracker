using System.Text.Json;
using TaskTracker.Models;
using TaskTracker.Repos.Interfaces;

namespace TaskTracker.Repos
{
    public class TaskRepos : ITaskRepos
    {
        private readonly string filePath;

        public TaskRepos(string _filePath = "tasks.json")
        {
            filePath = _filePath;
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public List<TaskDetail> GetAllTasks()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskDetail>();
            }

            string jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskDetail>>(jsonContent);
        }

        public void SaveTasks(List<TaskDetail> tasks)
        {
            string jsonContent = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}