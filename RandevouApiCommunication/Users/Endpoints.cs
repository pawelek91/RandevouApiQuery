using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public static class Endpoints
    {
        public const string Users = "/api/Users";
        public const string GetAllUsers = Users;
        public const string GetUser = Users +"/{id}";
        public const string PatchUser = Users;
        public const string DeleteUser = Users + "/{id}";
        public const string PostUser = Users;

        public const string GetUserDetails = GetUser + "/Details";
        public const string PutUserDetails = PatchUser + "/{id}/Details";
    }
}
