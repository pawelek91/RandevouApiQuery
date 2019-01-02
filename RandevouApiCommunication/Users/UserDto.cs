using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Users
{
    public class UserDto
    {
        public int? Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public char? Gender { get; set; }
    }
}
