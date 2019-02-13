using System;
namespace RandevouApiCommunication.UsersFinder
{
    public interface IUserFinderQuery
    {
        int[] FindUsers(UsersFinderDto dto);
    }
}
