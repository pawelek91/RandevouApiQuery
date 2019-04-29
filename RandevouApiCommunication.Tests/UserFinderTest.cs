using System;
using System.Collections.Generic;
using System.Linq;
using RandevouApiCommunication.Users;
using RandevouApiCommunication.UsersFinder;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    /// <summary>
    /// Test dość prosty sprawdzający czy API działa,
    /// szczegółowy test sprawdzajacy działanie serwisu jest na solucji z API/serwisami
    /// </summary>
    public class UserFinderTest : CommonTest
    {
        IUserFinderQuery ufq;
        IUsersQuery uq;
        public UserFinderTest()
        {
            ufq = GetQueryProvider<IUserFinderQuery>();
            uq = GetQueryProvider<IUsersQuery>();
        }

        [Fact]
        public void TestUserFinding()
        {
            var users = GenerateUsers(10, "userToFind");
            authDto.UserName = users.ElementAt(0).Value;
            var searchDto = new SearchQueryDto
            {
                Name = "userToFind",
            };

            var result = ufq.FindUsers(searchDto, authDto);
            Assert.True(result.Length >= 10);


            var usersDtso = uq.GetManyUsers(authDto, result);

        }

    }
}
