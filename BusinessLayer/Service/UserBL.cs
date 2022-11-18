using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
            {
            this.iuserRL = iuserRL;
        }
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                return iuserRL.Registration(User);
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
                return iuserRL.Login(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string Email, string Password, string ConfirmPassword)
        {
            try
            {
                return iuserRL.ResetPassword(Email, Password, ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
