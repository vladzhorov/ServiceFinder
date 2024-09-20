using System.ComponentModel.DataAnnotations;

namespace ServiceFinder.Auth.Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
