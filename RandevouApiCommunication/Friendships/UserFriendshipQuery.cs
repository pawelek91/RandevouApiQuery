using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    internal class UserFriendshipQuery : ApiQuery, IUserFriendshipQuery
    {
        public int[] GetFriends(int userId)
            => Query<int[]>(Endpoints.GetFriendsList, userId.ToString()).Result;

        public int[] GetFriendshipRequests(int userId)
            => Query<int[]>(Endpoints.GetFriendshipisRequests, userId.ToString()).Result;

        public string[] GetPossibleRequestActions()
            => Query<string[]>(Endpoints.GetPossibleAction).Result;

        public void PostFriendshipInvitation(FriendshipSendRequestDto dto)
            => Set(Endpoints.SendInvitation, dto);

        public void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto)
            => Set(Endpoints.SetFriendshipStatus, dto);
    }
}
