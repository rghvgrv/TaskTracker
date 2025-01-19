using TaskTracker.Models;

namespace TaskTracker.Repos.Interfaces
{
    public interface ITaskRepos
    {
        List<TaskDetail> GetAllTasks();
        void SaveTasks(List<TaskDetail> tasks);
    }
}
