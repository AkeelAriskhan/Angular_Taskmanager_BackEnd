using System.ComponentModel.DataAnnotations;

namespace TaskManager.Modules
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstLane { get; set; }
        public string SecondLane { get; set; }
        public  string City { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

    }
}
