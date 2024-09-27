﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Mentor_Booking_System_Data.Data
{
    public class MentorSlot
    {
        public int SlotId { get; set; }
        public int MentorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int BookingPoint { get; set; }
        public bool Mode { get; set; }

        public Mentor Mentor { get; set; }
    }

}
