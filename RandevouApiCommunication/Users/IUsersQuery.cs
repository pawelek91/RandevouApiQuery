using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public interface IUsersQuery
    {
        IEnumerable<UsersDto> GetManyUsers(ApiAuthDto authDto, int[] ids);
        IEnumerable<UsersDto> GetManyUsers(string apiKey, int[] ids);
        IEnumerable<UsersDto> GetUsersLists(ApiAuthDto authDto);
        IEnumerable<UsersDto> GetUsersLists(string apiKey);
        UserDetailsDto GetUserDetails(int id, ApiAuthDto authDto);
        UserDetailsDto GetUserDetails(int id, string apiKey);
        UsersDto GetUser(int id, ApiAuthDto authDto);
        UsersDto GetUser(int id, string apiKey);
        void DeleteUser(int id, ApiAuthDto authDto);
        void DeleteUser(int id, string apiKey);
        void UpdateUser(UsersDto dto, ApiAuthDto authDto);
        void UpdateUser(UsersDto dto, string apiKey);
        void UpdateUserDetails(int userId, UserDetailsDto dto, ApiAuthDto authDto);
        void UpdateUserDetails(int userId, UserDetailsDto dto, string apiKey);

        int CreateUserWithLogin(UserComplexDto dto);
    }
}
