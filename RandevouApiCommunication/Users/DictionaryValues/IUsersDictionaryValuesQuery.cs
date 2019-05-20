using System;
using System.Collections.Generic;
using System.Text;
using RandevouApiCommunication.Authentication;

namespace RandevouApiCommunication.Users.DictionaryValues
{
    public interface IUsersDictionaryValuesQuery
    {
        DictionaryItemDto[] GetInterests(string apiKey);
        DictionaryItemDto[] GetInterests(ApiAuthDto authDto);
        DictionaryItemDto[] GetHairColors(string apiKey);
        DictionaryItemDto[] GetHairColors(ApiAuthDto authDto);
        DictionaryItemDto[] GetEyesColors(string apiKey);
        DictionaryItemDto[] GetEyesColors(ApiAuthDto authDto);
    }
}
