using starter_serv.BindingModel.AuthLogin;
using starter_serv.Model;
using starter_serv.Models;
using starter_serv.ViewModel;
using System.IdentityModel.Tokens.Jwt;

namespace starter_serv.BusinessProviders
{
    public interface IAuthenticateBusinessProviders
    {
        public Task<ResponseOneDataViewModel<AuthenticViewModel>> AuthProcess(AuthBindingModel data);
        public JwtSecurityToken GetToken(UsrUser data);
        public UserCliamTokenViewModel GetUserClaimToken();
    }
}
