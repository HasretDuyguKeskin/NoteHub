using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteHub.API.Data;
using NoteHub.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError("", "Invalid username or password!");
                    return BadRequest(ModelState);

                }

                var autClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (string role in await _userManager.GetRolesAsync(user))
                {
                    autClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.AUTH_SİGNİNG_KEY));

                var token = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: autClaims,
                    expires: DateTime.Now.AddYears(1),
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            return BadRequest(ModelState);
        }
    }
}
