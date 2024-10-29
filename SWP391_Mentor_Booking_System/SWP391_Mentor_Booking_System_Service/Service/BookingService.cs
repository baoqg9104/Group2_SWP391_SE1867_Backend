﻿using SWP391_Mentor_Booking_System_Data.Data;
using SWP391_Mentor_Booking_System_Data.DTO.BookingSlot;
using SWP391_Mentor_Booking_System_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SWP391_Mentor_Booking_System_Data.DTO.Transaction;
using System.Text.RegularExpressions;

namespace SWP391_Mentor_Booking_System_Service.Service
{
    public class BookingService
    {
        private readonly SWP391_Mentor_Booking_System_DBContext _context;

        public BookingService(SWP391_Mentor_Booking_System_DBContext context)
        {
            _context = context;
        }

        // Create Booking
        public async Task<(bool Success, string Error)> CreateBookingAsync(CreateBookingDTO createBookingDto)
        {
            // Check if the group exists and has enough wallet points
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == createBookingDto.GroupId);
            if (group == null)
                return (false, "Group does not exist");

            var mentorSlot = await _context.MentorSlots.FirstOrDefaultAsync(ms => ms.MentorSlotId == createBookingDto.MentorSlotId);
            if (mentorSlot == null)
                return (false, "Mentor slot does not exist");

            if (mentorSlot.Status == "Approved")
                return (false, "Slot has approved");

            if (group.WalletPoint < mentorSlot.BookingPoint)
                return (false, "Not enough wallet points");

            // Check if the slot is in the past
            if (mentorSlot.StartTime < DateTime.Now)
                return (false, "Cannot book a slot in the past");

            // Check for overlapping bookings for the group
            var overlappingBooking = await _context.BookingSlots
                .Include(b => b.MentorSlot)
                .FirstOrDefaultAsync(b => b.GroupId == createBookingDto.GroupId
                && b.MentorSlot.StartTime < mentorSlot.EndTime
                && b.MentorSlot.EndTime > mentorSlot.StartTime);


            if (overlappingBooking != null)
                return (false, "The group already has a booking that overlaps with the selected slot");



            // Create the booking
            var booking = new BookingSlot
            {
                GroupId = createBookingDto.GroupId,
                MentorSlotId = createBookingDto.MentorSlotId,
                BookingTime = DateTime.Now,
                Status = "Pending"
            };

           

            _context.BookingSlots.Add(booking);

            foreach (var mentorSkillId in createBookingDto.MentorSkillIds)
            {
                var bookingSkill = new BookingSkill
                {
                    BookingSlot = booking,        // Liên kết với booking vừa tạo
                    MentorSkillId = mentorSkillId // Gán MentorSkillId từ createBookingDto
                };
                _context.BookingSkills.Add(bookingSkill);
            }

            // Deduct wallet points from the group
            group.WalletPoint -= mentorSlot.BookingPoint;   

            await _context.SaveChangesAsync();
            return (true, null);
        }

        // Update Booking Status
        public async Task<bool> UpdateBookingStatusAsync(UpdateBookingStatusDTO updateBookingStatusDto)
        {
            //existingBooking.Status = updateBookingStatusDto.Status;

            if (updateBookingStatusDto.Status.Equals("Approved"))
            {
                var booking = await _context.BookingSlots
                    .FirstOrDefaultAsync(bs => bs.BookingId == updateBookingStatusDto.BookingId);

                var mentorSlot = await _context.MentorSlots
                    .FirstOrDefaultAsync(ms => ms.MentorSlotId == updateBookingStatusDto.MentorSlotId);

                if (booking == null || mentorSlot == null)
                    return false;

                if (mentorSlot.StartTime < DateTime.Now)
                {
                    return false;
                }

                booking.Status = "Approved";
                mentorSlot.Status = "Approved";

                var mentor = await _context.Mentors.FirstOrDefaultAsync(m => m.MentorId == mentorSlot.MentorId);

                mentor.PointsReceived += mentorSlot.BookingPoint;

                var otherBookingsSameSlot = await _context.BookingSlots
                    .Include(bs => bs.Group)
                    .Include(bs => bs.MentorSlot)
                    .Where(bs => bs.BookingId != updateBookingStatusDto.BookingId && bs.MentorSlotId == booking.MentorSlotId).ToListAsync();

                foreach (var b in otherBookingsSameSlot)
                {
                    b.Status = "Denied";
                    b.Group.WalletPoint += b.MentorSlot.BookingPoint;
                }

                // Add Wallet Transactions

                var transaction = new WalletTransaction()
                {
                    BookingId = booking.BookingId,
                    Point = mentorSlot.BookingPoint,
                    DateTime = DateTime.Now
                };

                if (transaction != null)
                {
                    await _context.WalletTransactions.AddAsync(transaction);
                }

            }
            else if (updateBookingStatusDto.Status.Equals("Completed"))
            {
                var mentorSlot = await _context.MentorSlots
                    .FirstOrDefaultAsync(ms => ms.MentorSlotId == updateBookingStatusDto.MentorSlotId);

                var booking = await _context.BookingSlots
                    .FirstOrDefaultAsync(ms => ms.MentorSlotId == updateBookingStatusDto.MentorSlotId && ms.Status == "Approved");

                if (booking == null || mentorSlot == null)
                    return false;

                if (mentorSlot.EndTime > DateTime.Now)
                {
                    return false;
                }

                mentorSlot.Status = "Completed";

                booking.Status = "Completed";
            }

            await _context.SaveChangesAsync();
            return true;

        }

