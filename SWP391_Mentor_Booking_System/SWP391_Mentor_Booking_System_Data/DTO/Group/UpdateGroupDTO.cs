﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Mentor_Booking_System_Data.DTO.Group
{
    public class UpdateGroupDTO
    {
        public string? GroupId { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public int SwpClassId { get; set; }
    }
}
