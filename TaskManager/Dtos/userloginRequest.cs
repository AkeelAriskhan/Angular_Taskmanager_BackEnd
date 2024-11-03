using TaskManager.Modules;

namespace TaskManager.Dtos
{
    public class userloginRequest
    {

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
