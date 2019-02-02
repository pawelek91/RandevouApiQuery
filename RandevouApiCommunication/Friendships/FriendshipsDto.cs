using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Friendships
{
    public class FriendshipSendRequestDto
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }

    public class UpdateFriendshipStatusDto : FriendshipSendRequestDto
    {
        public string Action { get; set; }
    }
}
