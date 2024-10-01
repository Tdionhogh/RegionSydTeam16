using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace RegionSydTeam16.Model
{
    public class LoginService
    {
        private List<User> _users = new List<User>
    {
        new User { UserID = 1, UserName = "admin", PassWordHash = BCrypt.Net.BCrypt.HashPassword("admin123") },
        new User { UserID = 2, UserName = "user", PassWordHash = BCrypt.Net.BCrypt.HashPassword("user123") }
    };
        public bool Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PassWordHash);
        }

        public void Register(string username, string password)
        {
            var newUser = new User
            {
                UserName = username,
                PassWordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _users.Add(newUser);
        }
    }

}
