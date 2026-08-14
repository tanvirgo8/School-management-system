using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxMarks = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAssignments_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherAssignments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherAssignments_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Marks = table.Column<int>(type: "integer", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5225), "Grade 6", true, "Class 6" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5239), "Grade 7", true, "Class 7" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5243), "Grade 10 - Senior Secondary", true, "Class 10" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "MATH101", new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5477), "Core mathematics", true, "Mathematics" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "ENG101", new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5481), "English language & literature", true, "English" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "PHY101", new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5505), "General physics", true, "Physics" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "CHEM101", new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5508), "General chemistry", true, "Chemistry" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "ICT101", new DateTime(2026, 8, 14, 10, 27, 43, 459, DateTimeKind.Utc).AddTicks(5511), "Information & Communications Technology", true, "ICT" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 8, 14, 10, 27, 43, 673, DateTimeKind.Utc).AddTicks(7465), "admin@school.com", "System Administrator", true, "$2a$11$Rb1oNoeZU.ZTH.XTAF/6IO83GL5Dz/RCGDXR9J1xWdTfddqP6hHVa", "+1-555-0100", 0, new DateTime(2025, 8, 14, 10, 27, 43, 673, DateTimeKind.Utc).AddTicks(7482) },
                    { new Guid("30000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 10, 14, 10, 27, 43, 945, DateTimeKind.Utc).AddTicks(336), "teacher@school.com", "John Smith", true, "$2a$11$oyM40K.jHc4jgQ5GQ7az..2z3.1O3G5TVAfiBnZt7r5dv/xBQfkz6", "+1-555-0101", 1, new DateTime(2025, 10, 14, 10, 27, 43, 945, DateTimeKind.Utc).AddTicks(349) },
                    { new Guid("30000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 12, 14, 10, 27, 44, 225, DateTimeKind.Utc).AddTicks(2659), "sarah.johnson@school.com", "Sarah Johnson", true, "$2a$11$c/nf3Z0QgxGhGDGuAhRG3e.3kuZKgwe6pAo0y9gM4TOrGplC1X2z.", "+1-555-0102", 1, new DateTime(2025, 12, 14, 10, 27, 44, 225, DateTimeKind.Utc).AddTicks(2676) }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 11, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6519), new DateTime(2026, 8, 21, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6503), "Solve the following quadratic equations and show all working steps. Problems cover factoring, completing the square, and the quadratic formula.", 100, 1, new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002"), "Algebra Basics - Quadratic Equations", new DateTime(2026, 8, 11, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6520) },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6528), new DateTime(2026, 8, 19, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6527), "Write a 500-word argumentative essay on the topic: 'Technology has more benefits than drawbacks in modern education.' Support your argument with evidence and examples.", 50, 1, new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000003"), "Essay Writing - Argumentative Essay", new DateTime(2026, 8, 9, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6529) },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 7, 30, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6535), new DateTime(2026, 8, 12, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6534), "Complete the problem set on Newton's three laws of motion. Include free-body diagrams where applicable.", 80, 2, new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000002"), "Newton's Laws of Motion - Problem Set", new DateTime(2026, 7, 30, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6536) },
                    { new Guid("40000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6542), new DateTime(2026, 8, 24, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6541), "Complete exercises on converting fractions to decimals, adding fractions with different denominators, and word problems.", 60, 1, new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000002"), "Fractions and Decimals", new DateTime(2026, 8, 13, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6543) },
                    { new Guid("40000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6549), new DateTime(2026, 8, 28, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6548), "This assignment is in draft. Content to be finalized.", 70, 0, new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000003"), "Introduction to Chemical Bonding", new DateTime(2026, 8, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(6550) }
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 14, 10, 27, 44, 479, DateTimeKind.Utc).AddTicks(9256), "student@school.com", "Alice Brown", true, "$2a$11$pfftifRGuGSmOgDjmsKC4OgQabDvx498.cIuZMeqMb5kryFf/cpSW", "+1-555-0201", 2, new DateTime(2026, 2, 14, 10, 27, 44, 479, DateTimeKind.Utc).AddTicks(9274) },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 14, 10, 27, 44, 768, DateTimeKind.Utc).AddTicks(2932), "bob.davis@school.com", "Bob Davis", true, "$2a$11$oxrXAiW.A7V9yPRtVotF4.50uaiqf4FtvpdyBk0xurWDBQhTurOgS", "+1-555-0202", 2, new DateTime(2026, 2, 14, 10, 27, 44, 768, DateTimeKind.Utc).AddTicks(2952) },
                    { new Guid("30000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 3, 14, 10, 27, 45, 77, DateTimeKind.Utc).AddTicks(6113), "carol.white@school.com", "Carol White", true, "$2a$11$ZEwX8KLW9MGzYOmcNfsGB.2OOnRzKin25fdRA26rgL8cHWUUx76vO", "+1-555-0203", 2, new DateTime(2026, 3, 14, 10, 27, 45, 77, DateTimeKind.Utc).AddTicks(6219) },
                    { new Guid("30000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 3, 14, 10, 27, 45, 339, DateTimeKind.Utc).AddTicks(83), "david.martinez@school.com", "David Martinez", true, "$2a$11$mhtY4F7TauVY6y/E1bqrQ.6MJMM57DRMi4e0xxlIb1AwCMtZAEKRi", "+1-555-0204", 2, new DateTime(2026, 3, 14, 10, 27, 45, 339, DateTimeKind.Utc).AddTicks(94) },
                    { new Guid("30000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 4, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(4694), "eva.garcia@school.com", "Eva Garcia", true, "$2a$11$QW8lczsgVI6rj3EFEfLZAOWBYZt0cRjuLQ2b.UwxmAcNcFqSIvnlS", "+1-555-0205", 2, new DateTime(2026, 4, 14, 10, 27, 45, 576, DateTimeKind.Utc).AddTicks(4782) }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ClassId",
                table: "Assignments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SubjectId",
                table: "Assignments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TeacherId",
                table: "Assignments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_ClassId",
                table: "TeacherAssignments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_SubjectId",
                table: "TeacherAssignments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_TeacherId",
                table: "TeacherAssignments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassId",
                table: "Users",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "TeacherAssignments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
