using MES.Acquintance;

namespace MES.Data
{
    class User : IUser
    {
        private string username;
        private string password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
