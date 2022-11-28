using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iuserBL;
        
        NLog nlog = new NLog();
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }

        [HttpPost("Register")]

        public IActionResult Registration(UserRegistration User1)
        {
            try
            {
                var result = iuserBL.Registration(User1);
                if (result != null)
                {
                    nlog.LogInfo("Registration Successfull");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Registration Successfull",
                        response = result
                    }
                        ) ;

                }
                else
                {
                    nlog.LogInfo("Registration UnSuccessfull");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Registration Unsuccessfull",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost("Login")]

        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = iuserBL.Login(loginModel);
                if (result != null)
                {
                    nlog.LogInfo("Login Successfull");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Successfull",
                        data = result
                    }
                        );
                    
                }
                else
                {
                    nlog.LogInfo("Login Unsuccessfull");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Login Unsuccessfull",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost("ForgetPassword")]

        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(email);
                if (result != null)
                {
                    nlog.LogInfo("Token sent to you MailID");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Token sent to you MailID",
                    }
                        );

                }
                else
                {
                    nlog.LogInfo("Password Request Failed");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Password Request Failed",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]

        public IActionResult ResetPassword( string Password, string ConfirmPassword)
        {
            try
            {
                var passwordReset = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = iuserBL.ResetPassword(passwordReset, Password, ConfirmPassword);
                if (result != null)
                {
                    nlog.LogInfo("Password Reset Successfull.");

                    return this.Ok(new
                    {
                        success = true,
                        message = "Password Reset Successfull.",
                    }
                        );

                }
                else
                {
                    nlog.LogInfo("Password Reset Failed, Please check the details.");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Password Reset Failed, Please check the details.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
