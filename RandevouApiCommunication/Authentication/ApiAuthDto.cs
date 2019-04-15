using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouApiCommunication.Authentication
{
    public class ApiAuthDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
