using System;
using System.Collections.Generic;

namespace RandevouApiCommunication.UsersFinder
{
    public class UsersFinderDto
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? HeightFrom { get; set; }
        public int? HeightTo { get; set; }
        public int? WidthFrom { get; set; }
        public int? WidthTo { get; set; }
        public string Gender { get; set; }
        public bool? Tatoos { get; set; }
        public List<int> InterestIds { get; set; }
        public int? HairColor { get; set; }
        public int? EyesColor { get; set; }
        public bool? IsOnline { get; set; }
    }
}