        // Get BookingSlots by MentorSlotId
        public async Task<List<BookingDTO>> GetBookingsByMentorSlotIdAsync(int mentorSlotId)
        {
            var bookings = await _context.BookingSlots
                .Include(bs => bs.BookingSkills)
                    .ThenInclude(bsk => bsk.MentorSkill)
                        .ThenInclude(ms => ms.Skill)
                .Where(bs => bs.MentorSlotId == mentorSlotId)
                .ToListAsync();

            return bookings.Select(bs => new BookingDTO
            {
                BookingId = bs.BookingId,
                GroupId = bs.GroupId,
                GroupName = _context.Groups.FirstOrDefault(g => g.GroupId == bs.GroupId)?.Name ?? "Unknown",
                SwpClass = _context.SwpClasses.FirstOrDefault(c => c.SwpClassId == bs.Group.SwpClassId).Name,
                MentorSlotId = bs.MentorSlotId,
                MentorName = _context.MentorSlots
                .Where(ms => ms.MentorSlotId == bs.MentorSlotId)
                .Select(ms => ms.Mentor.MentorName)
                .FirstOrDefault() ?? "Unknown",
                StartTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .StartTime,
                EndTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .EndTime,
                Room = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .room,
                IsOnline = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .isOnline,
                SkillName = bs.BookingSkills != null
            ? bs.BookingSkills
                .Where(bsk => bsk.MentorSkill != null && bsk.MentorSkill.Skill != null)
                .Select(bsk => bsk.MentorSkill.Skill.Name)
                .ToList()
            : new List<string>(),
                BookingTime = bs.BookingTime,
                TopicName = _context.Topics.FirstOrDefault(t => t.TopicId == bs.Group.TopicId).Name,
                Status = bs.Status
            }).ToList();
        }

        public async Task<List<BookingDTO>> GetBookingByGroupIdAsync(string groupId)
        {
            var bookings = await _context.BookingSlots
                .Include(bs => bs.BookingSkills)
                    .ThenInclude(bsk => bsk.MentorSkill)
                        .ThenInclude(ms => ms.Skill)
                .Where(bs => bs.GroupId == groupId)
                .ToListAsync();

            return bookings.Select(bs => new BookingDTO
            {
                BookingId = bs.BookingId,
                GroupId = bs.GroupId,
                GroupName = _context.Groups.FirstOrDefault(g => g.GroupId == bs.GroupId)?.Name ?? "Unknown",
                MentorSlotId = bs.MentorSlotId,
                MentorName = _context.MentorSlots
                .Where(ms => ms.MentorSlotId == bs.MentorSlotId)
                .Select(ms => ms.Mentor.MentorName)
                .FirstOrDefault() ?? "Unknown",
                StartTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .StartTime,
                EndTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .EndTime,
                Room = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .room,
                IsOnline = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .isOnline,
                SkillName = bs.BookingSkills != null
            ? bs.BookingSkills
                .Where(bsk => bsk.MentorSkill != null && bsk.MentorSkill.Skill != null)
                .Select(bsk => bsk.MentorSkill.Skill.Name)
                .ToList()
            : new List<string>(),
                BookingTime = bs.BookingTime,
                Status = bs.Status
            }).ToList();
        }

        public async Task<List<BookingDTO>> GetBookingsAsync()
        {
            var bookings = await _context.BookingSlots
                .Include(bs => bs.BookingSkills)
                    .ThenInclude(bsk => bsk.MentorSkill)
                        .ThenInclude(ms => ms.Skill)
                .ToListAsync();

            return bookings.Select(bs => new BookingDTO
            {
                BookingId = bs.BookingId,
                GroupId = bs.GroupId,
                GroupName = _context.Groups.FirstOrDefault(g => g.GroupId == bs.GroupId)?.Name ?? "Unknown",
                MentorSlotId = bs.MentorSlotId,
                MentorName = _context.MentorSlots
                .Where(ms => ms.MentorSlotId == bs.MentorSlotId)
                .Select(ms => ms.Mentor.MentorName)
                .FirstOrDefault() ?? "Unknown",
                StartTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .StartTime,
                EndTime = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .EndTime,
                Room = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .room,
                IsOnline = _context.MentorSlots
                .FirstOrDefault(ms => ms.MentorSlotId == bs.MentorSlotId)
                .isOnline,
                SkillName = bs.BookingSkills != null
            ? bs.BookingSkills
                .Where(bsk => bsk.MentorSkill != null && bsk.MentorSkill.Skill != null)
                .Select(bsk => bsk.MentorSkill.Skill.Name)
                .ToList()
            : new List<string>(),
                BookingTime = bs.BookingTime,
                Status = bs.Status
            }).ToList();
        }
    }

}
