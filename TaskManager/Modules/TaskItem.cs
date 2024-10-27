﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Modules
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime Duedate { get; set; }
        [Required]
        public string Priority { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
         public ICollection<CheakList>? cheakLists { get; set; }
    }
}