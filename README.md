
# TaskTracker

TaskTracker is a C# application designed to help users manage and track their tasks efficiently - https://github.com/rghvgrv/TaskTracker

## Features

- **Task Management**: Create, update, and delete tasks.
- **Command-Line Interface**: Interact with the application through a CLI for quick task management.
- **Modular Architecture**: Organized into Models, Repositories, and Services for scalability and maintainability.

## Project Structure

The project is organized as follows:

- **Models/**: Contains the data models used in the application.
- **Repos/**: Includes repository classes responsible for data access.
- **Services/**: Contains business logic and services.
- **UnitTests/**: Holds unit tests for various components.
- **TaskTracker.cs**: The main entry point of the application.
- **TaskTrackerCLI.cs**: Handles command-line interactions.
- **TaskTracker.sln**: Visual Studio solution file.
- **task.json**: Configuration file for tasks.

## Getting Started

To get started with TaskTracker:

1. **Clone the repository**:

   ```bash
   git clone https://github.com/rghvgrv/TaskTracker.git
   ```

2. **Navigate to the project directory**:

   ```bash
   cd TaskTracker
   ```

3. **Build the solution**:

   Open `TaskTracker.sln` in Visual Studio and build the solution.

4. **Run the application**:

   Execute the application through Visual Studio or via the command line:

   ```bash
   dotnet run
   ```

## Usage

After running the application, use the command-line interface to manage your tasks.

For example:

```bash
# Adding a new task
task-cli add "Buy groceries"
Output: Task added successfully (ID: 1)

# Updating and deleting tasks
task-cli update 1 "Buy groceries and cook dinner"
task-cli delete 1

# Marking a task as in progress or done
task-cli mark-in-progress 1
task-cli mark-done 1

# Listing all tasks
task-cli list

# Listing tasks by status
task-cli list done
task-cli list todo
task-cli list in-progress
```

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## Contact

For any inquiries or feedback, please contact [rghvgrv](https://github.com/rghvgrv).

