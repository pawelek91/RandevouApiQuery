using RandevouApiCommunication.Authentication;
using System;
namespace RandevouApiCommunication.UsersFinder
{
    public interface IUserFinderQuery
    {
        int[] FindUsers(SearchQueryDto dto, ApiAuthDto authDto);
        int[] FindUsers(SearchQueryDto dto, string apiKey);
    }
}
