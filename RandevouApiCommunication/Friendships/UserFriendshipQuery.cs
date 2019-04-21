using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    internal class UserFriendshipQuery : ApiQuery, IUserFriendshipQuery
    {
        public int[] GetFriends(int userId, ApiAuthDto authDto)
            => Query<int[]>(Endpoints.GetFriendsList, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public int[] GetFriends(int userId, string apiKey)
         => Query<int[]>(Endpoints.GetFriendsList, userId.ToString(), CreateAuth(apiKey)).Result;

        public int[] GetFriendshipRequests(int userId, ApiAuthDto authDto)
            => Query<int[]>(Endpoints.GetFriendshipisRequests, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public int[] GetFriendshipRequests(int userId, string apiKey)
            => Query<int[]>(Endpoints.GetFriendshipisRequests, userId.ToString(), CreateAuth(apiKey)).Result;

        public string[] GetPossibleRequestActions()
            => Query<string[]>(Endpoints.GetPossibleAction).Result;

        public void PostFriendshipInvitation(FriendshipSendRequestDto dto, ApiAuthDto authDto)
            => Set(Endpoints.SendInvitation, dto, string.Empty, GetAuthentitaceUserKey(authDto));

        public void PostFriendshipInvitation(FriendshipSendRequestDto dto, string apiKey)
            => Set(Endpoints.SendInvitation, dto, string.Empty, CreateAuth(apiKey));

        public void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto, ApiAuthDto authDto)
            => Set(Endpoints.SetFriendshipStatus, dto,string.Empty, GetAuthentitaceUserKey(authDto));

        public void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto, string apiKey)
            => Set(Endpoints.SetFriendshipStatus, dto, string.Empty, CreateAuth(apiKey));
    }
}
