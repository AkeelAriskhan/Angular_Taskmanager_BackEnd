using System.ComponentModel.DataAnnotations;

namespace TaskManager.Modules
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Address? Address { get; set; }
        public ICollection<TaskItem>? TaskItems { get; set; }
    }
}
