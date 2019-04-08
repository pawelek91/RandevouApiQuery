using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Authentication;
using RandevouApiCommunication.Users;

namespace RandevouApiCommunication.Tests
{
    public abstract class CommonTest
    {
        private ApiCommunicationProvider communicationProvider;
        protected IUsersQuery usersQueryProvider;
        protected ApiAuthDto authDto;

        public CommonTest()
        {
            communicationProvider = ApiCommunicationProvider.GetInstance();
            usersQueryProvider = communicationProvider.GetQueryProvider<IUsersQuery>();
            authDto = new ApiAuthDto
            {
                UserName = "admin",
                Password = "sasser",
            };
        }

        protected TContract GetQueryProvider<TContract>()
            => communicationProvider.GetQueryProvider<TContract>();

        protected int[] GenerateUsers(int count, string defaultName = "NowyUserek")
        {
            List<int> result = new List<int>();

            for (int i=0;i< count;i++)
            {
                string name = defaultName + Guid.NewGuid().ToString().Substring(0, 10);
                var dto = new UsersDto()
                {
                    BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                    DisplayName = name,
                    Gender = 'F',
                    Name = name,
                };
                var userId = usersQueryProvider.CreateUser(dto);
                result.Add(userId);

            }
            return result.ToArray();
        }
    }
}
