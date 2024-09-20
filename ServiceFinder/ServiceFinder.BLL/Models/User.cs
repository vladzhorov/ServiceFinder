namespace ServiceFinder.BLL.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Auth0Id { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }

}
