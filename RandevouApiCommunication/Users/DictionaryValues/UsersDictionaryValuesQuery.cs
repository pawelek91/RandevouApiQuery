using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Authentication;

namespace RandevouApiCommunication.Users.DictionaryValues
{
    internal class UsersDictionaryValuesQuery : ApiQuery, IUsersDictionaryValuesQuery
    {
        public DictionaryItemDto[] GetInterests(string apiKey)
            =>Query<DictionaryItemDto[]>(Endpoints.Interests, apiKey).Result;

        public DictionaryItemDto[] GetInterests(ApiAuthDto authDto)
            => Query<DictionaryItemDto[]>(Endpoints.Interests, auth:GetAuthentitaceUserKey(authDto)).Result;


        public DictionaryItemDto[] GetHairColors(string apiKey)
            => Query<DictionaryItemDto[]>(Endpoints.HairColors, auth: CreateAuth(apiKey)).Result;

        public DictionaryItemDto[] GetHairColors(ApiAuthDto authDto)
            => Query<DictionaryItemDto[]>(Endpoints.HairColors, auth: GetAuthentitaceUserKey(authDto)).Result;

        public DictionaryItemDto[] GetEyesColors(string apiKey)
            => Query<DictionaryItemDto[]>(Endpoints.EyesColors, auth: CreateAuth(apiKey)).Result;

        public DictionaryItemDto[] GetEyesColors(ApiAuthDto authDto)
            => Query<DictionaryItemDto[]>(Endpoints.EyesColors, auth: GetAuthentitaceUserKey(authDto)).Result;
    }
}
