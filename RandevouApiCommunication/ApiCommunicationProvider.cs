using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication
{
    public class ApiCommunicationProvider
    {
        private static ApiCommunicationProvider _instance;
        
        private ApiCommunicationProvider()
        {
            ApiQueriesIOCcontainer.Register<Users.IUsersQuery, Users.UsersQuery>();
            ApiQueriesIOCcontainer.Register<Friendships.IUserFriendshipQuery, Friendships.UserFriendshipQuery>();
            ApiQueriesIOCcontainer.Register<Messages.IMessagesQuery, Messages.MessagesQuery>();
            ApiQueriesIOCcontainer.Register<UsersFinder.IUserFinderQuery, UsersFinder.UserFinderQuery>();
        }

        public static ApiCommunicationProvider GetInstance()
        {
            if (_instance == null)
                _instance = new ApiCommunicationProvider();

            return _instance;
        }

        public TConract GetQueryProvider<TConract>()
            => ApiQueriesIOCcontainer.Resolve<TConract>();


    }
}
