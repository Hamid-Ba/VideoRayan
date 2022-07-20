namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignIn(OperatorAuthViewModel account);
    }
}