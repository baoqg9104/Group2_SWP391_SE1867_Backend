﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Mentor_Booking_System_Data.Data
{
    public class Admin
    {
        public string AdminId { get; set; } // Primary key
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
