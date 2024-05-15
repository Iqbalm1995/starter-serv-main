using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Cms;
using starter_serv.BindingModel.AuthLogin;
using starter_serv.Constant;
using starter_serv.DataProviders;
using starter_serv.Model;
using starter_serv.Models;
using starter_serv.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace starter_serv.BusinessProviders
{
    public class AuthenticateBusinessProviders : IAuthenticateBusinessProviders
    {
        private readonly IUsersDataProvider _UsersDataProvider;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticateBusinessProviders> _logger;

        [ExcludeFromCodeCoverage]
        public AuthenticateBusinessProviders(
            IUsersDataProvider usersDataProvider, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuthenticateBusinessProviders> logger)
        {
            _UsersDataProvider = usersDataProvider ?? throw new ArgumentNullException(nameof(usersDataProvider));
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ResponseOneDataViewModel<AuthenticViewModel>> AuthProcess(AuthBindingModel data)
        {
            ResponseOneDataViewModel<AuthenticViewModel> result = new ResponseOneDataViewModel<AuthenticViewModel>();
            AuthenticViewModel authResponse = new AuthenticViewModel();

            UsrUser dataUsers = await _UsersDataProvider.GetByEmail(data.Email);

            if (dataUsers == null)
            {
                _logger.LogInformation($"{data.Email} failed login : {ApplicationConstant.STATUS_MSG_NOT_FOUND}");

                result.StatusCode = ApplicationConstant.STATUS_CODE_NOT_FOUND;
                result.Message = ApplicationConstant.STATUS_MSG_NOT_FOUND;
            }
            else
            { 
                var token = GetToken(dataUsers);

                authResponse.ApiKey = new JwtSecurityTokenHandler().WriteToken(token);
                authResponse.Expiration = token.ValidTo;
                //authResponse.UsersInfo = await _UsersDataProvider.ViewModeling(dataUsers);

                _logger.LogInformation($"{data.Email} success login");

                result.StatusCode = ApplicationConstant.STATUS_CODE_OK;
                result.Message = ApplicationConstant.STATUS_MSG_OK;
                result.Data = authResponse;
            }

            return result;
        }

        public JwtSecurityToken GetToken(UsrUser data)
        {

            var authClaims = new List<Claim>
            {
                new Claim(type: "Id", value : (data.Id != null ? data.Id.ToString() : "-")),
                new Claim(type: "Name", value : (data.Name != null ? data.Name : "-")),
                new Claim(type: "Age", value : (data.Age != null ? data.Age.ToString() : "-")),
                new Claim(type: "Email", value : (data.Email != null ? data.Email : "-")),
                new Claim(type: "StatusUser", value : (data.StatusUser != null ? data.StatusUser.ToString() : "-")),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JsonWebTokenKeys:ValidIssuer"],
                audience: _configuration["JsonWebTokenKeys:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                //expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public UserCliamTokenViewModel GetUserClaimToken()
        {
            UserCliamTokenViewModel result = new UserCliamTokenViewModel();

            // Get the token from the Authorization header
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Decode the token
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);

                result = new UserCliamTokenViewModel()
                {
                    Id = decodedToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value,
                    Name = decodedToken.Claims.FirstOrDefault(c => c.Type == "Name")?.Value,
                    Age = decodedToken.Claims.FirstOrDefault(c => c.Type == "Age")?.Value,
                    Email = decodedToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                    StatusUser = decodedToken.Claims.FirstOrDefault(c => c.Type == "StatusUser")?.Value,

                };

            }
            else
            {
                result = null;

            }

            return result;
        }
    }
}
