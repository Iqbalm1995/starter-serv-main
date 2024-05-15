using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using starter_serv.BindingModel.Users;
using starter_serv.BusinessProviders;
using starter_serv.Constant;
using starter_serv.Model;
using starter_serv.ViewModel;
using starter_serv.ViewModel.Users;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.Controllers.v1
{
    [Authorize]
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusinessProviders _BusinessProviders;

        [ExcludeFromCodeCoverage]
        public UsersController(IUsersBusinessProviders usersBusinessProvider)
        {
            _BusinessProviders = usersBusinessProvider ?? throw new ArgumentNullException(nameof(usersBusinessProvider));
        }

        [HttpPost(template: "GetPagedList")]
        [ProducesResponseType(200, Type = typeof(ResponseViewModel<UsersViewModel>))]
        public async Task<IActionResult> GetPagedList([FromBody] RequestPagedFilterModel request)
        {
            ResponseViewModel<UsersViewModel> response = new ResponseViewModel<UsersViewModel>();
            try
            {
                response = await _BusinessProviders.GetList(request);
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

        [HttpGet(template: "{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<UsersViewModel>))]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseOneDataViewModel<UsersViewModel> response = new ResponseOneDataViewModel<UsersViewModel>();
            try
            {
                response = await _BusinessProviders.GetById(id);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost(template: "UpdateAvatar/{UserId}")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<UsersViewModel>))]
        public async Task<IActionResult> UpdateAvatar(int UserId, IFormFile FileUpload)
        {
            ResponseOneDataViewModel<UsersViewModel> response = new ResponseOneDataViewModel<UsersViewModel>();
            try
            {
                response = await _BusinessProviders.UpdateAvatar(UserId, FileUpload);
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

        [HttpPost(template: "Insert")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<string>))]
        public async Task<IActionResult> Insert([FromBody] InsertUserBindingModel data)
        {
            ResponseOneDataViewModel<string> response = new ResponseOneDataViewModel<string>();
            try
            {
                response = await _BusinessProviders.Insert(data);
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

        [HttpPatch(template: "Update")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<string>))]
        public async Task<IActionResult> Update([FromBody] UpdateUserBindingModel data)
        {
            ResponseOneDataViewModel<string> response = new ResponseOneDataViewModel<string>();
            try
            {
                response = await _BusinessProviders.Update(data);
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

        [HttpDelete(template: "Delete/{UserId}")]
        [ProducesResponseType(200, Type = typeof(ResponseOneDataViewModel<string>))]
        public async Task<IActionResult> Delete(Int32 UserId)
        {
            ResponseOneDataViewModel<string> response = new ResponseOneDataViewModel<string>();
            try
            {
                response = await _BusinessProviders.Delete(UserId);
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

    }
}
