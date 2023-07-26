using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAysnc([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var result = await _authService.RegisterAysnc(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenrRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAysnc(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAsync([FromBody] TokenModel tokenModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.VerifyAsync(tokenModel);

            if (!result.EmailConfirmed)
                return BadRequest(result.Message);
            
            return Ok(result);
        }
        [HttpPost("email-exists")]
        public async Task<IActionResult> EmailExistsAsyn([FromQuery] string email)
        {
            bool EmailExists = await _authService.EmailExistsAsyn(email);

            return Ok(EmailExists);
        }
    }
}
