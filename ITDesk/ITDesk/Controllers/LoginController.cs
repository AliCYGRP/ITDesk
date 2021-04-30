using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using ITDesk.Models;
using ITDesk.Models.Request;
using ITDesk.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ITDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ITDeskContext _context;
        private IConfiguration _config;

        public LoginController(IConfiguration config, ITDeskContext context)
        {
            _config = config;
            _context = context;
        }

        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        public LoginResponse employeeLogin([FromBody] LoginRequest loginInfo)
        {
            var employeeInfo = AuthenticateLogin(loginInfo);

            if (employeeInfo != null)
            {
                var tokenString = GenerateJSONWebToken(employeeInfo);

                var queryRole = _context.EmployeeInfo
                            .Where(v => v.EmployeeEmail == loginInfo.EmployeeEmail)
                            .Select(v => v.Role).ToList();
                bool role = queryRole[0];

                var queryId = _context.EmployeeInfo
                            .Where(v => v.EmployeeEmail == loginInfo.EmployeeEmail)
                            .Select(v => v.EmployeeId).ToList();
                int id = queryId[0];

                return new LoginResponse(id,tokenString, role);
            }
            return new LoginResponse(0,"", false);
        }

        // PUT: api/Login/resetPassword/5
        [Authorize]
        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult resetPassword(int id, [FromBody] string password)
        {
            EmployeeInfo employeeInfo = _context.EmployeeInfo.FirstOrDefault(x => x.EmployeeId == id);
            employeeInfo.Password = password;
            _context.EmployeeInfo.Update(employeeInfo);
            _context.SaveChanges();
            return Ok(employeeInfo);
        }

        private string GenerateJSONWebToken(EmployeeInfo employeeInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private EmployeeInfo AuthenticateLogin(LoginRequest loginInfo)
        {
            EmployeeInfo employeeInfo = _context.EmployeeInfo.FirstOrDefault(x => x.EmployeeEmail == loginInfo.EmployeeEmail && x.Password == loginInfo.Password);
            return employeeInfo;
        }
    }
}
