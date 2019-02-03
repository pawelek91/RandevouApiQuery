using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    internal class MessagesQuery : ApiQuery, IMessagesQuery
    {
        public int CreateMessage(MessageDto dto)
        => Post<MessageDto>(Endpoints.Messages, dto);

        public IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto)
        => PostSpecific<IEnumerable<MessageDto>, RequestMessagesDto>(Endpoints.WholeConversation, dto);

        public IEnumerable<LastMessagesDto> GetLastMessages(int userId)
        => Query<IEnumerable<LastMessagesDto>>(Endpoints.Conversations, userId.ToString()).Result;

        public int[] GetSpeakers(int userId)
        => Query<int[]>(Endpoints.Speakers, userId.ToString()).Result;
    }
}
