using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    public static class Endpoints
    {
        public const string Messages = "/api/Messages";
        public const string Conversations = Messages + "/Conversation";
        public const string Speakers = Messages + "/{userId}/Speakers";
        public const string WholeConversation = Conversations + "/{userid}";
    }
}
