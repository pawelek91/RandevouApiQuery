using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    public interface IUserFriendshipQuery
    {
        int[] GetFriends(int userId);
        int[] GetFriendshipRequests(int userId);
        string[] GetPossibleRequestActions();
        void PostFriendshipInvitation(FriendshipSendRequestDto dto);
        void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto);
    }
}
