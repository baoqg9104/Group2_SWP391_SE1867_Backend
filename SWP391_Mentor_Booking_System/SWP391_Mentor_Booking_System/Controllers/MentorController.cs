﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391_Mentor_Booking_System_Service.Service;

namespace SWP391_Mentor_Booking_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorController : ControllerBase
    {
        private readonly MentorService _mentorService;

        public MentorController(MentorService mentorService)
        {
            _mentorService = mentorService;
        }

        // Endpoint to change apply status
        [HttpPut("change-apply-status/{mentorId}")]
        public async Task<IActionResult> ChangeApplyStatus(string mentorId)
        {
            var result = await _mentorService.ChangeMentorApplyStatusAsync(mentorId);
            if (!result)
                return NotFound();

            return Ok();
        }
    }

}