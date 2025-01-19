using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Repos.Interfaces;

namespace TaskTracker.Repos
{
    public class TaskRepos : ITaskRepos
    {
        private readonly string jsonFilePath = "D:\\Dotnet\\TaskTracker\\task.json";

        public List<TaskDetail> GetAllTasks()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<TaskDetail>();
            }

            string jsonContent = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<TaskDetail>>(jsonContent);
        }

        public void SaveTasks(List<TaskDetail> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
