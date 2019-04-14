using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public interface IUsersQuery
    {
        IEnumerable<UsersDto> GetUsersLists(ApiAuthDto authDto);
        UserDetailsDto GetUserDetails(int id, ApiAuthDto authDto);
        UsersDto GetUser(int id, ApiAuthDto authDto);
        //int CreateUser(UsersDto dto);
        void DeleteUser(int id, ApiAuthDto authDto);
        void UpdateUser(UsersDto dto, ApiAuthDto authDto);
        void UpdateUserDetails(int userId, UserDetailsDto dto, ApiAuthDto authDto);

        int CreateUserWithLogin(UserComplexDto dto);
    }
}
