using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public class UserAvatarDto
    {
        public int UserId { get; set; }
        public byte[] AvatarContentBytes { get; set; }
        public string AvatarContentType { get; set; }
    }
}
