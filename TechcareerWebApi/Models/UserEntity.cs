using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechcareerWebApi.Models
{
    public class UserEntity
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
    }
}