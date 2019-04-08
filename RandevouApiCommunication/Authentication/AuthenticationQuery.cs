﻿namespace RandevouApiCommunication.Authentication
{
    internal class AuthenticationQuery : ApiQuery,IAuthenticationQuery
    {
        public string GetLoginAuthKey(string username, string password)
            => PostSpecific<string, ApiAuthDto>(Endpoints.Login,
                new ApiAuthDto
                {
                    UserName= username,
                    Password = password,
                });

        public void RegisterUser(int userId, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}