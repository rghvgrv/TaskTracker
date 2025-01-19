using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Services.Interfaces
{
    public interface ITaskServices
    {
        void AddTask(string description);
        void UpdateTask(int id, string description);
        void DeleteTask(int id);
        List<TaskDetail> GetTasksByStatus(string status);
        List<TaskDetail> GetAllTasks();
        void MarkTaskInProgress(int id);
        void MarkTaskDone(int id);
    }
}
