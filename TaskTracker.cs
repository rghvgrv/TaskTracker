using TaskTracker.Repos;
using TaskTracker.Repos.Interfaces;
using TaskTracker.Services;
using TaskTracker.Services.Interfaces;

namespace TaskTracker
{
    class TaskTracker
    {
        private readonly static string jsonFilePath = "D:\\Dotnet\\TaskTracker\\task.json";

        static void Main(string[] args)
        {
            ITaskRepos taskRepository = new TaskRepos();
            ITaskServices taskService = new TaskServices(taskRepository);
            TaskTrackerCLI cli = new TaskTrackerCLI(taskService);
            cli.Run();
        }
    }
}
