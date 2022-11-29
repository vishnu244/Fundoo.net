using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        public static string Key = "abcd@efgh$ijk@";
        FundooContext fundooContext;

        private readonly IConfiguration config;

        public UserRL(FundooContext fundooContext, IConfiguration config)

        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        //Method for Registration

        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = User.FirstName;
                userEntity.LastName = User.LastName;
                userEntity.Email = User.Email;
                userEntity.Password = EncryptPass(User.Password);

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if( result > 0 )
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public string Login(LoginModel loginModel)
        {
            try
            {

                var logindata = fundooContext.UserTable.FirstOrDefault( x => x.Email == loginModel.Email && x.Password == DecryptPass(loginModel.Password));
                if (logindata != null)
                {
                    /*loginModel.Email = logindata.Email;
                    loginModel.Password = logindata.Password;*/

                    var token = GenerateSecurityToken(logindata.Email, logindata.UserId);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(string email, long UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config[("JWT:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserID", UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


        public string ForgetPassword(string Email)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if (emailCheck != null)
                {
                    var token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MsmqModel msmqModel = new MsmqModel();
                    msmqModel.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception )
            {
                throw;
            }
        }


        public bool ResetPassword(string Email, string Password, string ConfirmPassword)
        {
            try
            {
                if (Password.Equals(ConfirmPassword))
                {
                    UserEntity user = fundooContext.UserTable.Where(e => e.Email == Email).FirstOrDefault();
                    user.Password = ConfirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string EncryptPass(string password)
        {
            try
            {
                if(string.IsNullOrEmpty(password))
                {
                    return "";
                }
                password += Key;
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DecryptPass(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
                }
                password += Key;
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
