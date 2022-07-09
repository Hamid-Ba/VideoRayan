namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignInAsync(VisitorAuthViewModel account);
        void SignInAsync(OperatorAuthViewModel account);
    }
}