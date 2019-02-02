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
            ApuQueriesIOCcontainer.Register<Users.IUsersQuery, Users.UsersQuery>();
            ApuQueriesIOCcontainer.Register<Friendships.IUserFriendshipQuery, Friendships.UserFriendshipQuery>();
        }

        public static ApiCommunicationProvider GetInstance()
        {
            if (_instance == null)
                _instance = new ApiCommunicationProvider();

            return _instance;
        }

        public TConract GetQueryProvider<TConract>()
            => ApuQueriesIOCcontainer.Resolve<TConract>();


    }
}
