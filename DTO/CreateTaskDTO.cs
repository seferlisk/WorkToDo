﻿using System;
using System.ComponentModel.DataAnnotations;
using WorkToDo.Helper;
using WorkToDo.Models;


namespace WorkToDo.DTO
{
    public class CreateTaskDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateNotPast(ErrorMessage = "The due date cannot be in the past.")]
        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; }

        public string AssignedTo { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }// Add categories here
    }

}