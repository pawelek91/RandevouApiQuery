using RandevouApiCommunication.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    public interface IMessagesQuery
    {
        int[] GetSpeakers(int userId,ApiAuthDto authDto);
        int CreateMessage(MessageDto dto, ApiAuthDto authDto);
        IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto, ApiAuthDto authDto);
        IEnumerable<LastMessagesDto> GetLastMessages(int userId, ApiAuthDto authDto);
        void MarkAsRead(MessageMarkDto dto, ApiAuthDto authDto);
        void MarkAsUnread(MessageMarkDto dto, ApiAuthDto authDto);
    }
}
