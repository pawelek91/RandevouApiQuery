using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RandevouApiCommunication.Friendships;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    
    public class UserFriendshipTest : CommonTest
    {

        private const string Accept = nameof(Accept);
        private const string Delete = nameof(Delete);

        IUserFriendshipQuery friendshipsQueryProvider;

        public UserFriendshipTest():base()
        {
            friendshipsQueryProvider = GetQueryProvider<IUserFriendshipQuery>();
        }
        [Fact]
        public void TestFriendshipsRequest() 
        {
            var users = GenerateUsers(4);

            Assert.True(friendshipsQueryProvider.GetFriends(users[0], authDto).Length==0);
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[0], ToUserId = users[1] }, authDto);

            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1], authDto).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[0], authDto).Length == 0);

            Assert.ThrowsAny<Exception>(() => //user0 wyslal do user1, wiec to user1 musi zaakceptowac
            {
                friendshipsQueryProvider.SetFriendshipStatusAction(
                    new UpdateFriendshipStatusDto()
                    {
                        FromUserId = users[0],
                        ToUserId = users[1],
                        Action = Accept
                    }, authDto);
            });

            friendshipsQueryProvider.SetFriendshipStatusAction(
                  new UpdateFriendshipStatusDto()
                  {
                      FromUserId = users[1],
                      ToUserId = users[0],
                      Action = Accept
                  }, authDto);

            Assert.True(friendshipsQueryProvider.GetFriends(users[0], authDto).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[1], authDto).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1], authDto).Length == 0);

            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[2], ToUserId = users[1] }, authDto);
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users[3], ToUserId = users[1] }, authDto);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1], authDto).Length == 2);

            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users[1],
                     ToUserId = users[2],
                     Action = Accept
                 }, authDto);


            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users[1],
                     ToUserId = users[3],
                     Action = Delete
                 }, authDto);

            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[1], authDto).Length == 0);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[2], authDto).Length == 0);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users[3], authDto).Length == 0);

            Assert.True(friendshipsQueryProvider.GetFriends(users[1], authDto).Length == 2);
            Assert.True(friendshipsQueryProvider.GetFriends(users[2], authDto).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriends(users[3], authDto).Length == 0);


            friendshipsQueryProvider.SetFriendshipStatusAction(
           new UpdateFriendshipStatusDto()
           {
               FromUserId = users[2],
               ToUserId = users[1],
               Action = Delete
           }, authDto);
            Assert.True(friendshipsQueryProvider.GetFriends(users[2], authDto).Length == 0);

            Assert.True(friendshipsQueryProvider.GetFriends(users[1], authDto).Length == 1);
        }

      
    }
}
