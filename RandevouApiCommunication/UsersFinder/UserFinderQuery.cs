using System;
namespace RandevouApiCommunication.UsersFinder
{
	internal class UserFinderQuery : ApiQuery, IUserFinderQuery
    {
        public int[] FindUsers(UsersFinderDto dto)
        {
            var result = PostSpecific<int[], UsersFinderDto>(Endpoints.PostUserFind, dto);
            return result;
        }
    }
}
