using System;
using System.Collections.Generic;
using System.Linq;
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


            authDto.UserName = users.ElementAt(0).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(0).Key, authDto).Length==0);
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users.ElementAt(0).Key, ToUserId = users.ElementAt(1).Key }, authDto);
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(0).Key, authDto).Length == 0);

            authDto.UserName = users.ElementAt(1).Value;
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(1).Key, authDto).Length == 1);
            

            Assert.ThrowsAny<Exception>(() => //user0 wyslal do user1, wiec to user1 musi zaakceptowac
            {
                authDto.UserName = users.ElementAt(0).Value;
                friendshipsQueryProvider.SetFriendshipStatusAction(
                    new UpdateFriendshipStatusDto()
                    {
                        FromUserId = users.ElementAt(0).Key,
                        ToUserId = users.ElementAt(1).Key,
                        Action = Accept
                    }, authDto);
            });

            authDto.UserName = users.ElementAt(1).Value;
            friendshipsQueryProvider.SetFriendshipStatusAction(
                  new UpdateFriendshipStatusDto()
                  {
                      FromUserId = users.ElementAt(1).Key,
                      ToUserId = users.ElementAt(0).Key,
                      Action = Accept
                  }, authDto);

            authDto.UserName = users.ElementAt(0).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(0).Key, authDto).Length == 1);

            authDto.UserName = users.ElementAt(1).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(1).Key, authDto).Length == 1);
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(1).Key, authDto).Length == 0);

            authDto.UserName = users.ElementAt(2).Value;
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users.ElementAt(2).Key, ToUserId = users.ElementAt(1).Key }, authDto);

            authDto.UserName = users.ElementAt(3).Value;
            friendshipsQueryProvider.PostFriendshipInvitation(new FriendshipSendRequestDto() { FromUserId = users.ElementAt(3).Key, ToUserId = users.ElementAt(1).Key }, authDto);

            authDto.UserName = users.ElementAt(1).Value;
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(1).Key, authDto).Length == 2);

            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users.ElementAt(1).Key,
                     ToUserId = users.ElementAt(2).Key,
                     Action = Accept
                 }, authDto);


            friendshipsQueryProvider.SetFriendshipStatusAction(
                 new UpdateFriendshipStatusDto()
                 {
                     FromUserId = users.ElementAt(1).Key,
                     ToUserId = users.ElementAt(3).Key,
                     Action = Delete
                 }, authDto);

            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(1).Key, authDto).Length == 0);

            authDto.UserName = users.ElementAt(2).Value;
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(2).Key, authDto).Length == 0);

            authDto.UserName = users.ElementAt(3).Value;
            Assert.True(friendshipsQueryProvider.GetFriendshipRequests(users.ElementAt(3).Key, authDto).Length == 0);


            authDto.UserName = users.ElementAt(1).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(1).Key, authDto).Length == 2);

            authDto.UserName = users.ElementAt(2).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(2).Key, authDto).Length == 1);

            authDto.UserName = users.ElementAt(3).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(3).Key, authDto).Length == 0);

            authDto.UserName = users.ElementAt(2).Value;
            friendshipsQueryProvider.SetFriendshipStatusAction(
           new UpdateFriendshipStatusDto()
           {
               FromUserId = users.ElementAt(2).Key,
               ToUserId = users.ElementAt(1).Key,
               Action = Delete
           }, authDto);
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(2).Key, authDto).Length == 0);


            authDto.UserName = users.ElementAt(1).Value;
            Assert.True(friendshipsQueryProvider.GetFriends(users.ElementAt(1).Key, authDto).Length == 1);
        }

      
    }
}
