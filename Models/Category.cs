﻿namespace WorkToDo.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Task> Tasks { get; set; } // Navigation property
    }

}
