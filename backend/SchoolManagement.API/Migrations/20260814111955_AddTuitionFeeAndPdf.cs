using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTuitionFeeAndPdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("0c33b8eb-154d-4f3d-8ba4-0906f9eb4f48"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("4f11890c-1c95-464b-85c5-41f9a4097691"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("9991fe79-48f2-4e7e-b708-7b25cc906fb0"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("a5d04283-c397-4aed-ba01-53d0ec7b3ecc"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("307acc63-062e-4d6b-b0ee-caa1ac54b358"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("326a9e41-d915-4bbe-a3d0-168d541df3d2"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("32adbf21-ed8c-4e47-8e88-9c2a3b67f565"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("5e797388-3359-4454-9c30-f23001cef250"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("78705af5-27a1-488f-82b6-33e0ca7558a1"));

            migrationBuilder.DeleteData(
                table: "TeacherAssignments",
                keyColumn: "Id",
                keyValue: new Guid("a9d985c1-b44a-4b8f-9a8f-213c93c64afd"));

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Submissions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "Subjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TuitionFee",
                table: "Classes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "ClassId", "Code", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000002"), "MATH101", new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8563), "Core mathematics", true, "Mathematics" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000001"), "ENG101", new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8605), "English language & literature", true, "English" }
                });

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
                    { new Guid("5b33d480-6a78-477c-8b1c-e4ca2075f179"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3305), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Deadline", "PdfUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 11, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3689), new DateTime(2026, 8, 21, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3669), null, new DateTime(2026, 8, 11, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3690) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Deadline", "PdfUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3706), new DateTime(2026, 8, 19, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3704), null, new DateTime(2026, 8, 9, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3707) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Deadline", "PdfUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 30, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3715), new DateTime(2026, 8, 12, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3713), null, new DateTime(2026, 7, 30, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3716) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Deadline", "PdfUrl", "SubjectId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3726), new DateTime(2026, 8, 24, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3723), null, new Guid("20000000-0000-0000-0000-000000000006"), new DateTime(2026, 8, 13, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Deadline", "PdfUrl", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3743), new DateTime(2026, 8, 28, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3741), null, new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3744) });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "TuitionFee" },
                values: new object[] { new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7794), 1500m });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "TuitionFee" },
                values: new object[] { new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7821), 1800m });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "TuitionFee" },
                values: new object[] { new DateTime(2026, 2, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(7831), 2500m });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                columns: new[] { "ClassId", "CreatedAt" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8554) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                columns: new[] { "ClassId", "CreatedAt" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8571) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                columns: new[] { "ClassId", "CreatedAt" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8612) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                columns: new[] { "ClassId", "CreatedAt" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8618) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                columns: new[] { "ClassId", "CreatedAt" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 11, 19, 39, 937, DateTimeKind.Utc).AddTicks(8625) });

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

            migrationBuilder.InsertData(
                table: "TeacherAssignments",
                columns: new[] { "Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("77334f15-8913-4db3-871d-eb78ea87d4cb"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3277), new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("da1816ed-a2cf-470e-8af2-6368bff30266"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 14, 11, 19, 50, 592, DateTimeKind.Utc).AddTicks(3299), new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassId",
                table: "Subjects",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classes_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classes_ClassId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassId",
                table: "Subjects");

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

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"));

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TuitionFee",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Assignments");

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 11, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6519), new DateTime(2026, 8, 21, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6503), new DateTime(2026, 8, 11, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6528), new DateTime(2026, 8, 19, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6527), new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6529) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 30, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6535), new DateTime(2026, 8, 12, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6534), new DateTime(2026, 7, 30, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6536) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Deadline", "SubjectId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6542), new DateTime(2026, 8, 24, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6541), new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6543) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Deadline", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6549), new DateTime(2026, 8, 28, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6548), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5225));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5505));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5511));

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "Answer", "AssignmentId", "Feedback", "Marks", "Status", "StudentId", "SubmittedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0c33b8eb-154d-4f3d-8ba4-0906f9eb4f48"), "Newton's First Law: An object at rest stays at rest... [detailed answers]", new Guid("40000000-0000-0000-0000-000000000003"), "Good understanding of the first two laws. Work on applying the third law in complex systems.", 72, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6690), new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6690) },
                    { new Guid("4f11890c-1c95-464b-85c5-41f9a4097691"), "1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3\n2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3\n3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3", new Guid("40000000-0000-0000-0000-000000000001"), "Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.", 88, 3, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 12, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6654), new DateTime(2026, 8, 12, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6655) },
                    { new Guid("9991fe79-48f2-4e7e-b708-7b25cc906fb0"), "Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...", new Guid("40000000-0000-0000-0000-000000000002"), null, null, 1, new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2026, 8, 14, 4, 27, 45, 576, DateTimeKind.Utc).AddTicks(6682), new DateTime(2026, 8, 14, 4, 27, 45, 576, DateTimeKind.Utc).AddTicks(6685) },
                    { new Guid("a5d04283-c397-4aed-ba01-53d0ec7b3ecc"), "1) x = 2 or x = 3\n2) x = -0.5 or x = -3\n3) x = 3 or x = -3\n(Solved using quadratic formula for all)", new Guid("40000000-0000-0000-0000-000000000001"), null, null, 2, new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6667), new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6668) }
                });

            migrationBuilder.InsertData(
                table: "TeacherAssignments",
                columns: new[] { "Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("307acc63-062e-4d6b-b0ee-caa1ac54b358"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6359), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("326a9e41-d915-4bbe-a3d0-168d541df3d2"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6371), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("32adbf21-ed8c-4e47-8e88-9c2a3b67f565"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6366), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("5e797388-3359-4454-9c30-f23001cef250"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6363), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("78705af5-27a1-488f-82b6-33e0ca7558a1"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6334), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("a9d985c1-b44a-4b8f-9a8f-213c93c64afd"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6378), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 14, 10, 27, 43, 673, DateTimeKind.Utc).AddTicks(7465), "$2a$11$Rb1oNoeZU.ZTH.XTAF/6IO83GL5Dz/RCGDXR9J1xWdTfddqP6hHVa", new DateTime(2025, 8, 14, 10, 27, 43, 673, DateTimeKind.Utc).AddTicks(7482) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 14, 10, 27, 43, 945, DateTimeKind.Utc).AddTicks(336), "$2a$11$oyM40K.jHc4jgQ5GQ7az..2z3.1O3G5TVAfiBnZt7r5dv/xBQfkz6", new DateTime(2025, 10, 14, 10, 27, 43, 945, DateTimeKind.Utc).AddTicks(349) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 14, 10, 27, 44, 225, DateTimeKind.Utc).AddTicks(2659), "$2a$11$c/nf3Z0QgxGhGDGuAhRG3e.3kuZKgwe6pAo0y9gM4TOrGplC1X2z.", new DateTime(2025, 12, 14, 10, 27, 44, 225, DateTimeKind.Utc).AddTicks(2676) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 10, 27, 44, 479, DateTimeKind.Utc).AddTicks(9256), "$2a$11$pfftifRGuGSmOgDjmsKC4OgQabDvx498.cIuZMeqMb5kryFf/cpSW", new DateTime(2026, 2, 14, 10, 27, 44, 479, DateTimeKind.Utc).AddTicks(9274) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 14, 10, 27, 44, 768, DateTimeKind.Utc).AddTicks(2932), "$2a$11$oxrXAiW.A7V9yPRtVotF4.50uaiqf4FtvpdyBk0xurWDBQhTurOgS", new DateTime(2026, 2, 14, 10, 27, 44, 768, DateTimeKind.Utc).AddTicks(2952) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 10, 27, 45, 77, DateTimeKind.Utc).AddTicks(6113), "$2a$11$ZEwX8KLW9MGzYOmcNfsGB.2OOnRzKin25fdRA26rgL8cHWUUx76vO", new DateTime(2026, 3, 14, 10, 27, 45, 77, DateTimeKind.Utc).AddTicks(6219) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 14, 10, 27, 45, 339, DateTimeKind.Utc).AddTicks(83), "$2a$11$mhtY4F7TauVY6y/E1bqrQ.6MJMM57DRMi4e0xxlIb1AwCMtZAEKRi", new DateTime(2026, 3, 14, 10, 27, 45, 339, DateTimeKind.Utc).AddTicks(94) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(4694), "$2a$11$QW8lczsgVI6rj3EFEfLZAOWBYZt0cRjuLQ2b.UwxmAcNcFqSIvnlS", new DateTime(2026, 4, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(4782) });
        }
    }
}
