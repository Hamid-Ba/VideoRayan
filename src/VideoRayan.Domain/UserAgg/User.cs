﻿using Framework.Domain;

namespace VideoRayan.Domain.UserAgg
{
    public class User : EntityBase
	{
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Mobile { get; private set; }
        public string? PhoneCode { get; private set; }
        public string? Email { get; private set; }
        public bool IsActive { get;private set; }
        public DateTime LoginExpireDate { get; private set; }

        public User(string phone, string phoneCode, string firstName,string lastName, string email)
        {
            Guard(phone);

            Mobile = phone;
            PhoneCode = phoneCode;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = true;
        }

        public static User Register(string phone, string phoneCode) => new(phone, phoneCode,"", "", "");

        public void Edit(string firstName,string lastName,string mobile, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Mobile = mobile;

            LastUpdateDate = DateTime.Now;
        }

        public void ControlActivation(bool isActive)
        {
            IsActive = isActive;
            LastUpdateDate = DateTime.Now;
        }

        public void SetAccessToLoginDate(DateTime accessTime) => LoginExpireDate = accessTime;

        public void ChangePhoneCode(string newPhoneCode) => PhoneCode = newPhoneCode;

        private void Guard(string phone) { if (string.IsNullOrWhiteSpace(phone)) throw new Exception("Phone Can Not Be Null Or Empty"); }
    }
}