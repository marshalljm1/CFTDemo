namespace CFT.Promotions.Core.Models
{
    public partial class Users
    {
        public new int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
