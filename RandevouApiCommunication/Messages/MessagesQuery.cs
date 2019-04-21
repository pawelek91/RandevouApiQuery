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

        public int CreateMessage(MessageDto dto, string apiKey)
        => Post<MessageDto>(Endpoints.Messages, dto, CreateAuth(apiKey));


        public IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto, ApiAuthDto authDto)
        => PostSpecific<IEnumerable<MessageDto>, RequestMessagesDto>
            (Endpoints.WholeConversation, dto, GetAuthentitaceUserKey(authDto));

        public IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto, string apiKey)
        => PostSpecific<IEnumerable<MessageDto>, RequestMessagesDto>
            (Endpoints.WholeConversation, dto, CreateAuth(apiKey));


        public IEnumerable<LastMessagesDto> GetLastMessages(int userId, ApiAuthDto authDto)
            => Query<IEnumerable<LastMessagesDto>>
                (Endpoints.Conversations, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public IEnumerable<LastMessagesDto> GetLastMessages(int userId, string apiKey)
             => Query<IEnumerable<LastMessagesDto>>
                (Endpoints.Conversations, userId.ToString(), CreateAuth(apiKey)).Result;

        public int[] GetSpeakers(int userId, ApiAuthDto authDto)
        => Query<int[]>(Endpoints.Speakers, userId.ToString(), GetAuthentitaceUserKey(authDto)).Result;

        public int[] GetSpeakers(int userId, string apiKey)
        => Query<int[]>(Endpoints.Speakers, userId.ToString(), CreateAuth(apiKey)).Result;


        public void MarkAsRead(MessageMarkDto dto, ApiAuthDto authDto)
            => Set<MessageMarkDto>(Endpoints.MessageMarkRead, dto, string.Empty, GetAuthentitaceUserKey(authDto));

        public void MarkAsRead(MessageMarkDto dto, string apiKey)
            => Set<MessageMarkDto>(Endpoints.MessageMarkRead, dto, string.Empty, CreateAuth(apiKey));

        public void MarkAsUnread(MessageMarkDto dto, ApiAuthDto authDto)
            => Set<MessageMarkDto>(Endpoints.MessageMarkUnread, dto, string.Empty, GetAuthentitaceUserKey(authDto));

        public void MarkAsUnread(MessageMarkDto dto, string apiKey)
        => Set<MessageMarkDto>(Endpoints.MessageMarkUnread, dto, string.Empty, CreateAuth(apiKey));
    }
}
