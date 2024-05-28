using AESAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace AESAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly AESDBContext _context;
        public LoginAPIController(AESDBContext context)
        {
            _context = context;
        }

        [HttpPost("ValidateCredentials")]
        public IActionResult ValidateCredentials(string UMobno, string UPwd)
        {
            var user = _context.TblMUsers.FirstOrDefault(u => u.UMobno == UMobno && u.UPwd == UPwd);

            ValidateCredentialsResponse response = new ValidateCredentialsResponse();

            if (user != null)
            {
                response.IsSuccess = true;
                response.Message = "Credentials are correct";
                return Ok(response);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User Mobile number or password!";
                return Ok(response);
            }
        }



        [HttpGet("GetUserNameByUserPhnno")]
        public IActionResult GetUserNameByUserPhnno(string UMobno)
        {
            try
            {
                var user_nm = _context.TblMUsers.Where(u => u.UMobno == UMobno && u.DFlag != 1).Select(u => u.UName).FirstOrDefault();
                return Ok(user_nm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
