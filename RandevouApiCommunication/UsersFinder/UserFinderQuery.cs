using RandevouApiCommunication.Authentication;
using System;
namespace RandevouApiCommunication.UsersFinder
{
	internal class UserFinderQuery : ApiQuery, IUserFinderQuery
    {
        public int[] FindUsers(SearchQueryDto dto, ApiAuthDto authDto)
            => PostSpecific<int[], SearchQueryDto>
            (Endpoints.PostUserFind, dto, GetAuthentitaceUserKey(authDto));

        public int[] FindUsers(SearchQueryDto dto, string apiKey)
           => PostSpecific<int[], SearchQueryDto>
            (Endpoints.PostUserFind, dto, CreateAuth(apiKey));
    }
}
