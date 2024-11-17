﻿namespace WorkToDo.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Assignment> AssignedTasks { get; set; } // Tasks assigned to the user
    }

}
