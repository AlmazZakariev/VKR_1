﻿// <auto-generated />
using System;
using DAL.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.EfStructures.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.General", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte[]>("Active")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varbinary(1)")
                        .HasColumnName("active");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("general");
                });

            modelBuilder.Entity("Domain.Entities.Registration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AdministratorId")
                        .HasColumnType("bigint")
                        .HasColumnName("administrator_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<long>("RequestId")
                        .HasColumnType("bigint")
                        .HasColumnName("request_id");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint")
                        .HasColumnName("room_id");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("registrations");
                });

            modelBuilder.Entity("Domain.Entities.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<DateTime?>("PreferenceDate")
                        .HasColumnType("datetime")
                        .HasColumnName("preference_date");

                    b.Property<long>("TimeSlotId")
                        .HasColumnType("bigint")
                        .HasColumnName("time_slot_id");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TimeSlotId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("requests");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<short?>("AddNumber")
                        .HasColumnType("smallint")
                        .HasColumnName("add_number");

                    b.Property<short>("Floor")
                        .HasColumnType("smallint")
                        .HasColumnName("floor");

                    b.Property<short>("FreeSlots")
                        .HasColumnType("smallint")
                        .HasColumnName("free_slots");

                    b.Property<short?>("Gender")
                        .HasColumnType("smallint")
                        .HasColumnName("gender");

                    b.Property<short>("Number")
                        .HasColumnType("smallint")
                        .HasColumnName("number");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("Domain.Entities.SeriLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("LineNumber")
                        .HasColumnType("int");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("Xml");

                    b.Property<string>("RequestPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceContext")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Id");

                    b.ToTable("SeriLogs", "Logging");
                });

            modelBuilder.Entity("Domain.Entities.TimeSlot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AdministratorId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("administrator_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("dateTime")
                        .HasColumnName("date");

                    b.Property<byte[]>("Free")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("binary(1)")
                        .HasColumnName("free")
                        .IsFixedLength();

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.ToTable("time_slots");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte[]>("Admin")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("binary(1)")
                        .HasColumnName("admin")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<short?>("Gender")
                        .HasColumnType("smallint")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("pass");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("patronymic");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("phone");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("surname");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Domain.Entities.Registration", b =>
                {
                    b.HasOne("Domain.Entities.User", "Administrator")
                        .WithMany("Registrations")
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_registrations_users");

                    b.HasOne("Domain.Entities.Request", "Request")
                        .WithOne("Registration")
                        .HasForeignKey("Domain.Entities.Registration", "RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_registrations_requests");

                    b.HasOne("Domain.Entities.Room", "Room")
                        .WithMany("Registrations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_registrations_rooms");

                    b.Navigation("Administrator");

                    b.Navigation("Request");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.Request", b =>
                {
                    b.HasOne("Domain.Entities.TimeSlot", "TimeSlot")
                        .WithOne("Request")
                        .HasForeignKey("Domain.Entities.Request", "TimeSlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_requests_time_slots");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Request")
                        .HasForeignKey("Domain.Entities.Request", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_requests_users");

                    b.Navigation("TimeSlot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.TimeSlot", b =>
                {
                    b.HasOne("Domain.Entities.User", "Administrator")
                        .WithMany("TimeSlots")
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_time_slots_users");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("Domain.Entities.Request", b =>
                {
                    b.Navigation("Registration");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Domain.Entities.TimeSlot", b =>
                {
                    b.Navigation("Request");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Registrations");

                    b.Navigation("Request")
                        .IsRequired();

                    b.Navigation("TimeSlots");
                });
#pragma warning restore 612, 618
        }
    }
}
