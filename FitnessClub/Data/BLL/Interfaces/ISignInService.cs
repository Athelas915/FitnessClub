using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ISignInService
    {
        Task<ExternalLoginInfo> GetExternalLoginInfo(); //potentially to move somewhere else
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemes(); //potentially to move somewhere else
        Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(int userId); //potentially to move somewhere else
        Task<IdentityResult> AddLogin(int userId); //potentially to move somewhere else
        Task<IdentityResult> RemoveLogin(int userId, string loginProvider, string providerKey); //potentially to move somewhere else
        //SignInManager wrapped methods
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, int userId = -1); //potentially to move somewhere else
        Task<SignInResult> ExternalLoginSignIn(ExternalLoginInfo info); //potentially to move somewhere else
        Task SignOut(); //potentially to move somewhere else
        Task<SignInResult> SignIn(string email, string password, bool rememberMe, bool lockoutOnFailure = false); //potentially to move somewhere else
    }
}
