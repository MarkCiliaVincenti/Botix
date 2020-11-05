using System;

namespace Botix.Bot.Core.Domains
{
    public class User : Entity
    {
        public User() { }

        public static User Create(long identifier, string userName) =>
            new User
            {
                Identifier = identifier,
                Username = userName,
                CreatedAt = DateTime.UtcNow
            };

        public void UpdateUserName(string userName)
        {
            if (userName != null)
            {
                Username = userName;
                UpdateAt = DateTime.UtcNow;
            }
        }

        public void UpdateFirstName(string firstName)
        {
            if (firstName != null)
            {
                FirstName = firstName;
                UpdateAt = DateTime.UtcNow;
            }
        }

        public long Id { get; protected set; }

        public long Identifier { get; protected set; }

        public string Username { get; protected set; }

        public string FirstName { get; protected set; }
    }
}
