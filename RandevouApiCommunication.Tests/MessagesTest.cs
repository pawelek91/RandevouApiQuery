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

            var conversation = messagesQueryProvider.GetConversation(getConversationDto);

            Assert.True(conversation.Count(x => x.SenderId == users[0]) == 2);
            Assert.True(conversation.Count(x => x.SenderId == users[2]) == 1);


        }
    }
}
