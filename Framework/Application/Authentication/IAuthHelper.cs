namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignInAsync(OperatorAuthViewModel account);
    }
}