using RandevouApiCommunication.Authentication;
using System;
namespace RandevouApiCommunication.UsersFinder
{
	internal class UserFinderQuery : ApiQuery, IUserFinderQuery
    {
        public int[] FindUsers(UsersFinderDto dto, ApiAuthDto authDto)
            => PostSpecific<int[], UsersFinderDto>
            (Endpoints.PostUserFind, dto, GetAuthentitaceUserKey(authDto));
    }
}
