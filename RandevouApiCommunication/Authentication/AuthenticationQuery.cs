using System;

namespace RandevouApiCommunication.Authentication
{
    internal class AuthenticationQuery : ApiQuery,IAuthenticationQuery
    {
        public int GetIdentity(string apiKey)
        {
            var result = (Query<string>(Endpoints.Identity, null, CreateAuth(apiKey)).Result);
            if (!int.TryParse(result, out var id))
                throw new ArgumentException("Id not found");
            return id;
        }
            

        public string GetLoginAuthKey(string username, string password)
            => PostSpecific<string, ApiAuthDto>(Endpoints.Login,
                new ApiAuthDto
                {
                    UserName= username,
                    Password = password,
                });

        public void RegisterUser(int userId, string password)
        {
            Post<RegisterDto>(Endpoints.Register,
                new RegisterDto
                {
                    UserId = userId,
                    Password = password
                });
        }
    }
}