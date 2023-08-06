using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarShopAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }
        public async Task<string> AddRoleAysnc(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if(await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            
            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<AuthModel> RegisterAysnc(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByEmailAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Img_url = "Not Set Yet"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                Username = user.UserName,
				IsAuthenticated = true,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				ExpiresOn = jwtSecurityToken.ValidTo,
				Roles = new List<string> { "User" },
			};
        }
        public async Task<AuthModel> GetTokenAsync(TokenrRequestModel model)
        {
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
				return new AuthModel { Message = "Email or Password is incorrect" };

			var jwtSecurityToken = await CreateJwtToken(user);
			var rolesList = await _userManager.GetRolesAsync(user);

			return new AuthModel
			{
				Email = user.Email,
				Username = user.UserName,
				EmailConfirmed = user.EmailConfirmed,
				IsAuthenticated = true,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				ExpiresOn = jwtSecurityToken.ValidTo,
				Roles = rolesList.ToList()
			};
		}
        public async Task<AuthModel> VerifyAsync(TokenModel tokenModel)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenModel.Token) as JwtSecurityToken;

            var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return new AuthModel { Message = "User not found." };
			
            var rolesList = await _userManager.GetRolesAsync(user);

			if (user.EmailConfirmed)
				return new AuthModel { Message = "Email is already confirmed." };

			user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
				return new AuthModel { Message = "Failed to update user data." };

            return new AuthModel
            {
            };
        }
        public async Task<TokenModel> FoegetPasswordAsync(string email)
        {
            TokenModel tokenModel = new TokenModel();
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
				return new TokenModel { Message = "User is not found" };

            if (!await _userManager.IsEmailConfirmedAsync(user))
				return new TokenModel { Message = "Email is not confirmed" };

            var jwtSecurityToken = await CreateJwtToken(user);
            return new TokenModel { Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) };
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            bool emailExists =  user is not null;
            return emailExists;
        }
    }
}
