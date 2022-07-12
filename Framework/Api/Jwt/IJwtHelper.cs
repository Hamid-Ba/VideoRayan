namespace Framework.Api.Jwt
{
    public interface IJwtHelper
	{
		string SignIn(JwtDto command);
	}
}