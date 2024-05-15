using Microsoft.AspNetCore.Mvc;
using starter_serv.BindingModel.AuthLogin;
using starter_serv.BusinessProviders;
using starter_serv.Constant;
using starter_serv.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace starter_serv.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateBusinessProviders _businessProviders;

        [ExcludeFromCodeCoverage]
        public AuthenticateController(IAuthenticateBusinessProviders businessProviders)
        {
            _businessProviders = businessProviders ?? throw new ArgumentNullException(nameof(businessProviders));
        }

        [HttpPost(template: "Login")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<AuthenticViewModel>))]
        public async Task<IActionResult> AuthLogin([FromBody] AuthBindingModel data)
        {
            ResponseOneDataViewModel<AuthenticViewModel> response = new ResponseOneDataViewModel<AuthenticViewModel>();
            try
            {
                response = await _businessProviders.AuthProcess(data);
            }
            catch (Exception ex)
            {
                response.StatusCode = ApplicationConstant.STATUS_CODE_ERROR;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    response.Message = ex.InnerException.Message;
                }
            }
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet(template: "GetAuth")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<UserCliamTokenViewModel>))]
        public async Task<IActionResult> GetAuth()
        {
            ResponseOneDataViewModel<UserCliamTokenViewModel> response = new ResponseOneDataViewModel<UserCliamTokenViewModel>();

            // Get the token from the Authorization header
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Decode the token
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);


                UserCliamTokenViewModel userCliamTokenViewModel = new UserCliamTokenViewModel()
                {
                    Id = decodedToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value,
                    Name = decodedToken.Claims.FirstOrDefault(c => c.Type == "Name")?.Value,
                    Age = decodedToken.Claims.FirstOrDefault(c => c.Type == "Age")?.Value,
                    Email = decodedToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                    StatusUser = decodedToken.Claims.FirstOrDefault(c => c.Type == "StatusUser")?.Value,
                };

                response.StatusCode = ApplicationConstant.STATUS_CODE_OK;
                response.Message = ApplicationConstant.STATUS_MSG_OK;
                response.Data = userCliamTokenViewModel;
            }
            else
            {
                response.StatusCode = ApplicationConstant.STATUS_CODE_NOT_FOUND;
                response.Message = "Token not found";

            }

            

            return StatusCode(response.StatusCode, response);
        }
    }
    
}
