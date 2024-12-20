﻿using Microsoft.EntityFrameworkCore;
using SWP391_Mentor_Booking_System_Data;
using SWP391_Mentor_Booking_System_Data.DTO.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Mentor_Booking_System_Service.Service
{
    public class TransactionService
    {
        private readonly SWP391_Mentor_Booking_System_DBContext _context;

        public TransactionService(SWP391_Mentor_Booking_System_DBContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDTO>> GetTransactionsByMentorIdAsync(string mentorId)
        {
            var transactions = await _context.WalletTransactions
           .Where(wt => wt.BookingSlot.MentorSlot.MentorId == mentorId
           && (wt.BookingSlot.Status == "Approved" || wt.BookingSlot.Status == "Completed")) 
           .Select(wt => new TransactionDTO
           {
               BookingId = wt.BookingId,
               Type = wt.Type,
               Point = wt.Point,
               DateTime = wt.DateTime,
               GroupName = wt.BookingSlot.Group.Name,
               SwpClassName = wt.BookingSlot.Group.SwpClass.Name,
               MentorName = wt.BookingSlot.MentorSlot.Mentor.MentorName
           })
           .ToListAsync();

            return transactions;
        }

        public async Task<List<TransactionDTO>> GetTransactionsByGroupIdAsync(string groupId)
        {
            var transactions = await _context.WalletTransactions
           .Where(wt => wt.BookingSlot.Group.GroupId == groupId) // Lọc theo groupId
           .Select(wt => new TransactionDTO
           {
               BookingId = wt.BookingId,
               Type = wt.Type,
               Point = wt.Point,
               DateTime = wt.DateTime,
               GroupName = wt.BookingSlot.Group.Name,
               SwpClassName = wt.BookingSlot.Group.SwpClass.Name,
               MentorName = wt.BookingSlot.MentorSlot.Mentor.MentorName
           })
           .ToListAsync();

            return transactions;
        }
    }
}
