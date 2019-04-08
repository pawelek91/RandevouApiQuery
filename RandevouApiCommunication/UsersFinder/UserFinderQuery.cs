using RandevouApiCommunication.Authentication;
using System;
namespace RandevouApiCommunication.UsersFinder
{
	internal class UserFinderQuery : ApiQuery, IUserFinderQuery
    {
        public int[] FindUsers(UsersFinderDto dto, ApiAuthDto authDto = null)
        {
            if(authDto != null)
            {
                auth = GetAuthentitaceUserKey(authDto.UserName, authDto.Password);
            }

            var result = PostSpecific<int[], UsersFinderDto>(Endpoints.PostUserFind, dto, auth);
            return result;
        }
    }
}
