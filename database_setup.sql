CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Classes" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "IsActive" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Classes" PRIMARY KEY ("Id")
);

CREATE TABLE "Subjects" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "Description" text,
    "IsActive" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Subjects" PRIMARY KEY ("Id")
);

CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "FullName" text NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "Role" integer NOT NULL,
    "Phone" text,
    "IsActive" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    "ClassId" uuid,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Users_Classes_ClassId" FOREIGN KEY ("ClassId") REFERENCES "Classes" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Assignments" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "TeacherId" uuid NOT NULL,
    "ClassId" uuid NOT NULL,
    "SubjectId" uuid NOT NULL,
    "Deadline" timestamp with time zone NOT NULL,
    "MaxMarks" integer NOT NULL,
    "Status" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Assignments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Assignments_Classes_ClassId" FOREIGN KEY ("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Assignments_Subjects_SubjectId" FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Assignments_Users_TeacherId" FOREIGN KEY ("TeacherId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "TeacherAssignments" (
    "Id" uuid NOT NULL,
    "TeacherId" uuid NOT NULL,
    "ClassId" uuid NOT NULL,
    "SubjectId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_TeacherAssignments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TeacherAssignments_Classes_ClassId" FOREIGN KEY ("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TeacherAssignments_Subjects_SubjectId" FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TeacherAssignments_Users_TeacherId" FOREIGN KEY ("TeacherId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Submissions" (
    "Id" uuid NOT NULL,
    "AssignmentId" uuid NOT NULL,
    "StudentId" uuid NOT NULL,
    "Answer" text NOT NULL,
    "SubmittedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    "Marks" integer,
    "Feedback" text,
    "Status" integer NOT NULL,
    CONSTRAINT "PK_Submissions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Submissions_Assignments_AssignmentId" FOREIGN KEY ("AssignmentId") REFERENCES "Assignments" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Submissions_Users_StudentId" FOREIGN KEY ("StudentId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

INSERT INTO "Classes" ("Id", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('10000000-0000-0000-0000-000000000001', TIMESTAMPTZ '2026-02-14T10:27:43.459522Z', 'Grade 6', TRUE, 'Class 6');
INSERT INTO "Classes" ("Id", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-02-14T10:27:43.459523Z', 'Grade 7', TRUE, 'Class 7');
INSERT INTO "Classes" ("Id", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-02-14T10:27:43.459524Z', 'Grade 10 - Senior Secondary', TRUE, 'Class 10');

INSERT INTO "Subjects" ("Id", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000001', 'MATH101', TIMESTAMPTZ '2026-08-14T10:27:43.459547Z', 'Core mathematics', TRUE, 'Mathematics');
INSERT INTO "Subjects" ("Id", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000002', 'ENG101', TIMESTAMPTZ '2026-08-14T10:27:43.459548Z', 'English language & literature', TRUE, 'English');
INSERT INTO "Subjects" ("Id", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000003', 'PHY101', TIMESTAMPTZ '2026-08-14T10:27:43.45955Z', 'General physics', TRUE, 'Physics');
INSERT INTO "Subjects" ("Id", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000004', 'CHEM101', TIMESTAMPTZ '2026-08-14T10:27:43.45955Z', 'General chemistry', TRUE, 'Chemistry');
INSERT INTO "Subjects" ("Id", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000005', 'ICT101', TIMESTAMPTZ '2026-08-14T10:27:43.459551Z', 'Information & Communications Technology', TRUE, 'ICT');

INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000001', NULL, TIMESTAMPTZ '2025-08-14T10:27:43.673746Z', 'admin@school.com', 'System Administrator', TRUE, '$2a$11$Rb1oNoeZU.ZTH.XTAF/6IO83GL5Dz/RCGDXR9J1xWdTfddqP6hHVa', '+1-555-0100', 0, TIMESTAMPTZ '2025-08-14T10:27:43.673748Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000002', NULL, TIMESTAMPTZ '2025-10-14T10:27:43.945033Z', 'teacher@school.com', 'John Smith', TRUE, '$2a$11$oyM40K.jHc4jgQ5GQ7az..2z3.1O3G5TVAfiBnZt7r5dv/xBQfkz6', '+1-555-0101', 1, TIMESTAMPTZ '2025-10-14T10:27:43.945034Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000003', NULL, TIMESTAMPTZ '2025-12-14T10:27:44.225265Z', 'sarah.johnson@school.com', 'Sarah Johnson', TRUE, '$2a$11$c/nf3Z0QgxGhGDGuAhRG3e.3kuZKgwe6pAo0y9gM4TOrGplC1X2z.', '+1-555-0102', 1, TIMESTAMPTZ '2025-12-14T10:27:44.225267Z');

INSERT INTO "Assignments" ("Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt")
VALUES ('40000000-0000-0000-0000-000000000001', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-11T10:27:45.576651Z', TIMESTAMPTZ '2026-08-21T10:27:45.57665Z', 'Solve the following quadratic equations and show all working steps. Problems cover factoring, completing the square, and the quadratic formula.', 100, 1, '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000002', 'Algebra Basics - Quadratic Equations', TIMESTAMPTZ '2026-08-11T10:27:45.576652Z');
INSERT INTO "Assignments" ("Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt")
VALUES ('40000000-0000-0000-0000-000000000002', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-09T10:27:45.576652Z', TIMESTAMPTZ '2026-08-19T10:27:45.576652Z', 'Write a 500-word argumentative essay on the topic: ''Technology has more benefits than drawbacks in modern education.'' Support your argument with evidence and examples.', 50, 1, '20000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000003', 'Essay Writing - Argumentative Essay', TIMESTAMPTZ '2026-08-09T10:27:45.576652Z');
INSERT INTO "Assignments" ("Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt")
VALUES ('40000000-0000-0000-0000-000000000003', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-07-30T10:27:45.576653Z', TIMESTAMPTZ '2026-08-12T10:27:45.576653Z', 'Complete the problem set on Newton''s three laws of motion. Include free-body diagrams where applicable.', 80, 2, '20000000-0000-0000-0000-000000000003', '30000000-0000-0000-0000-000000000002', 'Newton''s Laws of Motion - Problem Set', TIMESTAMPTZ '2026-07-30T10:27:45.576653Z');
INSERT INTO "Assignments" ("Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt")
VALUES ('40000000-0000-0000-0000-000000000004', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-13T10:27:45.576654Z', TIMESTAMPTZ '2026-08-24T10:27:45.576654Z', 'Complete exercises on converting fractions to decimals, adding fractions with different denominators, and word problems.', 60, 1, '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000002', 'Fractions and Decimals', TIMESTAMPTZ '2026-08-13T10:27:45.576654Z');
INSERT INTO "Assignments" ("Id", "ClassId", "CreatedAt", "Deadline", "Description", "MaxMarks", "Status", "SubjectId", "TeacherId", "Title", "UpdatedAt")
VALUES ('40000000-0000-0000-0000-000000000005', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-14T10:27:45.576654Z', TIMESTAMPTZ '2026-08-28T10:27:45.576654Z', 'This assignment is in draft. Content to be finalized.', 70, 0, '20000000-0000-0000-0000-000000000004', '30000000-0000-0000-0000-000000000003', 'Introduction to Chemical Bonding', TIMESTAMPTZ '2026-08-14T10:27:45.576655Z');

INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('307acc63-062e-4d6b-b0ee-caa1ac54b358', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T10:27:45.576635Z', '20000000-0000-0000-0000-000000000003', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('326a9e41-d915-4bbe-a3d0-168d541df3d2', '10000000-0000-0000-0000-000000000001', TIMESTAMPTZ '2026-08-14T10:27:45.576637Z', '20000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000003');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('32adbf21-ed8c-4e47-8e88-9c2a3b67f565', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T10:27:45.576636Z', '20000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000003');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('5e797388-3359-4454-9c30-f23001cef250', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-14T10:27:45.576636Z', '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('78705af5-27a1-488f-82b6-33e0ca7558a1', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T10:27:45.576633Z', '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('a9d985c1-b44a-4b8f-9a8f-213c93c64afd', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-14T10:27:45.576637Z', '20000000-0000-0000-0000-000000000004', '30000000-0000-0000-0000-000000000003');

INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000004', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-02-14T10:27:44.479925Z', 'student@school.com', 'Alice Brown', TRUE, '$2a$11$pfftifRGuGSmOgDjmsKC4OgQabDvx498.cIuZMeqMb5kryFf/cpSW', '+1-555-0201', 2, TIMESTAMPTZ '2026-02-14T10:27:44.479927Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000005', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-02-14T10:27:44.768293Z', 'bob.davis@school.com', 'Bob Davis', TRUE, '$2a$11$oxrXAiW.A7V9yPRtVotF4.50uaiqf4FtvpdyBk0xurWDBQhTurOgS', '+1-555-0202', 2, TIMESTAMPTZ '2026-02-14T10:27:44.768295Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000006', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-03-14T10:27:45.077611Z', 'carol.white@school.com', 'Carol White', TRUE, '$2a$11$ZEwX8KLW9MGzYOmcNfsGB.2OOnRzKin25fdRA26rgL8cHWUUx76vO', '+1-555-0203', 2, TIMESTAMPTZ '2026-03-14T10:27:45.077621Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000007', '10000000-0000-0000-0000-000000000001', TIMESTAMPTZ '2026-03-14T10:27:45.339008Z', 'david.martinez@school.com', 'David Martinez', TRUE, '$2a$11$mhtY4F7TauVY6y/E1bqrQ.6MJMM57DRMi4e0xxlIb1AwCMtZAEKRi', '+1-555-0204', 2, TIMESTAMPTZ '2026-03-14T10:27:45.339009Z');
INSERT INTO "Users" ("Id", "ClassId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt")
VALUES ('30000000-0000-0000-0000-000000000008', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-04-14T10:27:45.576469Z', 'eva.garcia@school.com', 'Eva Garcia', TRUE, '$2a$11$QW8lczsgVI6rj3EFEfLZAOWBYZt0cRjuLQ2b.UwxmAcNcFqSIvnlS', '+1-555-0205', 2, TIMESTAMPTZ '2026-04-14T10:27:45.576478Z');

INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('0c33b8eb-154d-4f3d-8ba4-0906f9eb4f48', 'Newton''s First Law: An object at rest stays at rest... [detailed answers]', '40000000-0000-0000-0000-000000000003', 'Good understanding of the first two laws. Work on applying the third law in complex systems.', 72, 3, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-09T10:27:45.576669Z', TIMESTAMPTZ '2026-08-09T10:27:45.576669Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('4f11890c-1c95-464b-85c5-41f9a4097691', '1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3
2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3
3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3', '40000000-0000-0000-0000-000000000001', 'Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.', 88, 3, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-12T10:27:45.576665Z', TIMESTAMPTZ '2026-08-12T10:27:45.576665Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('9991fe79-48f2-4e7e-b708-7b25cc906fb0', 'Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...', '40000000-0000-0000-0000-000000000002', NULL, NULL, 1, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-14T04:27:45.576668Z', TIMESTAMPTZ '2026-08-14T04:27:45.576668Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('a5d04283-c397-4aed-ba01-53d0ec7b3ecc', '1) x = 2 or x = 3
2) x = -0.5 or x = -3
3) x = 3 or x = -3
(Solved using quadratic formula for all)', '40000000-0000-0000-0000-000000000001', NULL, NULL, 2, '30000000-0000-0000-0000-000000000005', TIMESTAMPTZ '2026-08-13T10:27:45.576666Z', TIMESTAMPTZ '2026-08-13T10:27:45.576666Z');

CREATE INDEX "IX_Assignments_ClassId" ON "Assignments" ("ClassId");

CREATE INDEX "IX_Assignments_SubjectId" ON "Assignments" ("SubjectId");

CREATE INDEX "IX_Assignments_TeacherId" ON "Assignments" ("TeacherId");

CREATE INDEX "IX_Submissions_AssignmentId" ON "Submissions" ("AssignmentId");

CREATE INDEX "IX_Submissions_StudentId" ON "Submissions" ("StudentId");

CREATE INDEX "IX_TeacherAssignments_ClassId" ON "TeacherAssignments" ("ClassId");

CREATE INDEX "IX_TeacherAssignments_SubjectId" ON "TeacherAssignments" ("SubjectId");

CREATE INDEX "IX_TeacherAssignments_TeacherId" ON "TeacherAssignments" ("TeacherId");

CREATE INDEX "IX_Users_ClassId" ON "Users" ("ClassId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260814102746_InitialCreate', '8.0.0');

COMMIT;

START TRANSACTION;

DELETE FROM "Submissions"
WHERE "Id" = '0c33b8eb-154d-4f3d-8ba4-0906f9eb4f48';

DELETE FROM "Submissions"
WHERE "Id" = '4f11890c-1c95-464b-85c5-41f9a4097691';

DELETE FROM "Submissions"
WHERE "Id" = '9991fe79-48f2-4e7e-b708-7b25cc906fb0';

DELETE FROM "Submissions"
WHERE "Id" = 'a5d04283-c397-4aed-ba01-53d0ec7b3ecc';

DELETE FROM "TeacherAssignments"
WHERE "Id" = '307acc63-062e-4d6b-b0ee-caa1ac54b358';

DELETE FROM "TeacherAssignments"
WHERE "Id" = '326a9e41-d915-4bbe-a3d0-168d541df3d2';

DELETE FROM "TeacherAssignments"
WHERE "Id" = '32adbf21-ed8c-4e47-8e88-9c2a3b67f565';

DELETE FROM "TeacherAssignments"
WHERE "Id" = '5e797388-3359-4454-9c30-f23001cef250';

DELETE FROM "TeacherAssignments"
WHERE "Id" = '78705af5-27a1-488f-82b6-33e0ca7558a1';

DELETE FROM "TeacherAssignments"
WHERE "Id" = 'a9d985c1-b44a-4b8f-9a8f-213c93c64afd';

ALTER TABLE "Submissions" ADD "PdfUrl" text;

ALTER TABLE "Subjects" ADD "ClassId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE "Classes" ADD "TuitionFee" numeric NOT NULL DEFAULT 0.0;

ALTER TABLE "Assignments" ADD "PdfUrl" text;

INSERT INTO "Subjects" ("Id", "ClassId", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000006', '10000000-0000-0000-0000-000000000002', 'MATH101', TIMESTAMPTZ '2026-08-14T11:19:39.937856Z', 'Core mathematics', TRUE, 'Mathematics');
INSERT INTO "Subjects" ("Id", "ClassId", "Code", "CreatedAt", "Description", "IsActive", "Name")
VALUES ('20000000-0000-0000-0000-000000000007', '10000000-0000-0000-0000-000000000001', 'ENG101', TIMESTAMPTZ '2026-08-14T11:19:39.93786Z', 'English language & literature', TRUE, 'English');

INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('17232891-7f1b-4967-a0ef-b62e0b7cebaf', '1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3
2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3
3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3', '40000000-0000-0000-0000-000000000001', 'Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.', 88, NULL, 3, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-12T11:19:50.592387Z', TIMESTAMPTZ '2026-08-12T11:19:50.592387Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('7bbd1ef4-9432-4ea2-ab29-b6fda8e6ad11', '1) x = 2 or x = 3
2) x = -0.5 or x = -3
3) x = 3 or x = -3
(Solved using quadratic formula for all)', '40000000-0000-0000-0000-000000000001', NULL, NULL, NULL, 2, '30000000-0000-0000-0000-000000000005', TIMESTAMPTZ '2026-08-13T11:19:50.592388Z', TIMESTAMPTZ '2026-08-13T11:19:50.592388Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('a1ad2589-6f12-40a3-acc8-a51262255649', 'Newton''s First Law: An object at rest stays at rest... [detailed answers]', '40000000-0000-0000-0000-000000000003', 'Good understanding of the first two laws. Work on applying the third law in complex systems.', 72, NULL, 3, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-09T11:19:50.592393Z', TIMESTAMPTZ '2026-08-09T11:19:50.592393Z');
INSERT INTO "Submissions" ("Id", "Answer", "AssignmentId", "Feedback", "Marks", "PdfUrl", "Status", "StudentId", "SubmittedAt", "UpdatedAt")
VALUES ('cd6c1df0-a612-4bac-ba86-6804a24a41fd', 'Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...', '40000000-0000-0000-0000-000000000002', NULL, NULL, NULL, 1, '30000000-0000-0000-0000-000000000004', TIMESTAMPTZ '2026-08-14T05:19:50.592391Z', TIMESTAMPTZ '2026-08-14T05:19:50.592391Z');

INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('047cbd26-036e-4dcf-b18a-26d1a3f0abb7', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T11:19:50.592326Z', '20000000-0000-0000-0000-000000000003', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('11201fde-d423-4751-8d04-9bef0f11fda6', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T11:19:50.592328Z', '20000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000003');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('2e4bbb50-289f-483d-8d3a-6abd7c88d69b', '10000000-0000-0000-0000-000000000003', TIMESTAMPTZ '2026-08-14T11:19:50.592325Z', '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('5b33d480-6a78-477c-8b1c-e4ca2075f179', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-14T11:19:50.59233Z', '20000000-0000-0000-0000-000000000004', '30000000-0000-0000-0000-000000000003');

UPDATE "Assignments" SET "CreatedAt" = TIMESTAMPTZ '2026-08-11T11:19:50.592368Z', "Deadline" = TIMESTAMPTZ '2026-08-21T11:19:50.592366Z', "PdfUrl" = NULL, "UpdatedAt" = TIMESTAMPTZ '2026-08-11T11:19:50.592369Z'
WHERE "Id" = '40000000-0000-0000-0000-000000000001';

UPDATE "Assignments" SET "CreatedAt" = TIMESTAMPTZ '2026-08-09T11:19:50.59237Z', "Deadline" = TIMESTAMPTZ '2026-08-19T11:19:50.59237Z', "PdfUrl" = NULL, "UpdatedAt" = TIMESTAMPTZ '2026-08-09T11:19:50.59237Z'
WHERE "Id" = '40000000-0000-0000-0000-000000000002';

UPDATE "Assignments" SET "CreatedAt" = TIMESTAMPTZ '2026-07-30T11:19:50.592371Z', "Deadline" = TIMESTAMPTZ '2026-08-12T11:19:50.592371Z', "PdfUrl" = NULL, "UpdatedAt" = TIMESTAMPTZ '2026-07-30T11:19:50.592371Z'
WHERE "Id" = '40000000-0000-0000-0000-000000000003';

UPDATE "Assignments" SET "CreatedAt" = TIMESTAMPTZ '2026-08-13T11:19:50.592372Z', "Deadline" = TIMESTAMPTZ '2026-08-24T11:19:50.592372Z', "PdfUrl" = NULL, "SubjectId" = '20000000-0000-0000-0000-000000000006', "UpdatedAt" = TIMESTAMPTZ '2026-08-13T11:19:50.592372Z'
WHERE "Id" = '40000000-0000-0000-0000-000000000004';

UPDATE "Assignments" SET "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:50.592374Z', "Deadline" = TIMESTAMPTZ '2026-08-28T11:19:50.592374Z', "PdfUrl" = NULL, "UpdatedAt" = TIMESTAMPTZ '2026-08-14T11:19:50.592374Z'
WHERE "Id" = '40000000-0000-0000-0000-000000000005';

UPDATE "Classes" SET "CreatedAt" = TIMESTAMPTZ '2026-02-14T11:19:39.937779Z', "TuitionFee" = 1500.0
WHERE "Id" = '10000000-0000-0000-0000-000000000001';

UPDATE "Classes" SET "CreatedAt" = TIMESTAMPTZ '2026-02-14T11:19:39.937782Z', "TuitionFee" = 1800.0
WHERE "Id" = '10000000-0000-0000-0000-000000000002';

UPDATE "Classes" SET "CreatedAt" = TIMESTAMPTZ '2026-02-14T11:19:39.937783Z', "TuitionFee" = 2500.0
WHERE "Id" = '10000000-0000-0000-0000-000000000003';

UPDATE "Subjects" SET "ClassId" = '10000000-0000-0000-0000-000000000003', "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:39.937855Z'
WHERE "Id" = '20000000-0000-0000-0000-000000000001';

UPDATE "Subjects" SET "ClassId" = '10000000-0000-0000-0000-000000000003', "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:39.937857Z'
WHERE "Id" = '20000000-0000-0000-0000-000000000002';

UPDATE "Subjects" SET "ClassId" = '10000000-0000-0000-0000-000000000003', "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:39.937861Z'
WHERE "Id" = '20000000-0000-0000-0000-000000000003';

UPDATE "Subjects" SET "ClassId" = '10000000-0000-0000-0000-000000000002', "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:39.937861Z'
WHERE "Id" = '20000000-0000-0000-0000-000000000004';

UPDATE "Subjects" SET "ClassId" = '10000000-0000-0000-0000-000000000003', "CreatedAt" = TIMESTAMPTZ '2026-08-14T11:19:39.937862Z'
WHERE "Id" = '20000000-0000-0000-0000-000000000005';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2025-08-14T11:19:40.769293Z', "PasswordHash" = '$2a$11$CS1JAanGZREygC/a.8fb5OxBCelw2yaQetNrz3U4np12m85.z1Cf6', "UpdatedAt" = TIMESTAMPTZ '2025-08-14T11:19:40.769295Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000001';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2025-10-14T11:19:41.704687Z', "PasswordHash" = '$2a$11$BlQnCqDtUC8zQoHBP2nhcOCvoh8BKXQhZgY3H2hZfgIx.al/vjSEi', "UpdatedAt" = TIMESTAMPTZ '2025-10-14T11:19:41.704689Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000002';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2025-12-14T11:19:42.672911Z', "PasswordHash" = '$2a$11$W8UcI.I/MEL22AgChGgn8eriHBFFEhBWeLT7y5nY9SDM55oNd5IYq', "UpdatedAt" = TIMESTAMPTZ '2025-12-14T11:19:42.672913Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000003';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2026-02-14T11:19:43.724629Z', "PasswordHash" = '$2a$11$2SLfeQkc16xVREiCvVIMYeCQosi13EBXnHC/UyigGfmsHq.LLZxay', "UpdatedAt" = TIMESTAMPTZ '2026-02-14T11:19:43.724659Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000004';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2026-02-14T11:19:45.696651Z', "PasswordHash" = '$2a$11$WZ9E4dZgSohvc/nvkFf0ieD7rLbUkjxfIhZbivxQKY8rBprJterKe', "UpdatedAt" = TIMESTAMPTZ '2026-02-14T11:19:45.696653Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000005';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2026-03-14T11:19:47.666364Z', "PasswordHash" = '$2a$11$6/YoxvTU.g1.kN5A5NKjn.2zFNvYAfAvoBTf5WPhEqBMQFFXMxva2', "UpdatedAt" = TIMESTAMPTZ '2026-03-14T11:19:47.666371Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000006';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2026-03-14T11:19:49.107341Z', "PasswordHash" = '$2a$11$vgDWZMNgjhA6twHLtnkUMOx8UHVNqloP2W/29hKK4J1yqeRL9zNim', "UpdatedAt" = TIMESTAMPTZ '2026-03-14T11:19:49.107344Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000007';

UPDATE "Users" SET "CreatedAt" = TIMESTAMPTZ '2026-04-14T11:19:50.591985Z', "PasswordHash" = '$2a$11$lqbgsvujaPqGRUPUT6FKRuBcP3nL73MUJGdUV6hNi37nOra.fAJEq', "UpdatedAt" = TIMESTAMPTZ '2026-04-14T11:19:50.591989Z'
WHERE "Id" = '30000000-0000-0000-0000-000000000008';

INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('77334f15-8913-4db3-871d-eb78ea87d4cb', '10000000-0000-0000-0000-000000000002', TIMESTAMPTZ '2026-08-14T11:19:50.592327Z', '20000000-0000-0000-0000-000000000006', '30000000-0000-0000-0000-000000000002');
INSERT INTO "TeacherAssignments" ("Id", "ClassId", "CreatedAt", "SubjectId", "TeacherId")
VALUES ('da1816ed-a2cf-470e-8af2-6368bff30266', '10000000-0000-0000-0000-000000000001', TIMESTAMPTZ '2026-08-14T11:19:50.592329Z', '20000000-0000-0000-0000-000000000007', '30000000-0000-0000-0000-000000000003');

CREATE INDEX "IX_Subjects_ClassId" ON "Subjects" ("ClassId");

ALTER TABLE "Subjects" ADD CONSTRAINT "FK_Subjects_Classes_ClassId" FOREIGN KEY ("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260814111955_AddTuitionFeeAndPdf', '8.0.0');

COMMIT;

