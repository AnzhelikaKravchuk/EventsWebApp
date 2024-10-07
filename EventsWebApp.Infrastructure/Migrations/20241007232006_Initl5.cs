using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initl5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresRefreshToken",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "SocialEvents",
                columns: new[] { "Id", "Category", "Date", "Description", "EventName", "Image", "MaxAttendee", "Place" },
                values: new object[,]
                {
                    { new Guid("0117d2b8-c515-43f8-9a5c-d4b5640dfb80"), 3, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "An exciting convention showcasing the latest technology and innovations. Meet industry leaders, attend panel discussions, and explore cutting-edge products.", "Tech Innovations Convention", "", 20, "Gomel" },
                    { new Guid("12fe139e-ceb0-46fa-ac9c-052fcdb07500"), 4, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lecture by a health expert on holistic wellness practices, covering topics like nutrition, mental health, and exercise for a balanced lifestyle.", "Health & Wellness Lecture", "", 10, "Grodno" },
                    { new Guid("16e94dac-13c1-4b12-bc38-bf16dc5e97af"), 2, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference discussing the impact of climate change, featuring experts from environmental science, policy-making, and sustainable practices.", "Climate Change Conference", "", 100, "Brest" },
                    { new Guid("47a67a9a-5e9e-43c4-84e7-1645752ee74d"), 3, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A convention designed for professionals looking to enhance their leadership skills, featuring workshops, panel discussions, and keynote speeches by industry leaders.", "Leadership Development Convention", "", 50, "Minsk" },
                    { new Guid("6b093954-45f3-40a4-8f85-a22eda54bec5"), 6, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A monthly book club meeting to discuss the chosen book. Enjoy lively discussions, snacks, and a chance to meet fellow book enthusiasts", "Book Club Gathering", "", 10, "Vitebsk" },
                    { new Guid("8a554725-b634-42aa-b817-12548fb666c8"), 5, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "An interactive Q&A session with financial experts who will provide insights on personal finance management, investment strategies, and wealth building.", "Financial Freedom Q&A Session", "", 50, "Polotsk" },
                    { new Guid("99305762-280e-498f-9934-bb7e43563622"), 4, new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lecture by a renowned art historian covering the evolution and impact of modern art movements in the 20th and 21st centuries.", "Modern Art Lecture", "", 20, "Mogilev" },
                    { new Guid("af2d76eb-d68d-40f5-b603-be42e19535f3"), 6, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A high-energy, outdoor fitness session led by a professional trainer. Suitable for all fitness levels.", "Fitness Boot Camp", "", 11, "Polotsk" },
                    { new Guid("ca175e55-d602-42fb-b2b4-7dc405e04a7d"), 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A hands-on masterclass with a professional photographer, focusing on advanced techniques in portrait and landscape photography.", "Photography MasterClass", "", 30, "Minsk" },
                    { new Guid("d71ef3e9-92a5-4014-89ad-468e6686cf6a"), 1, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A masterclass led by a successful entrepreneur, sharing insights on how to start and grow a business, including tips on funding, marketing, and scaling.", "Entrepreneurship MasterClass", "", 55, "Vitebsk" },
                    { new Guid("e355bd39-7810-4216-99af-964912b659d2"), 6, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fun-filled trivia night where teams compete to answer questions across various categories. Great prizes await the winners!", "Trivia Night Extravaganza", "", 1, "Minsk" },
                    { new Guid("e7b2ffc0-6895-4368-ab01-40f8debc7593"), 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive conference focused on the latest trends and techniques in digital marketing, featuring expert speakers and interactive sessions.", "Marketing Strategies Conference", "", 2, "Gomel" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ExpiresRefreshToken", "PasswordHash", "RefreshToken", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("5d5d1024-5b7c-43d7-bbcc-685fe6a5d4c1"), "admin@gmail.com", null, "$2a$11$FSYBbB6WwzissusHth7lIeVQS63Of.nJ59XIJ0bnemQ8t7DjsGgXm", "", "Admin", "Mark" },
                    { new Guid("b58923bc-c002-422f-a751-5e5eeb3bb068"), "great@gmail.com", null, "$2a$11$DmTeCkbVNtxPhQyLOgXXf.q2DR4SjVu7lp0nOk4lrvFozTC3GmJ1e", "", "User", "Victor" },
                    { new Guid("ebec568a-f3ec-46c0-9e38-8deaf6a581e0"), "example@gmail.com", null, "$2a$11$G/glNmIWbUDDTKwThLNvbuvCv.4Cu.Rt8euoJVrGffWcOu0csnYt.", "", "User", "Jake" }
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "DateOfBirth", "DateOfRegistration", "Email", "Name", "SocialEventId", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("03663ce6-895e-4098-bb67-17e3cd2e1237"), new DateTime(1990, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(418), "great@gmail.com", "Clark", new Guid("0117d2b8-c515-43f8-9a5c-d4b5640dfb80"), "Kent", new Guid("b58923bc-c002-422f-a751-5e5eeb3bb068") },
                    { new Guid("aa2f1447-103d-458d-a9c1-32882069366f"), new DateTime(1960, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(382), "example@gmail.com", "Peter", new Guid("e355bd39-7810-4216-99af-964912b659d2"), "Parker", new Guid("ebec568a-f3ec-46c0-9e38-8deaf6a581e0") },
                    { new Guid("e6618266-a95a-412c-9e64-f3adec53b14b"), new DateTime(1980, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 20, 6, 524, DateTimeKind.Local).AddTicks(412), "great@gmail.com", "Bruce", new Guid("e7b2ffc0-6895-4368-ab01-40f8debc7593"), "Banner", new Guid("5d5d1024-5b7c-43d7-bbcc-685fe6a5d4c1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("03663ce6-895e-4098-bb67-17e3cd2e1237"));

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("aa2f1447-103d-458d-a9c1-32882069366f"));

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("e6618266-a95a-412c-9e64-f3adec53b14b"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("12fe139e-ceb0-46fa-ac9c-052fcdb07500"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("16e94dac-13c1-4b12-bc38-bf16dc5e97af"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("47a67a9a-5e9e-43c4-84e7-1645752ee74d"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("6b093954-45f3-40a4-8f85-a22eda54bec5"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("8a554725-b634-42aa-b817-12548fb666c8"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("99305762-280e-498f-9934-bb7e43563622"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("af2d76eb-d68d-40f5-b603-be42e19535f3"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("ca175e55-d602-42fb-b2b4-7dc405e04a7d"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("d71ef3e9-92a5-4014-89ad-468e6686cf6a"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("0117d2b8-c515-43f8-9a5c-d4b5640dfb80"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("e355bd39-7810-4216-99af-964912b659d2"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("e7b2ffc0-6895-4368-ab01-40f8debc7593"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d5d1024-5b7c-43d7-bbcc-685fe6a5d4c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b58923bc-c002-422f-a751-5e5eeb3bb068"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ebec568a-f3ec-46c0-9e38-8deaf6a581e0"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresRefreshToken",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
