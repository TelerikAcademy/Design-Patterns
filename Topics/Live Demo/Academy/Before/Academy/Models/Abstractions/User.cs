using System;

namespace Academy.Models.Abstractions
{
    public abstract class User : IUser
    {
        private string username;

        public User(string username)
        {
            this.Username = username;
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The provided first name is not valid!");
                }

                if (value.Length < 3 || value.Length > 16)
                {
                    throw new ArgumentException("User's username should be between 3 and 16 symbols long!");
                }

                this.username = value;
            }
        }
    }
}
