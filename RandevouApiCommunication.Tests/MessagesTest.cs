using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Messages;
using RandevouApiCommunication.Users;

namespace RandevouApiCommunication.Tests
{
    public class MessagesTest : CommonTest
    {
        IMessagesQuery messagesQueryProvider;

        public MessagesTest():base()
        {
            messagesQueryProvider = GetQueryProvider<IMessagesQuery>();
        }

        public void TestMessagesQueries()
        {

        }
    }
}
