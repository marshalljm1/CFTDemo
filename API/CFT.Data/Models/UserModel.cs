using System;

namespace CFT.Data.Models
{
    public partial class UserModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool? Active { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
