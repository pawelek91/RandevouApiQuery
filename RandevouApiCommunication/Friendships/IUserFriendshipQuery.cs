using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    public interface IUserFriendshipQuery
    {
        int[] GetFriends(int userId,ApiAuthDto authDto);
        int[] GetFriends(int userId, string apiKey);
        int[] GetFriendshipRequests(int userId, ApiAuthDto authDto);
        int[] GetFriendshipRequests(int userId, string apiKey);
        string[] GetPossibleRequestActions();
        void PostFriendshipInvitation(FriendshipSendRequestDto dto, ApiAuthDto authDto);
        void PostFriendshipInvitation(FriendshipSendRequestDto dto, string apiKey);
        void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto, ApiAuthDto authDto);
        void SetFriendshipStatusAction(UpdateFriendshipStatusDto dto, string apiKey);
    }
}
