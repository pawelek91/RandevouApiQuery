using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Authentication
{
    public interface IAuthenticationQuery
    {
        string GetLoginAuthKey(string username, string password);
        void RegisterUser(int userId, string password);
    }
}
