using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Messages
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }

    public class RequestMessagesDto
    {
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class LastMessagesDto
    {
        public int SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public bool IsRead { get; set; }
        public string MessageShortContent { get; set; }
        public DateTime MessageDate { get; set; }
    }

    public class MessageMarkDto
    {
        public string MessageId { get; set; }
        public string OwnerId { get; set; }
    }
}
