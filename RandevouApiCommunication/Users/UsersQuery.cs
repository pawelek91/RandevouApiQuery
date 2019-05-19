using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RandevouApiCommunication.Authentication;

namespace RandevouApiCommunication.Users
{
    internal class UsersQuery : ApiQuery, IUsersQuery
    {
        public IEnumerable<UsersDto> GetUsersLists(ApiAuthDto authDto)
            => Query<IEnumerable<UsersDto>>
            (Endpoints.GetAllUsers, auth: GetAuthentitaceUserKey(authDto)).Result;

        public IEnumerable<UsersDto> GetManyUsers(ApiAuthDto authDto, int[] ids)
            => PostSpecific<IEnumerable<UsersDto>, int[]>(Endpoints.GetManyUsers, ids, GetAuthentitaceUserKey(authDto));

        public IEnumerable<UsersDto> GetManyUsers(string apiKey, int[] ids)
          => PostSpecific<IEnumerable<UsersDto>, int[]>(Endpoints.GetManyUsers, ids, CreateAuth(apiKey));

        public UserDetailsDto GetUserWithDetails(int id, ApiAuthDto authDto)
            => Query<UserDetailsDto>
            (Endpoints.GetUserDetails, id.ToString(), GetAuthentitaceUserKey(authDto)).Result;
        

        public UsersDto GetUser(int id, ApiAuthDto authDto)
            => Query<UsersDto>
            (Endpoints.GetUser, id.ToString(), GetAuthentitaceUserKey(authDto)).Result;
        

       

        private int CreateUser(UsersDto dto)
            => Post(Endpoints.PostUser, dto);

        public void DeleteUser(int id, ApiAuthDto authDto)
            => Delete(Endpoints.DeleteUser, id, GetAuthentitaceUserKey(authDto));

        public void UpdateUser(UsersDto dto, ApiAuthDto authDto)
            =>  Update(Endpoints.PatchUser, dto,string.Empty, GetAuthentitaceUserKey(authDto));

        public UserDetailsDto GetUserDetails(int userId, ApiAuthDto authDto)
            => Query<UserDetailsDto>
            (Endpoints.GetUserDetails, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public void UpdateUserDetails(int userId, UserDetailsDto dto, ApiAuthDto authDto)
            => Update(Endpoints.PutUserDetails, dto, userId.ToString(),GetAuthentitaceUserKey(authDto));

        public int CreateUserWithLogin(UserComplexDto dto)
        {
            var userId = CreateUser(dto.UserDto);
            var authQuery = GetQueryProvider<IAuthenticationQuery>();
            authQuery.RegisterUser(userId, dto.Password);
            return userId;
        }

        public IEnumerable<UsersDto> GetUsersLists(string apiKey)
         => Query<IEnumerable<UsersDto>>
            (Endpoints.GetAllUsers, auth: CreateAuth(apiKey)).Result;

        public UserDetailsDto GetUserDetails(int id, string apiKey)
         => Query<UserDetailsDto>
            (Endpoints.GetUserDetails, id.ToString(), CreateAuth(apiKey)).Result;

        public UsersDto GetUser(int id, string apiKey)
             => Query<UsersDto>
            (Endpoints.GetUser, id.ToString(), CreateAuth(apiKey)).Result;

        public void DeleteUser(int id, string apiKey)
        => Delete(Endpoints.DeleteUser, id, CreateAuth(apiKey));

        public void UpdateUser(UsersDto dto, string apiKey)
            => Update(Endpoints.PatchUser, dto, string.Empty, CreateAuth(apiKey));
        public void UpdateUserDetails(int userId, UserDetailsDto dto, string apiKey)
            => Update(Endpoints.PutUserDetails, dto, userId.ToString(), CreateAuth(apiKey));
    }
}
