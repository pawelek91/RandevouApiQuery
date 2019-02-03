using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    public interface IMessagesQuery
    {
        int[] GetSpeakers(int userId);
        int CreateMessage(MessageDto dto);
        IEnumerable<MessageDto> GetConversation(RequestMessagesDto dto);
        IEnumerable<LastMessagesDto> GetLastMessages(int userId);
    }
}
