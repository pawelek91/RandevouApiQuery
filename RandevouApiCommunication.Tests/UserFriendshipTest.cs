using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RandevouApiCommunication.Friendships;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    
    public class UserFriendshipTest
    {

        private const string Accept = nameof(Accept);
        private const string Delete = nameof(Delete);

        ApiCommunicationProvider communicationProvider;
        IUsersQuery usersQueryProvider;
        IUserFriendshipQuery friendshipsQueryProvider;

        [Fact]
        public void TestFriendshipsRequest()
        {
            communicationProvider = ApiCommunicationProvider.GetInstance();

            usersQueryProvider = communicationProvider.GetQueryProvider<IUsersQuery>();
            friendshipsQueryProvider = communicationProvider.GetQueryProvider<IUserFriendshipQuery>();

            var users = CreateUsers();

            Assert.True(friendshipsQueryProvider.GetFriends(users[0]).Length==0);
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[0], ToUserId = users[1] });

            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1]).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[0]).Length == 0);

            Assert.ThrowsAny<Exception>(() => //user0 wyslal do user1, wiec to user1 musi zaakceptowac
            {
                friendshipsQueryProvider.SetFriendshipStatusAction(
                    new UpdateFriendshipStatusDto()
                    {
                        FromUserId = users[0],
                        ToUserId = users[1],
                        Action = Accept
                    });
            });

            friendshipsQueryProvider.SetFriendshipStatusAction(
                  new UpdateFriendshipStatusDto()
                  {
                      FromUserId = users[1],
                      ToUserId = users[0],
                      Action = Accept
                  });

            Assert.True(friendshipsQueryProvider.GetFriends(users[0]).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[1]).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1]).Length == 0);

            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[2], ToUserId = users[1] });
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[3], ToUserId = users[1] });
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1]).Length == 2);

            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users[1],
                     ToUserId = users[2],
                     Action = Accept
                 });


            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users[1],
                     ToUserId = users[3],
                     Action = Delete
                 });

            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1]).Length == 0);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[2]).Length == 0);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[3]).Length == 0);

            Assert.True(friendshipsQueryProvider.GetFriends(users[1]).Length == 2);
            Assert.True(friendshipsQueryProvider.GetFriends(users[2]).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[3]).Length == 0);


            friendshipsQueryProvider.SetFriendshipStatusAction(
           new UpdateFriendshipStatusDto()
           {
               FromUserId = users[2],
               ToUserId = users[1],
               Action = Delete
           });
            Assert.True(friendshipsQueryProvider.GetFriends(users[2]).Length == 0);

            Assert.True(friendshipsQueryProvider.GetFriends(users[1]).Length == 1);
        }

        private string CreateUserName()
            => "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10);

        private int[] CreateUsers()
        {
            var query = usersQueryProvider;

            var name1 = CreateUserName();
            var dto = new UsersDto()
            {
                BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                DisplayName = name1,
                Gender = 'F',
                Name = name1,
            };
       
            var userId = query.CreateUser(dto);

            var name2 = CreateUserName();
            dto.Name = name2;
            var userId2 = query.CreateUser(dto);



            var name3 = CreateUserName();
            dto.Name = name3;
            var userId3 = query.CreateUser(dto);

           
            var name4 = CreateUserName();
            dto.Name = name4;
            var userId4 = query.CreateUser(dto);

            return new int[] { userId, userId2, userId3, userId4 };
        }
    }
}
