using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker
{
    class TaskTracker
    {
        private readonly static string jsonFilePath = "D:\\Dotnet\\TaskTracker\\task.json";

        static void Main(string[] args)
        {
            List<TaskDetail> taskDetails = new List<TaskDetail>();
            string[] commands = { "add", "update", "delete", "mark-in-progress", "mark-done", "list" };
            string taskDescription = "";

            // Get the command from the user
            Console.Write("Write Command : ");
            string? instruction = Console.ReadLine();

            // Split the instruction on basis of space :
            string[] words = instruction.Split(' ');

            //To check if command is proper or not 
            if (!commands.Contains(words[0]))
            {
                Console.WriteLine("Give Proper Command i.e. add,update,delete,mark-in-progress,mark-done,list");
            }

            if (words[0] == "add")
            {
                taskDescription = HandleTaskDescription(instruction);
                AddTask(taskDescription);
            }
            else if (words[0] == "update")
            {
                taskDescription = HandleTaskDescription(instruction);
                int id = int.Parse(words[1]);
                UpdateTask(taskDescription, id);
            }
            else if (words[0] == "delete")
            {
                int id = int.Parse(words[1]);
                DeleteTask(id);
            }
            else if (instruction == "list") 
            {
                var allTask = ReadJson();
                foreach (var task in allTask)
                {
                    Console.WriteLine($"Task ID : {task.Id}");
                    Console.WriteLine($"Task Description : {task.Description}");
                    Console.WriteLine($"Task Status : {task.Status}");
                    Console.WriteLine($"Task Created at : {task.CreatedAt}");
                    Console.WriteLine($"Task Updated at : {task.UpdatedAt}");
                    Console.WriteLine("------------------------------------");
                }
            }
            else if(instruction == "list done")
            {
                string status = "done";
                FindTaskByStatus(status);

            }
            else if (instruction == "list todo")
            {
                string status = "todo";
                FindTaskByStatus(status);
            }
            else if (instruction == "list progress")
            {
                string status = "progress";
                FindTaskByStatus(status);
            }
            else if (words[0] == "mark-in-progress")
            {
                int id = int.Parse(words[1]);
                string status = "progess";
                UpdateStatus(id,status);
            }
            else if (words[0] == "mark-done")
            {
                string status = "done";
                int id = int.Parse(words[1]);
                UpdateStatus(id,status);
            }
        }

        private static void UpdateStatus(int taskid,string status)
        {
            List<TaskDetail> existingtaskDetail = ReadJson();
            if (existingtaskDetail != null)
            {
                var taskFound = existingtaskDetail.FirstOrDefault(t => t.Id == taskid);
                if (taskFound != null)
                {
                    taskFound.Status = status ;
                    taskFound.UpdatedAt = DateTime.Now;
                    WriteInJson(existingtaskDetail);
                    Console.WriteLine("Task Updated");
                }
                else
                {
                    throw new ArgumentException("Id is not there is JSON");
                }
            }
        }

        private static void FindTaskByStatus(string status)
        {
            var allTask = ReadJson();

            var statusofTask = allTask.FindAll(x => x.Status == status);

            foreach (var task in statusofTask)
            {
                Console.WriteLine($"Task ID : {task.Id}");
                Console.WriteLine($"Task Description : {task.Description}");
                Console.WriteLine($"Task Status : {task.Status}");
                Console.WriteLine($"Task Created at : {task.CreatedAt}");
                Console.WriteLine($"Task Updated at : {task.UpdatedAt}");
                Console.WriteLine("------------------------------------");
            }
        }

        private static void DeleteTask(int taskid)
        {
            List<TaskDetail> existingtaskDetail = ReadJson();
            if (existingtaskDetail != null)
            {
                var taskFound = existingtaskDetail.FirstOrDefault(t => t.Id == taskid);
                if (taskFound != null)
                {
                    existingtaskDetail.Remove(taskFound);
                    WriteInJson(existingtaskDetail);
                    Console.WriteLine("Task is deleted", ConsoleColor.Green);
                }
                else
                {
                    throw new ArgumentException("Task Id is not present in JSON");
                }
            }
        }
        private static string HandleTaskDescription(string instruction)
        {
            string taskDescription = "";
            
            int count = instruction.Count(c => c == '"');

            if(count > 2)
            {
                throw new ArgumentException("There are more than 2 ");
            }

            int firstQuoteIndex = instruction.IndexOf('"');
            if (firstQuoteIndex < 0)
            {
                throw new ArgumentException("Add Proper Instruction");
            }
            int lastQuoteIndex = instruction.LastIndexOf('"');
            if (lastQuoteIndex < 0 || firstQuoteIndex == lastQuoteIndex)
            {
                throw new ArgumentException("Close the instruction");
            }
            else
            {
                if (lastQuoteIndex > firstQuoteIndex)
                {
                    taskDescription = instruction.Substring(firstQuoteIndex + 1, lastQuoteIndex - firstQuoteIndex - 1);
                    if (taskDescription == "")
                    {
                        throw new ArgumentException("Add Proper Task Description");
                    }
                }
                return taskDescription;
            }
        }

        private static void UpdateTask(string taskDescription, int taskid)
        {
            List<TaskDetail> existingtaskDetail = ReadJson();
            if (existingtaskDetail != null)
            {
                var taskFound = existingtaskDetail.FirstOrDefault(t => t.Id == taskid);
                if (taskFound != null)
                {
                    taskFound.Description = taskDescription;
                    taskFound.UpdatedAt = DateTime.Now;
                    WriteInJson(existingtaskDetail);
                    Console.WriteLine("Task Updated");
                }
                else
                {
                    throw new ArgumentException("Id is not there is JSON");
                }
            }
        }

        private static void AddTask(string taskDescription)
        {
            int id = 0;
            List<TaskDetail> taskDetails = ReadJson();
            if (taskDetails.Count > 0)
            {
                id = taskDetails[taskDetails.Count - 1].Id;
            }
            TaskDetail taskDetail = new()
            {
                Id = id+1,
                Status = "todo",
                Description = taskDescription,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            taskDetails.Add(taskDetail);
            WriteInJson(taskDetails);
        }

        private static void WriteInJson(List<TaskDetail> taskDetails)
        {
            string jsonString = JsonSerializer.Serialize(taskDetails);

            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));

                // Write to the file
                File.WriteAllText(jsonFilePath, jsonString);

                Console.WriteLine("Details saved to task.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        private static List<TaskDetail> ReadJson()
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<TaskDetail>>(jsonContent);
        }
    }
}
