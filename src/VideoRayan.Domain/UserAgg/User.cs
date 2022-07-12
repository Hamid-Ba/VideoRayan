using System;
using Framework.Domain;

namespace VideoRayan.Domain.UserAgg
{
	public class User : EntityBase
	{
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? UserName { get; private set; }
        public string? Mobile { get; private set; }
        public string? PhoneCode { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get;private set; }

        public User(string firstName, string lastName, string userName, string mobile, string? phoneCode, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Mobile = mobile;
            PhoneCode = phoneCode;
            Email = email;
            Password = password;
        }

        public User Register(string userName, string email, string mobile, string password) => new("", "", userName, mobile, "", email, password);

        public void Edit(string firstName, string lastName, string userName, string mobile, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Mobile = mobile;
            Email = email;

            LastUpdateDate = DateTime.Now;
        }
    }
}