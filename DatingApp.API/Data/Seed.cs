using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext dataContext){
            if(!dataContext.Users.Any()) {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users) {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("123456", out passwordHash, out passwordSalt);

                    user.PasswordSalt = passwordSalt;
                    user.PasswordHash = passwordHash;
                    user.UserName = user.UserName.ToLower();

                    dataContext.Users.Add(user);
                }
                dataContext.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}