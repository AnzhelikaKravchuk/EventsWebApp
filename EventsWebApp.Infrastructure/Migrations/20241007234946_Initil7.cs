using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initil7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "SocialEvents",
                columns: new[] { "Id", "Category", "Date", "Description", "EventName", "Image", "MaxAttendee", "Place" },
                values: new object[,]
                {
                    { new Guid("097c029b-53c9-460d-9b7f-e1978a42ef78"), 6, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A high-energy, outdoor fitness session led by a professional trainer. Suitable for all fitness levels.", "Fitness Boot Camp", "images\\2006cffb-cd31-4fc8-ad90-1e20452dc255-изображение_2024-10-08_024022180.png", 11, "Polotsk" },
                    { new Guid("242c723e-fc49-43ef-9dd4-4a82a6457770"), 3, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "An exciting convention showcasing the latest technology and innovations. Meet industry leaders, attend panel discussions, and explore cutting-edge products.", "Tech Innovations Convention", "images\\2c3a971f-0bfc-4dda-9675-ed75d7d07db5-изображение_2024-10-08_024255264.png", 20, "Gomel" },
                    { new Guid("3582f285-5166-451d-be8b-3d151f61c07b"), 1, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A masterclass led by a successful entrepreneur, sharing insights on how to start and grow a business, including tips on funding, marketing, and scaling.", "Entrepreneurship MasterClass", "images\\4b4fd120-0fce-49ef-b9fb-178c5eba4f72-изображение_2024-10-08_024111696.png", 55, "Vitebsk" },
                    { new Guid("58295261-1669-4981-977d-7e15440efa52"), 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A hands-on masterclass with a professional photographer, focusing on advanced techniques in portrait and landscape photography.", "Photography MasterClass", "images\\ac7044ab-c598-42c0-8624-e5a8f157624b-изображение_2024-10-08_024317048.png", 30, "Minsk" },
                    { new Guid("5e2d9347-8a95-49d2-b58b-28adeeb0d21e"), 5, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "An interactive Q&A session with financial experts who will provide insights on personal finance management, investment strategies, and wealth building.", "Financial Freedom Q&A Session", "images\\fcefd765-202c-433b-b98c-b03065d019a5-изображение_2024-10-08_024038630.png", 50, "Polotsk" },
                    { new Guid("7436896f-e363-4890-a67d-d151a498c0f4"), 4, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lecture by a health expert on holistic wellness practices, covering topics like nutrition, mental health, and exercise for a balanced lifestyle.", "Health & Wellness Lecture", "images\\e4343661-d697-49ed-bf9c-cb10a4ec7cf4-изображение_2024-10-08_023959172.png", 10, "Grodno" },
                    { new Guid("7487e5ce-418b-4f53-a62b-86c47ccd0d9a"), 6, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fun-filled trivia night where teams compete to answer questions across various categories. Great prizes await the winners!", "Trivia Night Extravaganza", "images\\dc7ea763-8d70-43d5-bf36-c05242b31029-изображение_2024-10-08_024226418.png", 1, "Minsk" },
                    { new Guid("78943cc5-b0f5-49cb-92b7-a1e07d99eb2c"), 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive conference focused on the latest trends and techniques in digital marketing, featuring expert speakers and interactive sessions.", "Marketing Strategies Conference", "images\\af73310b-bd4a-4731-a052-5c19865f4c7a-изображение_2024-10-08_023853621.png", 2, "Gomel" },
                    { new Guid("7c3eff58-b71b-4e32-86e6-8a5255f2e4c8"), 6, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A monthly book club meeting to discuss the chosen book. Enjoy lively discussions, snacks, and a chance to meet fellow book enthusiasts", "Book Club Gathering", "images\\0dcc6bce-65ee-4555-8495-5b9b8f7e8fc0-изображение_2024-10-08_024204332.png", 10, "Vitebsk" },
                    { new Guid("aa3a61ec-8f2f-4bec-a7d9-380bf68cb047"), 2, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference discussing the impact of climate change, featuring experts from environmental science, policy-making, and sustainable practices.", "Climate Change Conference", "images\\d57d7aba-fa51-48f8-bc13-8a250489e219-изображение_2024-10-08_024142528.png", 100, "Brest" },
                    { new Guid("b2111853-c49d-4606-adb6-1e029ebb304d"), 3, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "A convention designed for professionals looking to enhance their leadership skills, featuring workshops, panel discussions, and keynote speeches by industry leaders.", "Leadership Development Convention", "images\\2f85a33e-04a7-42c9-9974-67b033c66d6f-изображение_2024-10-08_023935489.png", 50, "Minsk" },
                    { new Guid("fd600952-f8e6-4a73-a346-9a70ac0fbabe"), 4, new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "A lecture by a renowned art historian covering the evolution and impact of modern art movements in the 20th and 21st centuries.", "Modern Art Lecture", "images\\be1df92e-4250-4ce7-8d3e-b82b1a3ea6fe-изображение_2024-10-08_024330991.png", 20, "Mogilev" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ExpiresRefreshToken", "PasswordHash", "RefreshToken", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("11a368e8-3acf-4b8f-a82a-a2e6841ee4c1"), "great@gmail.com", null, "$2a$11$PWLX6tR4Eq8pw657iF8eAep19WYQ5S4qXBzPGuZV62pwBSN.TRDtW", "", "User", "Victor" },
                    { new Guid("71252927-119c-4cb2-8722-facff9e493d5"), "example@gmail.com", null, "$2a$11$9G6W9TMbak.tEln.2UZs.eV7wijqdbsfGi0bxQXbTvHATlNxvplBW", "", "User", "Jake" },
                    { new Guid("879acf08-83f9-44d7-8447-eaa203dc8ff8"), "admin@gmail.com", null, "$2a$11$JIQLxNwlInQ0HUvMbQQsDOKY5jbdfWXPmkZeH6iH5uZ/WUiKPea4m", "", "Admin", "Mark" }
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "DateOfBirth", "DateOfRegistration", "Email", "Name", "SocialEventId", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("81efed88-0d98-4d30-825d-8d58fb85ac72"), new DateTime(1980, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 49, 46, 542, DateTimeKind.Local).AddTicks(5250), "great@gmail.com", "Bruce", new Guid("78943cc5-b0f5-49cb-92b7-a1e07d99eb2c"), "Banner", new Guid("879acf08-83f9-44d7-8447-eaa203dc8ff8") },
                    { new Guid("c65e796f-5133-49a0-b519-52382a27e868"), new DateTime(1990, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 49, 46, 542, DateTimeKind.Local).AddTicks(5256), "great@gmail.com", "Clark", new Guid("242c723e-fc49-43ef-9dd4-4a82a6457770"), "Kent", new Guid("11a368e8-3acf-4b8f-a82a-a2e6841ee4c1") },
                    { new Guid("dc46dfad-3f56-4720-86d3-de1338a52793"), new DateTime(1960, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 2, 49, 46, 542, DateTimeKind.Local).AddTicks(5227), "example@gmail.com", "Peter", new Guid("7487e5ce-418b-4f53-a62b-86c47ccd0d9a"), "Parker", new Guid("71252927-119c-4cb2-8722-facff9e493d5") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("81efed88-0d98-4d30-825d-8d58fb85ac72"));

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("c65e796f-5133-49a0-b519-52382a27e868"));

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: new Guid("dc46dfad-3f56-4720-86d3-de1338a52793"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("097c029b-53c9-460d-9b7f-e1978a42ef78"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("3582f285-5166-451d-be8b-3d151f61c07b"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("58295261-1669-4981-977d-7e15440efa52"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("5e2d9347-8a95-49d2-b58b-28adeeb0d21e"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("7436896f-e363-4890-a67d-d151a498c0f4"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("7c3eff58-b71b-4e32-86e6-8a5255f2e4c8"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("aa3a61ec-8f2f-4bec-a7d9-380bf68cb047"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("b2111853-c49d-4606-adb6-1e029ebb304d"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("fd600952-f8e6-4a73-a346-9a70ac0fbabe"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("242c723e-fc49-43ef-9dd4-4a82a6457770"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("7487e5ce-418b-4f53-a62b-86c47ccd0d9a"));

            migrationBuilder.DeleteData(
                table: "SocialEvents",
                keyColumn: "Id",
                keyValue: new Guid("78943cc5-b0f5-49cb-92b7-a1e07d99eb2c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11a368e8-3acf-4b8f-a82a-a2e6841ee4c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("71252927-119c-4cb2-8722-facff9e493d5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("879acf08-83f9-44d7-8447-eaa203dc8ff8"));

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
    }
}
