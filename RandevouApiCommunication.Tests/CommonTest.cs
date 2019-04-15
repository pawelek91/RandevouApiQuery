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
                Password = "testowe",
            };
        }

        protected TContract GetQueryProvider<TContract>()
            => communicationProvider.GetQueryProvider<TContract>();

        protected Dictionary<int, string> GenerateUsers(int count, string defaultName = "NowyUserek")
        {
            var result = new Dictionary<int, string>();

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
                var userId = usersQueryProvider.CreateUserWithLogin(
                    new UserComplexDto{
                            UserDto = dto,
                            Password = authDto.Password,});
                result.Add(userId, dto.Name);

            }
            return result;
        }
    }
}
