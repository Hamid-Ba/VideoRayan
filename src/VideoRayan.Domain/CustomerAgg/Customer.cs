using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Domain.CustomerAgg
{
    public class Customer : EntityBase
    {
        public string? Title { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Mobile { get; private set; }
        public string? PhoneCode { get; private set; }
        public string? Logo { get; private set; }
        public string? Image { get; private set; }
        public string? Email { get; private set; }
        public bool IsActive { get; private set; }
        public CustomerType Type { get; private set; }
        public DateTime LoginExpireDate { get; private set; }

        public List<Meeting>? Meetings { get; set; }
        public List<Category>? Categories { get; private set; }
        public List<Audience>? Audiences { get; private set; }
        public List<FaceToFace>? FaceToFaces { get; private set; }

        public Customer(string title, string mobile, string phoneCode, string logo, string image, string firstName, string lastName, string email, CustomerType type = 0)
        {
            Guard(mobile);

            Title = title;

            Mobile = mobile;
            PhoneCode = phoneCode;

            if (!string.IsNullOrWhiteSpace(logo))
                Logo = logo;

            if (!string.IsNullOrWhiteSpace(image))
                Image = image;

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = true;
            Type = type;
            LoginExpireDate = DateTime.Now.AddMinutes(5);
        }

        public void Edit(string title, string phone, string logo, string image, string firstName, string lastName, string email, CustomerType type = 0)
        {
            Guard(phone);

            Title = title;
            Mobile = phone;

            if (!string.IsNullOrWhiteSpace(logo))
                Logo = logo;

            if (!string.IsNullOrWhiteSpace(image))
                Image = image;

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = true;
            Type = type;
        }

        public void EditLogo(string logo)
        {
            if (!string.IsNullOrWhiteSpace(logo))
                Logo = logo;
        }

        public void EditImage(string image)
        {
            if (!string.IsNullOrWhiteSpace(image))
                Image = image;
        }

        public void Edit(string phone, string firstName, string lastName, string email)
        {
            Mobile = phone;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static Customer Register(string phone, string phoneCode) => new("", phone, phoneCode, "", "", "", "", "");

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