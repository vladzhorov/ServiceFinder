using System.ComponentModel.DataAnnotations;

namespace ServiceFinder.Auth.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Auth0Id { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
