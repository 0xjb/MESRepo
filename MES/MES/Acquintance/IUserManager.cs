namespace MES.Acquintance
{
    interface IUserManager
    {
        /// <summary>
        /// Adds a user to the collection of users
        /// </summary>
        /// <param name="user"></param>
        void AddUser(IUser user);

        /// <summary>
        /// Authenticates the user information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IUser AuthenticateUserInformation(string username, string password);
    }
}
