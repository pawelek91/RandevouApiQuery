using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Friendships;
using RandevouApiCommunication.Users;

namespace RandevouApiCommunication.Tests
{
    public class UserFriendshipTest
    {
        ApiCommunicationProvider communicationProvider;
        IUsersQuery usersQueryProvider;
        IUserFriendshipQuery friendshipsQueryProvider;

        public void TestFriendshipsRequest()
        {
            communicationProvider = ApiCommunicationProvider.GetInstance();

            usersQueryProvider = communicationProvider.GetQueryProvider<IUsersQuery>();
            this.friendshipsQueryProvider = communicationProvider.GetQueryProvider<IUserFriendshipQuery>();

            var users = CreateUsers();
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
