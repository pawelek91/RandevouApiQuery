using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public interface IUsersQuery
    {
        IEnumerable<UsersDto> GetUsersLists();
        UserDetailsDto GetUserDetails(int id);
        UsersDto GetUser(int id);
        int CreateUser(UsersDto dto);
        void DeleteUser(int id);
        void UpdateUser(UsersDto dto);
        void UpdateUserDetails(int userId, UserDetailsDto dto);

    }
}
