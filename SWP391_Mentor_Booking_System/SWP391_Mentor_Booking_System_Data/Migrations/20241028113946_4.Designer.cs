﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SWP391_Mentor_Booking_System_Data;

#nullable disable

namespace SWP391_Mentor_Booking_System_Data.Migrations
{
    [DbContext(typeof(SWP391_Mentor_Booking_System_DBContext))]
    [Migration("20241028113946_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Admin", b =>
                {
                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.BookingSkill", b =>
                {
                    b.Property<int>("BookingSkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingSkillId"));

                    b.Property<int>("BookingSlotId")
                        .HasColumnType("int");

                    b.Property<int>("MentorSkillId")
                        .HasColumnType("int");

                    b.HasKey("BookingSkillId");

                    b.HasIndex("BookingSlotId");

                    b.HasIndex("MentorSkillId");

                    b.ToTable("BookingSkills");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MentorSlotId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingId");

                    b.HasIndex("GroupId");

                    b.HasIndex("MentorSlotId");

                    b.ToTable("BookingSlots");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("GroupFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupRating")
                        .HasColumnType("int");

                    b.Property<string>("MentorFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorRating")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Group", b =>
                {
                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<int>("SwpClassId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.Property<int>("WalletPoint")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("LeaderId");

                    b.HasIndex("SwpClassId");

                    b.HasIndex("TopicId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Mentor", b =>
                {
                    b.Property<string>("MentorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("ApplyStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MentorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumOfSlot")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PointsReceived")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MentorId");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSkill", b =>
                {
                    b.Property<int>("MentorSkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MentorSkillId"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("MentorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("MentorSkillId");

                    b.HasIndex("MentorId");

                    b.HasIndex("SkillId");

                    b.ToTable("MentorSkills");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSlot", b =>
                {
                    b.Property<int>("MentorSlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MentorSlotId"));

                    b.Property<int>("BookingPoint")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MentorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isOnline")
                        .HasColumnType("bit");

                    b.Property<string>("room")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MentorSlotId");

                    b.HasIndex("MentorId");

                    b.ToTable("MentorSlots");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Semester", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SemesterId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("SemesterId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.SwpClass", b =>
                {
                    b.Property<int>("SwpClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SwpClassId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("SwpClassId");

                    b.HasIndex("SemesterId");

                    b.ToTable("SwpClasses");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("TopicId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.WalletTransaction", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WalletId");

                    b.HasIndex("BookingId");

                    b.ToTable("WalletTransactions");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.BookingSkill", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", "BookingSlot")
                        .WithMany("BookingSkills")
                        .HasForeignKey("BookingSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.MentorSkill", "MentorSkill")
                        .WithMany("BookingSkills")
                        .HasForeignKey("MentorSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingSlot");

                    b.Navigation("MentorSkill");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Group", "Group")
                        .WithMany("BookingSlots")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.MentorSlot", "MentorSlot")
                        .WithMany("BookingSlots")
                        .HasForeignKey("MentorSlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("MentorSlot");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Feedback", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", "BookingSlot")
                        .WithOne("Feedback")
                        .HasForeignKey("SWP391_Mentor_Booking_System_Data.Data.Feedback", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingSlot");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Group", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Student", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.SwpClass", "SwpClass")
                        .WithMany("Groups")
                        .HasForeignKey("SwpClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Topic", "Topic")
                        .WithMany("Groups")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Leader");

                    b.Navigation("SwpClass");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSkill", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Mentor", "Mentor")
                        .WithMany("MentorSkills")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Skill", "Skill")
                        .WithMany("MentorSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSlot", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Mentor", "Mentor")
                        .WithMany("MentorSlots")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Student", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.SwpClass", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Semester", "Semester")
                        .WithMany("SwpClasses")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Topic", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.Semester", "Semester")
                        .WithMany("Topics")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.WalletTransaction", b =>
                {
                    b.HasOne("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", "BookingSlot")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingSlot");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.BookingSlot", b =>
                {
                    b.Navigation("BookingSkills");

                    b.Navigation("Feedback")
                        .IsRequired();
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Group", b =>
                {
                    b.Navigation("BookingSlots");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Mentor", b =>
                {
                    b.Navigation("MentorSkills");

                    b.Navigation("MentorSlots");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSkill", b =>
                {
                    b.Navigation("BookingSkills");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.MentorSlot", b =>
                {
                    b.Navigation("BookingSlots");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Semester", b =>
                {
                    b.Navigation("SwpClasses");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Skill", b =>
                {
                    b.Navigation("MentorSkills");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.SwpClass", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("SWP391_Mentor_Booking_System_Data.Data.Topic", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
