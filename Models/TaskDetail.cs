﻿namespace TaskTracker.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
