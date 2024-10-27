using System.ComponentModel.DataAnnotations;

namespace TaskManager.Modules
{
    public class CheakList
    {
        [Key ]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public bool ISDone { get; set; }

        public int TaskId { get; set; }
        public TaskItem? Task {  get; set; }
    }
}
