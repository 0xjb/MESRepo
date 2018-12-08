using MES.Acquintance;
using System.Collections.Generic;

namespace MES.Data
{
    public class UserManager : IUserManager
    {
        IDictionary<string, IUser> users;

        public UserManager()
        {
            users = new Dictionary<string, IUser>();
            IUser user = new User("", "");
            AddUser(user);
        }

        public void AddUser(IUser user)
        {
            users.Add(user.Username, user);
        }

        public IUser AuthenticateUserInformation(string username, string password)
        {
            //TODO er try/catch nødvendig?
            if (users.ContainsKey(username))
            {
                IUser user;
                users.TryGetValue(username, out user);
                if (user.Password.Equals(password))
                {
                    return user;
                }
                else { return null; }
            }
            else { return null; }
        }
    }
}
