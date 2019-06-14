using CFT.API.Models;

namespace CFT.API.Interfaces
{
    public interface ILoginRepository : IRepository<UserModel>
    {
        Users Login(LoginModel login);

        bool ActivateUser(UserModel user);
        bool DeactivateUser(UserModel user);
    }
}
