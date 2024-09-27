﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Mentor_Booking_System_Data.Data
{
    public class WalletTransaction
    {
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public string Type { get; set; }
        public int Point { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public BookingSlot BookingSlot { get; set; }
    }

}
