﻿// <auto-generated />
using System;
using EventsWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsWebApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241007232006_Initl5")]
    partial class Initl5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventsWebApp.Domain.Models.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("SocialEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SocialEventId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa2f1447-103d-458d-a9c1-32882069366f"),
                            DateOfBirth = new DateTime(1960, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfRegistration = new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(382),
                            Email = "example@gmail.com",
                            Name = "Peter",
                            SocialEventId = new Guid("e355bd39-7810-4216-99af-964912b659d2"),
                            Surname = "Parker",
                            UserId = new Guid("ebec568a-f3ec-46c0-9e38-8deaf6a581e0")
                        },
                        new
                        {
                            Id = new Guid("e6618266-a95a-412c-9e64-f3adec53b14b"),
                            DateOfBirth = new DateTime(1980, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfRegistration = new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(412),
                            Email = "great@gmail.com",
                            Name = "Bruce",
                            SocialEventId = new Guid("e7b2ffc0-6895-4368-ab01-40f8debc7593"),
                            Surname = "Banner",
                            UserId = new Guid("5d5d1024-5b7c-43d7-bbcc-685fe6a5d4c1")
                        },
                        new
                        {
                            Id = new Guid("03663ce6-895e-4098-bb67-17e3cd2e1237"),
                            DateOfBirth = new DateTime(1990, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfRegistration = new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(418),
                            Email = "great@gmail.com",
                            Name = "Clark",
                            SocialEventId = new Guid("0117d2b8-c515-43f8-9a5c-d4b5640dfb80"),
                            Surname = "Kent",
                            UserId = new Guid("b58923bc-c002-422f-a751-5e5eeb3bb068")
                        });
                });

            modelBuilder.Entity("EventsWebApp.Domain.Models.SocialEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAttendee")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SocialEvents");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e355bd39-7810-4216-99af-964912b659d2"),
                            Category = 6,
                            Date = new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A fun-filled trivia night where teams compete to answer questions across various categories. Great prizes await the winners!",
                            EventName = "Trivia Night Extravaganza",
                            Image = "",
                            MaxAttendee = 1,
                            Place = "Minsk"
                        },
                        new
                        {
                            Id = new Guid("e7b2ffc0-6895-4368-ab01-40f8debc7593"),
                            Category = 2,
                            Date = new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A comprehensive conference focused on the latest trends and techniques in digital marketing, featuring expert speakers and interactive sessions.",
                            EventName = "Marketing Strategies Conference",
                            Image = "",
                            MaxAttendee = 2,
                            Place = "Gomel"
                        },
                        new
                        {
                            Id = new Guid("0117d2b8-c515-43f8-9a5c-d4b5640dfb80"),
                            Category = 3,
                            Date = new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "An exciting convention showcasing the latest technology and innovations. Meet industry leaders, attend panel discussions, and explore cutting-edge products.",
                            EventName = "Tech Innovations Convention",
                            Image = "",
                            MaxAttendee = 20,
                            Place = "Gomel"
                        },
                        new
                        {
                            Id = new Guid("8a554725-b634-42aa-b817-12548fb666c8"),
                            Category = 5,
                            Date = new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "An interactive Q&A session with financial experts who will provide insights on personal finance management, investment strategies, and wealth building.",
                            EventName = "Financial Freedom Q&A Session",
                            Image = "",
                            MaxAttendee = 50,
                            Place = "Polotsk"
                        },
                        new
                        {
                            Id = new Guid("99305762-280e-498f-9934-bb7e43563622"),
                            Category = 4,
                            Date = new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A lecture by a renowned art historian covering the evolution and impact of modern art movements in the 20th and 21st centuries.",
                            EventName = "Modern Art Lecture",
                            Image = "",
                            MaxAttendee = 20,
                            Place = "Mogilev"
                        },
                        new
                        {
                            Id = new Guid("d71ef3e9-92a5-4014-89ad-468e6686cf6a"),
                            Category = 1,
                            Date = new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A masterclass led by a successful entrepreneur, sharing insights on how to start and grow a business, including tips on funding, marketing, and scaling.",
                            EventName = "Entrepreneurship MasterClass",
                            Image = "",
                            MaxAttendee = 55,
                            Place = "Vitebsk"
                        },
                        new
                        {
                            Id = new Guid("16e94dac-13c1-4b12-bc38-bf16dc5e97af"),
                            Category = 2,
                            Date = new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A conference discussing the impact of climate change, featuring experts from environmental science, policy-making, and sustainable practices.",
                            EventName = "Climate Change Conference",
                            Image = "",
                            MaxAttendee = 100,
                            Place = "Brest"
                        },
                        new
                        {
                            Id = new Guid("12fe139e-ceb0-46fa-ac9c-052fcdb07500"),
                            Category = 4,
                            Date = new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A lecture by a health expert on holistic wellness practices, covering topics like nutrition, mental health, and exercise for a balanced lifestyle.",
                            EventName = "Health & Wellness Lecture",
                            Image = "",
                            MaxAttendee = 10,
                            Place = "Grodno"
                        },
                        new
                        {
                            Id = new Guid("ca175e55-d602-42fb-b2b4-7dc405e04a7d"),
                            Category = 1,
                            Date = new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A hands-on masterclass with a professional photographer, focusing on advanced techniques in portrait and landscape photography.",
                            EventName = "Photography MasterClass",
                            Image = "",
                            MaxAttendee = 30,
                            Place = "Minsk"
                        },
                        new
                        {
                            Id = new Guid("47a67a9a-5e9e-43c4-84e7-1645752ee74d"),
                            Category = 3,
                            Date = new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A convention designed for professionals looking to enhance their leadership skills, featuring workshops, panel discussions, and keynote speeches by industry leaders.",
                            EventName = "Leadership Development Convention",
                            Image = "",
                            MaxAttendee = 50,
                            Place = "Minsk"
                        },
                        new
                        {
                            Id = new Guid("af2d76eb-d68d-40f5-b603-be42e19535f3"),
                            Category = 6,
                            Date = new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A high-energy, outdoor fitness session led by a professional trainer. Suitable for all fitness levels.",
                            EventName = "Fitness Boot Camp",
                            Image = "",
                            MaxAttendee = 11,
                            Place = "Polotsk"
                        },
                        new
                        {
                            Id = new Guid("6b093954-45f3-40a4-8f85-a22eda54bec5"),
                            Category = 6,
                            Date = new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A monthly book club meeting to discuss the chosen book. Enjoy lively discussions, snacks, and a chance to meet fellow book enthusiasts",
                            EventName = "Book Club Gathering",
                            Image = "",
                            MaxAttendee = 10,
                            Place = "Vitebsk"
                        });
                });

            modelBuilder.Entity("EventsWebApp.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ExpiresRefreshToken")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ebec568a-f3ec-46c0-9e38-8deaf6a581e0"),
                            Email = "example@gmail.com",
                            PasswordHash = "$2a$11$G/glNmIWbUDDTKwThLNvbuvCv.4Cu.Rt8euoJVrGffWcOu0csnYt.",
                            RefreshToken = "",
                            Role = "User",
                            Username = "Jake"
                        },
                        new
                        {
                            Id = new Guid("5d5d1024-5b7c-43d7-bbcc-685fe6a5d4c1"),
                            Email = "admin@gmail.com",
                            PasswordHash = "$2a$11$FSYBbB6WwzissusHth7lIeVQS63Of.nJ59XIJ0bnemQ8t7DjsGgXm",
                            RefreshToken = "",
                            Role = "Admin",
                            Username = "Mark"
                        },
                        new
                        {
                            Id = new Guid("b58923bc-c002-422f-a751-5e5eeb3bb068"),
                            Email = "great@gmail.com",
                            PasswordHash = "$2a$11$DmTeCkbVNtxPhQyLOgXXf.q2DR4SjVu7lp0nOk4lrvFozTC3GmJ1e",
                            RefreshToken = "",
                            Role = "User",
                            Username = "Victor"
                        });
                });

            modelBuilder.Entity("EventsWebApp.Domain.Models.Attendee", b =>
                {
                    b.HasOne("EventsWebApp.Domain.Models.SocialEvent", "SocialEvent")
                        .WithMany("ListOfAttendees")
                        .HasForeignKey("SocialEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventsWebApp.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialEvent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventsWebApp.Domain.Models.SocialEvent", b =>
                {
                    b.Navigation("ListOfAttendees");
                });
#pragma warning restore 612, 618
        }
    }
}