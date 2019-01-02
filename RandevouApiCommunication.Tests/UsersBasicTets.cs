using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    public class UsersBasicTets
    {
        [Fact]
        public void UserApiBasicTest()
        {
            var query = new UsersQuery();
            #region Create user
            var dto = new UserDto()
            {
                BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                DisplayName = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
                Gender = 'F',
                Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
            };
            var result = query.CreateUser(dto);
            Assert.True(result > 0);
            
            var createdUser = query.GetUser(result);
            Assert.True(createdUser.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase));
            #endregion

            #region Delete user
            query.DeleteUser(createdUser.Id.Value);
            var resp = query.GetUser(result);
            Assert.Null(resp);
            #endregion

            #region Add and Update
            dto.Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10);
            result = query.CreateUser(dto);
            createdUser = query.GetUser(result);
            
            dto.Gender = 'M';
            dto.Id = result;
            query.UpdateUser(dto);
            var updatedUser = query.GetUser(result);
            Assert.True(updatedUser.Gender == dto.Gender);
            Assert.True(updatedUser.Id == result);

            query.DeleteUser(updatedUser.Id.Value);
            #endregion
        }
        [Fact]
        public void UserDetailsTest()
        {
            var dto = new UserDto()
            {
                BirthDate = new DateTime(DateTime.Now.AddYears(-30).Year, 12, 1),
                DisplayName = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
                Gender = 'F',
                Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10),
            };
            var query = new UsersQuery();
            var userId = query.CreateUser(dto);

            var detailsDto = new UserDetailsDto()
            {
                UserId = userId,
                City = "Moscow",
                Heigth = 120,
                Width = 10,
                Tattos = 2,
            };
            query.UpdateUserDetails(userId, detailsDto);
        }
    }
}
