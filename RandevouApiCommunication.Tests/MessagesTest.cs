using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouApiCommunication.Exceptions;
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
            var users = GenerateUsers(3);

            authDto.UserName = users.ElementAt(0).Value;
            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users.ElementAt(0).Key,
                ReceiverId = users.ElementAt(1).Key,
                Content = "hi",
            },authDto);

            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users.ElementAt(0).Key,
                ReceiverId = users.ElementAt(2).Key,
                Content = "hi",
            }, authDto);
            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users.ElementAt(0).Key,
                ReceiverId = users.ElementAt(2).Key,
                Content = "sup",
            }, authDto);

            var uniqueReceivers = messagesQueryProvider.GetLastMessages(users.ElementAt(0).Key, authDto);

            authDto.UserName = users.ElementAt(2).Value;
            messagesQueryProvider.CreateMessage(new MessageDto
            {
                SenderId = users.ElementAt(2).Key,
                ReceiverId = users.ElementAt(0).Key,
                Content = "hi buddy",
            }, authDto);

            Assert.True(uniqueReceivers.Count() == 2);

            authDto.UserName = users.ElementAt(0).Value;
            var getConversationDto = new RequestMessagesDto
            {
                FirstUserId = users.ElementAt(0).Key,
                SecondUserId = users.ElementAt(2).Key,
            };

            var conversation = messagesQueryProvider.GetConversation(getConversationDto, authDto).ToArray();

            authDto.UserName = users.ElementAt(0).Value;
            Assert.True(conversation.Count(x => x.SenderId == users.ElementAt(0).Key) == 2);

            authDto.UserName = users.ElementAt(2).Value;
            Assert.True(conversation.Count(x => x.SenderId == users.ElementAt(2).Key) == 1);


            authDto.UserName = users.ElementAt(0).Value;
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

            Assert.True(conversation.All(x => !x.IsRead));

            messagesQueryProvider.MarkAsRead(firstMessageMarkReadDto, authDto);

            authDto.UserName = conversation[0].ReceiverName;
            var wrongDto = new MessageMarkDto
            {
                MessageId = conversation[0].MessageId.ToString(),
                OwnerId = conversation[0].SenderId.ToString()
            };


            Assert.Throws<ResourceAccessDenied>(() =>
            {
                messagesQueryProvider.MarkAsRead(wrongDto, authDto);
            });

            authDto.UserName = conversation[1].ReceiverName;
            messagesQueryProvider.MarkAsRead(secondMessageMarkReadDto, authDto);


            authDto.UserName = users.ElementAt(0).Value;
            conversation = messagesQueryProvider.GetConversation(getConversationDto, authDto).ToArray();
            Assert.True(conversation.Count(x => x.IsRead) == 2);

            messagesQueryProvider.MarkAsUnread(secondMessageMarkReadDto, authDto);

            authDto.UserName = users.ElementAt(0).Value;
            conversation = messagesQueryProvider.GetConversation(getConversationDto, authDto).ToArray();
            Assert.True(conversation.Count(x => x.IsRead) == 1);
        }
    }
}
