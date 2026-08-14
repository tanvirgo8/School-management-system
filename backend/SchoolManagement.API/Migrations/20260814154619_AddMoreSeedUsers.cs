using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("17232891-7f1b-4967-a0ef-b62e0b7cebaf"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("7bbd1ef4-9432-4ea2-ab29-b6fda8e6ad11"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("a1ad2589-6f12-40a3-acc8-a51262255649"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("cd6c1df0-a612-4bac-ba86-6804a24a41fd"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("047cbd26-036e-4dcf-b18a-26d1a3f0abb7"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("11201fde-d423-4751-8d04-9bef0f11fda6"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("2e4bbb50-289f-483d-8d3a-6abd7c88d69b"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("5b33d480-6a78-477c-8b1c-e4ca2075f179"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("77334f15-8913-4db3-871d-eb78ea87d4cb"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("da1816ed-a2cf-470e-8af2-6368bff30266"));

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 11, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5364), new DateTime(2026, 8, 21, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5350), new DateTime(2026, 8, 11, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5366) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 9, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5379), new DateTime(2026, 8, 19, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5376), new DateTime(2026, 8, 9, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5381) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 30, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5393), new DateTime(2026, 8, 12, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5390), new DateTime(2026, 7, 30, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5394) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 13, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5405), new DateTime(2026, 8, 24, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5403), new DateTime(2026, 8, 13, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5407) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5427), new DateTime(2026, 8, 28, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5424), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5428) });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(5781));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6116));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6119));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6122));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 15, 46, 14, 680, DateTimeKind.Utc).AddTicks(6113));

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3530c972-b4fe-4db8-85df-61199f54ea8f"), "1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3\n2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3\n3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3", new Guid("40000000-0000-0000-0000-000000000001"), "Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.", 88, null, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 12, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5587), new DateTime(2026, 8, 12, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5590) },
                    { new Guid("45fde33b-090f-4401-bc26-65ebb72e5a92"), "1) x = 2 or x = 3\n2) x = -0.5 or x = -3\n3) x = 3 or x = -3\n(Solved using quadratic formula for all)", new Guid("40000000-0000-0000-0000-000000000001"), null, null, null, 2, new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2026, 8, 13, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5610), new DateTime(2026, 8, 13, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5612) },
                    { new Guid("d891cff1-0590-43e2-bd8a-839356d3cad2"), "Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...", new Guid("40000000-0000-0000-0000-000000000002"), null, null, null, 1, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 14, 9, 46, 18, 207, DateTimeKind.Utc).AddTicks(5623), new DateTime(2026, 8, 14, 9, 46, 18, 207, DateTimeKind.Utc).AddTicks(5626) },
                    { new Guid("f7773fd0-4043-4110-8146-9f2762c7203b"), "Newton's First Law: An object at rest stays at rest... [detailed answers]", new Guid("40000000-0000-0000-0000-000000000003"), "Good understanding of the first two laws. Work on applying the third law in complex systems.", 72, null, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 9, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5646), new DateTime(2026, 8, 9, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5648) }
                });

            migrationBuilder.InsertData(
                table: "TeacherAssignments",
                columns: new[] { "Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("28b3d2bc-4b70-4c1f-8d3e-f5180a54b96b"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5111), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("4c8db5c2-1b41-4f42-85cc-cdbcf23d2e27"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(4875), new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("65da5580-27c5-415e-8f08-c94e732ba86e"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5103), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("7073c50e-7080-4bbb-91ad-3d2c6efbe927"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(4854), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("b55b7c53-6d4f-456a-88fd-95cf76778e4d"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(5081), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("dc02e9a5-7042-4844-8aa7-3b17df20af24"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(4866), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 14, 15, 46, 14, 846, DateTimeKind.Utc).AddTicks(5011), "$2a$11$5REohqOllGiuFkFsKa8D1.F5BcrlziMBWI4GL0VA5jEtO38eiQnj6", new DateTime(2025, 8, 14, 15, 46, 14, 846, DateTimeKind.Utc).AddTicks(5027) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 14, 15, 46, 15, 7, DateTimeKind.Utc).AddTicks(3423), "$2a$11$9ajSVm5oqU4Bg4iT/QStkeaGglO/Df6XFuWuhak6CNT9kuxfNnIjy", new DateTime(2025, 10, 14, 15, 46, 15, 7, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 15, 46, 15, 205, DateTimeKind.Utc).AddTicks(2529), "$2a$11$.zGwUqXxQkaQmyHg6Vye/.pX7RbHyFH7BlFT4T/Jb4St25.e8vYwa", new DateTime(2025, 12, 14, 15, 46, 15, 205, DateTimeKind.Utc).AddTicks(2569) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 15, 46, 15, 558, DateTimeKind.Utc).AddTicks(2504), "$2a$11$2TtwiuNppviN5AK0G2DKUubf2wvdxK55DHGMTuM2uvYfZwJB9WZR6", new DateTime(2026, 2, 14, 15, 46, 15, 558, DateTimeKind.Utc).AddTicks(2519) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 15, 46, 15, 857, DateTimeKind.Utc).AddTicks(4242), "$2a$11$WTJyW/sbtp5VYIuuikrgs.bR81d0RCLnu9Q13ys7ZQsoFeKpmNFS.", new DateTime(2026, 2, 14, 15, 46, 15, 857, DateTimeKind.Utc).AddTicks(4257) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 15, 46, 16, 195, DateTimeKind.Utc).AddTicks(4439), "$2a$11$5KPrlSBbugrsSRjP6UNQVOssoClfeNySRLKDXj28VOOA.GHM2lBRy", new DateTime(2026, 3, 14, 15, 46, 16, 195, DateTimeKind.Utc).AddTicks(4458) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 15, 46, 16, 541, DateTimeKind.Utc).AddTicks(7283), "$2a$11$4BD9ioRn6dOdA7KNKIndGu8TWJnZq8tn4HJJNLuLMNlgqU8cEUBhC", new DateTime(2026, 3, 14, 15, 46, 16, 541, DateTimeKind.Utc).AddTicks(7309) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 14, 15, 46, 16, 872, DateTimeKind.Utc).AddTicks(5871), "$2a$11$w9csFq.kDf5witgnu24LBO4R4uEega3TnUBxtucivPEw3wbAEKB7q", new DateTime(2026, 4, 14, 15, 46, 16, 872, DateTimeKind.Utc).AddTicks(5896) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000009"), null, new DateTime(2026, 5, 14, 15, 46, 17, 229, DateTimeKind.Utc).AddTicks(2160), "michael.brown@school.com", "Michael Brown", true, "$2a$11$DK5MZLjW2tjkR9Rk/lA13u6Hl5JhHqOeRfA1kSgXAs3pAYNlil4pO", "+1-555-0103", 1, new DateTime(2026, 5, 14, 15, 46, 17, 229, DateTimeKind.Utc).AddTicks(2183) },
                    { new Guid("30000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 5, 14, 15, 46, 17, 674, DateTimeKind.Utc).AddTicks(8614), "sophia.wilson@school.com", "Sophia Wilson", true, "$2a$11$BORnNoAsRn1NvUcHAOyu5OKjIMda5//mJ7hMD2F/nOcs3mKA2zavC", "+1-555-0206", 2, new DateTime(2026, 5, 14, 15, 46, 17, 674, DateTimeKind.Utc).AddTicks(8644) },
                    { new Guid("30000000-0000-0000-0000-000000000011"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 5, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(2675), "james.taylor@school.com", "James Taylor", true, "$2a$11$lhk9qRgEvurpjoDmfVjS0OPg/jbljhAKQ/ywKmdO1QOK8/VFsDxsu", "+1-555-0207", 2, new DateTime(2026, 5, 14, 15, 46, 18, 207, DateTimeKind.Utc).AddTicks(2698) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("3530c972-b4fe-4db8-85df-61199f54ea8f"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("45fde33b-090f-4401-bc26-65ebb72e5a92"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("d891cff1-0590-43e2-bd8a-839356d3cad2"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("f7773fd0-4043-4110-8146-9f2762c7203b"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("28b3d2bc-4b70-4c1f-8d3e-f5180a54b96b"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("4c8db5c2-1b41-4f42-85cc-cdbcf23d2e27"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("65da5580-27c5-415e-8f08-c94e732ba86e"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("7073c50e-7080-4bbb-91ad-3d2c6efbe927"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("b55b7c53-6d4f-456a-88fd-95cf76778e4d"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("dc02e9a5-7042-4844-8aa7-3b17df20af24"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000011"));

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 11, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3689), new DateTime(2026, 8, 21, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3669), new DateTime(2026, 8, 11, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3690) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3706), new DateTime(2026, 8, 19, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3704), new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3707) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 30, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3715), new DateTime(2026, 8, 12, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3713), new DateTime(2026, 7, 30, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3716) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3726), new DateTime(2026, 8, 24, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3723), new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3743), new DateTime(2026, 8, 28, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3741), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3744) });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7794));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7831));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8612));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8618));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8625));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8605));

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("17232891-7f1b-4967-a0ef-b62e0b7cebaf"), "1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3\n2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3\n3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3", new Guid("40000000-0000-0000-0000-000000000001"), "Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.", 88, null, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 12, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3875), new DateTime(2026, 8, 12, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3877) },
                    { new Guid("7bbd1ef4-9432-4ea2-ab29-b6fda8e6ad11"), "1) x = 2 or x = 3\n2) x = -0.5 or x = -3\n3) x = 3 or x = -3\n(Solved using quadratic formula for all)", new Guid("40000000-0000-0000-0000-000000000001"), null, null, null, 2, new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3888), new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3889) },
                    { new Guid("a1ad2589-6f12-40a3-acc8-a51262255649"), "Newton's First Law: An object at rest stays at rest... [detailed answers]", new Guid("40000000-0000-0000-0000-000000000003"), "Good understanding of the first two laws. Work on applying the third law in complex systems.", 72, null, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3930), new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3931) },
                    { new Guid("cd6c1df0-a612-4bac-ba86-6804a24a41fd"), "Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...", new Guid("40000000-0000-0000-0000-000000000002"), null, null, null, 1, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 14, 5, 19, 50, 592, DateTimeKind.Utc).AddTicks(3913), new DateTime(2026, 8, 14, 5, 19, 50, 592, DateTimeKind.Utc).AddTicks(3916) }
                });

            migrationBuilder.InsertData(
                table: "TeacherAssignments",
                columns: new[] { "Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("047cbd26-036e-4dcf-b18a-26d1a3f0abb7"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3269), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("11201fde-d423-4751-8d04-9bef0f11fda6"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3284), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("2e4bbb50-289f-483d-8d3a-6abd7c88d69b"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3258), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("5b33d480-6a78-477c-8b1c-e4ca2075f179"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3305), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("77334f15-8913-4db3-871d-eb78ea87d4cb"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3277), new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("da1816ed-a2cf-470e-8af2-6368bff30266"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3299), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 14, 11, 19, 40, 769, DateTimeKind.Utc).AddTicks(2932), "$2a$11$CS1JAanGZREygC/a.8fb5OxBCelw2yaQetNrz3U4np12m85.z1Cf6", new DateTime(2025, 8, 14, 11, 19, 40, 769, DateTimeKind.Utc).AddTicks(2955) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 14, 11, 19, 41, 704, DateTimeKind.Utc).AddTicks(6873), "$2a$11$BlQnCqDtUC8zQoHBP2nhcOCvoh8BKXQhZgY3H2hZfgIx.al/vjSEi", new DateTime(2025, 10, 14, 11, 19, 41, 704, DateTimeKind.Utc).AddTicks(6891) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 11, 19, 42, 672, DateTimeKind.Utc).AddTicks(9114), "$2a$11$W8UcI.I/MEL22AgChGgn8eriHBFFEhBWeLT7y5nY9SDM55oNd5IYq", new DateTime(2025, 12, 14, 11, 19, 42, 672, DateTimeKind.Utc).AddTicks(9132) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 11, 19, 43, 724, DateTimeKind.Utc).AddTicks(6298), "$2a$11$2SLfeQkc16xVREiCvVIMYeCQosi13EBXnHC/UyigGfmsHq.LLZxay", new DateTime(2026, 2, 14, 11, 19, 43, 724, DateTimeKind.Utc).AddTicks(6592) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 11, 19, 45, 696, DateTimeKind.Utc).AddTicks(6510), "$2a$11$WZ9E4dZgSohvc/nvkFf0ieD7rLbUkjxfIhZbivxQKY8rBprJterKe", new DateTime(2026, 2, 14, 11, 19, 45, 696, DateTimeKind.Utc).AddTicks(6531) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 11, 19, 47, 666, DateTimeKind.Utc).AddTicks(3648), "$2a$11$6/YoxvTU.g1.kN5A5NKjn.2zFNvYAfAvoBTf5WPhEqBMQFFXMxva2", new DateTime(2026, 3, 14, 11, 19, 47, 666, DateTimeKind.Utc).AddTicks(3718) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 11, 19, 49, 107, DateTimeKind.Utc).AddTicks(3412), "$2a$11$vgDWZMNgjhA6twHLtnkUMOx8UHVNqloP2W/29hKK4J1yqeRL9zNim", new DateTime(2026, 3, 14, 11, 19, 49, 107, DateTimeKind.Utc).AddTicks(3443) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 14, 11, 19, 50, 591, DateTimeKind.Utc).AddTicks(9858), "$2a$11$lqbgsvujaPqGRUPUT6FKRuBcP3nL73MUJGdUV6hNi37nOra.fAJEq", new DateTime(2026, 4, 14, 11, 19, 50, 591, DateTimeKind.Utc).AddTicks(9893) });
        }
    }
}
