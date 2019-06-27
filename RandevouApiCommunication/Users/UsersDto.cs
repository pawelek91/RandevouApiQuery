using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public class UsersDto
    {
        public int? Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }

        private string displayName;
        public string DisplayName
        {
            get => string.IsNullOrWhiteSpace(displayName) ? Name : displayName;
            set => displayName = value;
        }
        public char? Gender { get; set; }
    }

    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public int? Width { get; set; }
        public int? Heigth { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int? Tattos { get; set; }

        public byte[] AvatarImage { get; set; }
        public string AvatarContentType { get; set; }

        #region dictionary
        public int? EyesColor { get; set; }
        public int? HairColor { get; set; }
        public int[] Interests { get; set; }
        #endregion
    }
}
