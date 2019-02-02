using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    public static class Endpoints
    {
        public const string FriendshipQueryEndpoint = "api/UserFriendship";
        public const string GetFriendsList = FriendshipQueryEndpoint + "/users/{id}/friends";
        public const string GetFriendshipisRequests = FriendshipQueryEndpoint + "/users/{id}/requests";
        public const string GetPossibleAction = FriendshipQueryEndpoint + "/PossibleRequestsActions";
        public const string SendInvitation = FriendshipQueryEndpoint + "/Invitation";
        public const string SetFriendshipStatus = FriendshipQueryEndpoint + "/FriendshipStatusAction";
    }
}

