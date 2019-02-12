using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouApiCommunication.Messages;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    public class MessagesTest : CommonTest
    {
        IMessagesQuery messagesQueryProvider;

        public MessagesTest():base()
        {
            messagesQueryProvider = GetQueryProvider<IMessagesQuery>();
        }

        [Fact]
        public void TestMessagesQueries()
        {
            int[] users = GenerateUsers(3);
            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users[0],
                ReceiverId = users[1],
                Content = "hi",
            });

            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users[0],
                ReceiverId = users[2],
                Content = "hi",
            });
            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users[0],
                ReceiverId = users[2],
                Content = "sup",
            });

            var uniqueReceivers = messagesQueryProvider.GetLastMessages(users[0]);

            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users[2],
                ReceiverId = users[0],
                Content = "hi buddy",
            });

            Assert.True(uniqueReceivers.Count() == 2);

            var getConversationDto = new RequestMessagesDto
            {
                FirstUserId = users[0],
                SecondUserId = users[2],
            };

            var conversation = messagesQueryProvider.GetConversation(getConversationDto).ToArray();

            Assert.True(conversation.Count(x => x.SenderId == users[0]) == 2);
            Assert.True(conversation.Count(x => x.SenderId == users[2]) == 1);


            var firstMessageMarkReadDto = new MessageMarkDto
            {
                MessageId = conversation[0].MessageId.ToString(),
                OwnerId = conversation[0].ReceiverId.ToString()
            };

            var secondMessageMarkReadDto = new MessageMarkDto
            {
                MessageId = conversation[1].MessageId.ToString(),
                OwnerId = conversation[1].ReceiverId.ToString()
            };

            Assert.True(!conversation.Any(x => x.IsRead));

            messagesQueryProvider.MarkAsRead(firstMessageMarkReadDto);

            var wrongDto = new MessageMarkDto
            {
                MessageId = conversation[0].MessageId.ToString(),
                OwnerId = conversation[0].SenderId.ToString()
            };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                messagesQueryProvider.MarkAsRead(wrongDto);
            });

            messagesQueryProvider.MarkAsRead(secondMessageMarkReadDto);

            conversation = messagesQueryProvider.GetConversation(getConversationDto).ToArray();
            Assert.True(conversation.Count(x => x.IsRead) == 2);

            messagesQueryProvider.MarkAsUnread(secondMessageMarkReadDto);
            conversation = messagesQueryProvider.GetConversation(getConversationDto).ToArray();
            Assert.True(conversation.Count(x => x.IsRead) == 1);
        }
    }
}
