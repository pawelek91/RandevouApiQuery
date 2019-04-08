﻿using RandevouApiCommunication.Authentication;
using System;
namespace RandevouApiCommunication.UsersFinder
{
    public interface IUserFinderQuery
    {
        int[] FindUsers(UsersFinderDto dto, ApiAuthDto authDto = null);
    }
}
