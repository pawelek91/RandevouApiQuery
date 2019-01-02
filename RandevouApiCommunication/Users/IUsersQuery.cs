using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public interface IUsersQuery
    {
        IEnumerable<UserDto> GetUsersLists();
        UserDetailsDto GetUserWithDetails(int id);
        UserDto GetUser(int id);
    }
}
