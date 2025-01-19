using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Services.Interfaces;

namespace TaskTracker
{
    public class TaskTrackerCLI
    {
        private readonly ITaskServices _taskService;

        public TaskTrackerCLI(ITaskServices taskService)
        {
            _taskService = taskService;
        }
        public void Run()
        {
            string[] commands = { "add", "update", "delete", "mark-in-progress", "mark-done", "list", "list done", "list todo", "list progress" };
            DisplayHelp();
            Console.Write("task-cli ");
            string instruction = Console.ReadLine();
            string[] words = instruction.Split(' ');

            if (!commands.Contains(words[0]))
            {
                Console.WriteLine("Give Proper Command i.e. add,update,delete,mark-in-progress,mark-done,list");
            }

            try
            {
                switch (words[0])
                {
                    case "add":
                        string taskDescription = HandleTaskDescription(instruction);
                        _taskService.AddTask(taskDescription);
                        break;
                    case "update":
                        taskDescription = HandleTaskDescription(instruction);
                        int id = int.Parse(words[1]);
                        _taskService.UpdateTask(id, taskDescription);
                        break;
                    case "delete":
                        id = int.Parse(words[1]);
                        _taskService.DeleteTask(id);
                        break;
                    case "list":
                        if (instruction == "list")
                        {
                            var allTasks = _taskService.GetAllTasks();
                            DisplayTasks(allTasks);
                        }
                        else if (instruction == "list done")
                        {
                            var doneTasks = _taskService.GetTasksByStatus("done");
                            DisplayTasks(doneTasks);
                        }
                        else if (instruction == "list todo")
                        {
                            var todoTasks = _taskService.GetTasksByStatus("todo");
                            DisplayTasks(todoTasks);
                        }
                        else if (instruction == "list progress")
                        {
                            var progressTasks = _taskService.GetTasksByStatus("in progress");
                            DisplayTasks(progressTasks);
                        }
                        break;
                    case "mark-in-progress":
                        id = int.Parse(words[1]);
                        _taskService.MarkTaskInProgress(id);
                        break;
                    case "mark-done":
                        id = int.Parse(words[1]);
                        _taskService.MarkTaskDone(id);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        private string HandleTaskDescription(string instruction)
        {
            int firstQuoteIndex = instruction.IndexOf('"');
            int lastQuoteIndex = instruction.LastIndexOf('"');
            if (firstQuoteIndex > -1 && lastQuoteIndex > firstQuoteIndex)
            {
                return instruction.Substring(firstQuoteIndex + 1, lastQuoteIndex - firstQuoteIndex - 1);
            }
            throw new ArgumentException("Add Proper Task Description");
        }

        private void DisplayTasks(List<TaskDetail> tasks)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("| ID  | Description                 | Status      | Created At           | Updated At           |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            foreach (var task in tasks)
            {
                switch (task.Status)
                {
                    case "todo":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "in progress":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "done":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }
                Console.WriteLine("| {0,-3} | {1,-25} | {2,-11} | {3,-19} | {4,-19} |", task.Id, task.Description, task.Status, task.CreatedAt, task.UpdatedAt);
                Console.ResetColor();
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
        }
        private void DisplayHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("add \"task description\" - Adds a new task");
            Console.WriteLine("update <id> \"task description\" - Updates an existing task");
            Console.WriteLine("delete <id> - Deletes a task");
            Console.WriteLine("mark-in-progress <id> - Marks a task as in progress");
            Console.WriteLine("mark-done <id> - Marks a task as done");
            Console.WriteLine("list - Lists all tasks");
            Console.WriteLine("list done - Lists all tasks with status 'done'");
            Console.WriteLine("list todo - Lists all tasks with status 'todo'");
            Console.WriteLine("list progress - Lists all tasks with status 'in progress'");
            Console.WriteLine("------------------------------------------------");
        }
    }
}