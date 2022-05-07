using AspNetCore_RestAPI.DTOs.V1;
using AspNetCore_RestAPI.Routes;
using AspNetCore_RestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_RestAPI.Controllers.V1
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest requestBody)
        {
            var authenticationResponse = await this.identityService.RegisterUserAsync(requestBody.Email, requestBody.Password);
            if (authenticationResponse.Success == false)
            {
                return BadRequest(new AuthenticationResponse
                {
                    ErrorMessages = authenticationResponse.ErrorMessages,
                    Success = false
                });
            }

            return Ok(new AuthenticationResponse{
                Success = true,
                Token = authenticationResponse.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest requestBody)
        {
            var authenticationResponse = await this.identityService.LoginUserAsync(requestBody.Email, requestBody.Password);
            if (authenticationResponse.Success == false)
            {
                return BadRequest(new AuthenticationResponse
                {
                    ErrorMessages = authenticationResponse.ErrorMessages,
                    Success = false
                });
            }

            return Ok(new AuthenticationResponse{
                Success = true,
                Token = authenticationResponse.Token
            });
        }

    }
}