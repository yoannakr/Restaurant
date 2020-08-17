using System.Text;
using Restaurant.Interfaces;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Database.Services;
using System.Security.Cryptography;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class UserService : IUserService, IHashPassword
    {
        #region Declarations

        private readonly IUserDbService userDb;

        #endregion

        #region Constructors

        public UserService()
        {
            userDb = new UserDbService();
        }

        #endregion

        #region Methods

        public void CreateUser(string name, string username, string password, int roleId)
        {
            string securityPassword = ComputePasswordHashing(password);

            userDb.CreateUser(name, username, securityPassword, roleId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userDb.GetAllUsers();
        }

        public string ComputePasswordHashing(string rowPassword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rowPassword));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
                return builder.ToString();
            }
        }

        #endregion
    }
}
