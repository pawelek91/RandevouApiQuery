using System;
using System.Linq;
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

        public UserFinderTest()
        {
            ufq = GetQueryProvider<IUserFinderQuery>();
        }

        [Fact]
        public void TestUserFinding()
        {
            var users = GenerateUsers(10, "userToFind");
            var searchDto = new UsersFinderDto
            {
                Name = "userToFind"
            };

            var result = ufq.FindUsers(searchDto);
            Assert.True(result.Length >= 10);
        }

    }
}
