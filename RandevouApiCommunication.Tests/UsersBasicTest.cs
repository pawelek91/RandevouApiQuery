using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RandevouApiCommunication.Users;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    public class UsersBasicTets : CommonTest
    {
        readonly IUsersQuery _queryProvider;
        public UsersBasicTets()
        {
            var communicationProvider = ApiCommunicationProvider.GetInstance();
            _queryProvider = communicationProvider.GetQueryProvider<IUsersQuery>();
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
            var result = _queryProvider.CreateUserWithLogin(new UserComplexDto{
                UserDto = dto,
                Password = authDto.Password,
              });

            Assert.True(result > 0);
            
            authDto.UserName=dto.Name;
            
            var createdUser = _queryProvider.GetUser(result, authDto);
            Assert.True(createdUser.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase));
            #endregion

            #region Delete user
            _queryProvider.DeleteUser(createdUser.Id.Value, authDto);
            var resp = _queryProvider.GetUser(result, authDto);
            Assert.Null(resp);
            #endregion

            #region Add and Update
            dto.Name = "NowyUserek" + Guid.NewGuid().ToString().Substring(0, 10);
            authDto.UserName=dto.Name;
            result = _queryProvider.CreateUserWithLogin(new UserComplexDto{
                UserDto = dto,
                Password = authDto.Password});
            createdUser = _queryProvider.GetUser(result, authDto);
            
            dto.Gender = 'M';
            dto.Id = result;
            _queryProvider.UpdateUser(dto, authDto);
            var updatedUser = _queryProvider.GetUser(result, authDto);
            Assert.True(updatedUser.Gender == dto.Gender);
            Assert.True(updatedUser.Id == result);

            _queryProvider.DeleteUser(updatedUser.Id.Value, authDto);
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
            var userId = _queryProvider.CreateUserWithLogin(
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
            _queryProvider.UpdateUserDetails(userId, detailsDto, authDto);

            var resultDto = _queryProvider.GetUserDetails(userId, authDto);
            Assert.True(Equals(resultDto,detailsDto));
        }

        [Fact]
        public void SetAvatarTest()
        {
            byte[] fileStreamContent = null;
            var userId = _queryProvider.GetUsersLists(authDto).FirstOrDefault(x => x.Name.Equals(authDto.UserName)).Id;

            var filePath = @"C:\temp\testpicture.bmp";
            using (var fileStream = new FileStream(filePath,FileMode.Open ))
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    fileStreamContent = ms.ToArray();
                }
                fileStream.Seek(0, SeekOrigin.Begin);
                string contentType = @"image/bmp";
                _queryProvider.SetAvatar(userId.Value, fileStream, contentType, authDto);
              
            }

            var ud = _queryProvider.GetUserDetails(userId.Value, authDto);
            var imagesEquals = ByteArrayCompare(ud.AvatarImage, fileStreamContent);
            Assert.True(imagesEquals);

        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
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
