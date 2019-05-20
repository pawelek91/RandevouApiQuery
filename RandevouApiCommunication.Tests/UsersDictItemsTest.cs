using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouApiCommunication.Users.DictionaryValues;
using Xunit;

namespace RandevouApiCommunication.Tests
{
    public class UsersDictItemsTest:CommonTest
    {
        private readonly IUsersDictionaryValuesQuery _queryProvider;    
        public UsersDictItemsTest()
        {
            _queryProvider = ApiCommunicationProvider.GetInstance().GetQueryProvider<IUsersDictionaryValuesQuery>();
        }

        [Fact]
        public void TestInterestes()
        {
            var result = _queryProvider.GetInterests(authDto);
            Assert.True(result.Any());
        }

        [Fact]
        public void TestHairColors()
        {
            var result = _queryProvider.GetHairColors(authDto);
            Assert.True(result.Any());
        }

        [Fact]
        public void TestEyesColor()
        {
            var result = _queryProvider.GetEyesColors(authDto);
            Assert.True(result.Any());
        }
    }
}
