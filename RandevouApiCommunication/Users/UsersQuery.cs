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
    public class UsersQuery : ApiQuery, IUsersQuery
    {
        public IEnumerable<UserDto> GetUsersLists()
            => Query<IEnumerable<UserDto>>(Endpoints.GetAllUsers).Result;

        public UserDetailsDto GetUserWithDetails(int id)
            => Query<UserDetailsDto>(Endpoints.GetUser, id.ToString()).Result;

        public UserDto GetUser(int id)
            => Query<UserDto>(Endpoints.GetUser, id.ToString()).Result;

        public int CreateUser(UserDto dto)
            => Create(Endpoints.PostUser, dto);

        public void DeleteUser(int id)
            => Delete(Endpoints.DeleteUser, id);

        public void UpdateUser(UserDto dto)
            =>  Update(Endpoints.PatchUser, dto);

        public UserDetailsDto GetUserDetails(int userId)
            => Query<UserDetailsDto>(Endpoints.GetUserDetails, userId.ToString()).Result;

        public void UpdateUserDetails(int userId, UserDetailsDto dto)
            => Update(Endpoints.PutUserDetails, dto, userId.ToString());
    }
}
