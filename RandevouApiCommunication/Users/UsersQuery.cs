using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RandevouApiCommunication.Users
{
    internal class UsersQuery : ApiQuery, IUsersQuery
    {
        public IEnumerable<UsersDto> GetUsersLists()
            => Query<IEnumerable<UsersDto>>(Endpoints.GetAllUsers).Result;

        public UserDetailsDto GetUserWithDetails(int id)
            => Query<UserDetailsDto>(Endpoints.GetUser, id.ToString()).Result;

        public UsersDto GetUser(int id)
            => Query<UsersDto>(Endpoints.GetUser, id.ToString()).Result;

        public int CreateUser(UsersDto dto)
            => Create(Endpoints.PostUser, dto);

        public void DeleteUser(int id)
            => Delete(Endpoints.DeleteUser, id);

        public void UpdateUser(UsersDto dto)
            =>  Update(Endpoints.PatchUser, dto);

        public UserDetailsDto GetUserDetails(int userId)
            => Query<UserDetailsDto>(Endpoints.GetUserDetails, userId.ToString()).Result;

        public void UpdateUserDetails(int userId, UserDetailsDto dto)
            => Update(Endpoints.PutUserDetails, dto, userId.ToString());
    }
}
