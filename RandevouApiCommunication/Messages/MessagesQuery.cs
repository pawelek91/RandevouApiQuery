using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    internal class MessagesQuery : ApiQuery, IMessagesQuery
    {
        public int CreateMessage(MessageDto dto, ApiAuthDto authDto)
        => Post<MessageDto>(Endpoints.Messages, dto, GetAuthentitaceUserKey(authDto));

        public IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto, ApiAuthDto authDto)
        => PostSpecific<IEnumerable<MessageDto>, RequestMessagesDto>
            (Endpoints.WholeConversation, dto, GetAuthentitaceUserKey(authDto));

        public IEnumerable<LastMessagesDto> GetLastMessages(int userId, ApiAuthDto authDto)
        => Query<IEnumerable<LastMessagesDto>>
            (Endpoints.Conversations, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public int[] GetSpeakers(int userId, ApiAuthDto authDto)
        => Query<int[]>(Endpoints.Speakers, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public void MarkAsRead(MessageMarkDto dto, ApiAuthDto authDto)
        => Set<MessageMarkDto>(Endpoints.MessageMarkRead, dto, string.Empty, GetAuthentitaceUserKey(authDto));

        public void MarkAsUnread(MessageMarkDto dto, ApiAuthDto authDto)
        => Set<MessageMarkDto>(Endpoints.MessageMarkUnread, dto, string.Empty, GetAuthentitaceUserKey(authDto));
    }
}
