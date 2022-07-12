namespace Framework.Api.Jwt
{
    public class JwtDto
	{
		public Guid Id { get; set; }
		public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PhoneCode { get; set; }
        public DateTime LoginExpireDate { get; set; }
	}
}