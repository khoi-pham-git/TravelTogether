using System;
using System.Collections.Generic;

#nullable disable

namespace TravelTogether2.Models
{
    public class Acc1
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    public /*partial*/ class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }


  
}
