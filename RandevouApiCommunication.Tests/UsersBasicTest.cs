using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    public class UsersBasicTets : CommonTest
    {
        ApiCommunicationProvider communicationProvider;
        IUsersQuery queryProvider;
        public UsersBasicTets()
        {
            communicationProvider = ApiCommunicationProvider.GetInstance();
            queryProvider = communicationProvider.GetQueryProvider<IUsersQuery>();
        }
        [Fact]
        public void UserApiBasicTest()
        {

            #region Create user
            var dto = new UsersDto()
            {
                BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                DisplayName = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
                Gender = 'F',
                Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
            };
            var result = queryProvider.CreateUserWithLogin(new UserComplexDto{
                UserDto = dto,
                Password = authDto.Password,
              });

            Assert.True(result > 0);
            
            authDto.UserName=dto.Name;
            
            var createdUser = queryProvider.GetUser(result, authDto);
            Assert.True(createdUser.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase));
            #endregion

            #region Delete user
            queryProvider.DeleteUser(createdUser.Id.Value, authDto);
            var resp = queryProvider.GetUser(result, authDto);
            Assert.Null(resp);
            #endregion

            #region Add and Update
            dto.Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10);
            authDto.UserName=dto.Name;
            result = queryProvider.CreateUserWithLogin(new UserComplexDto{
                UserDto = dto,
                Password = authDto.Password});
            createdUser = queryProvider.GetUser(result, authDto);
            
            dto.Gender = 'M';
            dto.Id = result;
            queryProvider.UpdateUser(dto, authDto);
            var updatedUser = queryProvider.GetUser(result, authDto);
            Assert.True(updatedUser.Gender == dto.Gender);
            Assert.True(updatedUser.Id == result);

            queryProvider.DeleteUser(updatedUser.Id.Value, authDto);
            #endregion
        }
        [Fact]
        public void UserDetailsTest()
        {
            var dto = new UsersDto()
            {
                BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                DisplayName = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
                Gender = 'F',
                Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
            };
            authDto.UserName = dto.Name;
            var userId = queryProvider.CreateUserWithLogin(
                                        new UserComplexDto{
                                            UserDto = dto,
                                            Password = authDto.Password,});

            var detailsDto = new UserDetailsDto()
            {
                UserId = userId,
                City = "Moscow",
                Heigth = 120,
                Width = 10,
                Tattos = 2,
            };
            queryProvider.UpdateUserDetails(userId, detailsDto, authDto);

            var resultDto = queryProvider.GetUserDetails(userId, authDto);
            Assert.True(Equals(resultDto,detailsDto));
        }

        private bool Equals(UserDetailsDto source, UserDetailsDto target)
        {
            if (source == null || target == null)
                throw new ArgumentOutOfRangeException("source or target is null");

                return
                    source.City == target.City && source.EyesColor == target.EyesColor && source.HairColor == target.EyesColor
                    && source.HairColor == target.HairColor && source.Heigth == target.Heigth && source.Region == target.Region
                    && source.Tattos == target.Tattos && source.Width == target.Width;
            
        }
    }
}
