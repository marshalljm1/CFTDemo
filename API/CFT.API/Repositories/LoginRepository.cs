using System;
using System.Linq;
using CFT.API.Interfaces;
using CFT.API.Models;

namespace CFT.API.Repositories
{
    public class LoginRepository : Repository<UserModel>, ILoginRepository
    {
        public LoginRepository(CFTContext context) : base(context)
        {
        }

        public CFTContext Cft => Context as CFTContext;

        public bool ActivateUser(UserModel user)
        {
            try
            {
                Cft.Users.Single(x => x.Id.Equals(user.Id)).Active = true;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool DeactivateUser(UserModel user)
        {
            try
            {
                Cft.Users.Single(x => x.Id.Equals(user.Id)).Active = false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Users Login(LoginModel login)
        {
            try
            {
                var loggedInUser = Cft.Users.Single(x =>
                    x.UserName.Equals(login.Username) && x.PasswordHash.Equals(login.Password));

                return loggedInUser;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }

}
