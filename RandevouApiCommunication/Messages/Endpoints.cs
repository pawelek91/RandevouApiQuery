using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    public static class Endpoints
    {
        public const string Messages = "/api/Messages";
        public const string Conversations = Messages + "/Conversation/{id}";
        public const string Speakers = Messages + "/{id}/Speakers";
        public const string WholeConversation = "/api/Messages/Conversation";
    }
}
