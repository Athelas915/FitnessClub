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
        Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(string userId); //potentially to move somewhere else
        Task<IdentityResult> AddLogin(string userId); //potentially to move somewhere else
        Task<IdentityResult> RemoveLogin(string userId, string loginProvider, string providerKey); //potentially to move somewhere else
        //SignInManager wrapped methods
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null); //potentially to move somewhere else
        Task<SignInResult> ExternalLoginSignIn(ExternalLoginInfo info); //potentially to move somewhere else
        Task SignOut(); //potentially to move somewhere else
        Task<SignInResult> SignIn(string email, string password, bool rememberMe, bool lockoutOnFailure = false); //potentially to move somewhere else
    }
}
