# School Management System

A comprehensive web application designed to manage school activities, including user registration, course and class enrollment, teacher assignments, assignment management, and submission grading. The project features a **Next.js (React + TypeScript)** frontend and an **ASP.NET Core Web API (C#)** backend powered by **PostgreSQL**.

---

## 🚀 Main Features

### 1. User & Role Management
*   **Three Predefined Roles:** Admin, Teacher, and Student.
*   **Authentication:** JWT (JSON Web Tokens) with secure password hashing using **BCrypt**.
*   **User Directory:** Register and manage profiles.

### 2. Administrator Panel
*   **Class Management:** Create, edit, and deactivate classes (e.g., Class 6, 7, 10).
*   **Subject Management:** Create subjects mapped class-wise.
*   **Teacher Assignment:** Map teachers to specific classes and subjects.
*   **Student Registry:** Assign students to classes.

### 3. Teacher Dashboard
*   **Assigned Courses:** View classes and subjects taught by the teacher.
*   **Assignment Management:** Create, publish, close, and edit assignments (includes titles, descriptions, due dates, and max marks).
*   **Grading Panel:** View submissions, award marks, and provide detailed written feedback.

### 4. Student Portal
*   **Academic Hub:** View classmate directories and class details.
*   **Assignment Tracker:** Track active, submitted, and graded assignments.
*   **Submissions:** Write and submit assignment answers directly.
*   **Grade Book:** View marks, status (Submitted, Under Review, Graded), and teacher feedback.

---

## 🛠️ Technology Stack

*   **Frontend:** Next.js (App Router), React, TypeScript, Axios, TailwindCSS (for sleek, modern UI).
*   **Backend:** ASP.NET Core 8 Web API, Entity Framework (EF) Core 8.
*   **Database:** PostgreSQL.
*   **Testing:** xUnit, Moq, Microsoft.EntityFrameworkCore.InMemory.

---

## 📂 Project Structure

```text
School-management-system/
├── backend/
│   ├── SchoolManagement.API/     # ASP.NET Core Web API project
│   │   ├── Controllers/          # RESTful Endpoints (Auth, Users, Classes, Subjects, etc.)
│   │   ├── Data/                 # ApplicationDbContext & Data Seeding
│   │   ├── DTOs/                 # Data Transfer Objects
│   │   ├── Models/               # EF Core Entities (User, Class, Subject, etc.)
│   │   ├── Services/             # Business Logic Layer (Auth, Classes, Assignments, etc.)
│   │   └── Program.cs            # API entry point & configuration
│   └── tests/
│       └── SchoolManagement.Tests/# Unit Test Project (xUnit & Moq)
├── frontend/
│   ├── app/                      # Next.js App Router Pages (Admin, Teacher, Student)
│   ├── components/               # Reusable UI Components
│   ├── services/                 # Frontend API Service integration
│   ├── lib/                      # Axios client configuration and local storage helpers
│   └── types/                    # TypeScript interfaces/types
├── database_setup.sql            # Schema and Seed data backup SQL script
├── .env.example                  # Template for environment configuration
└── README.md                     # Main project guide (this file)
```

---

## ⚙️ Setup & Running Instructions

### Prerequisites
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js (v18.x or later)](https://nodejs.org/)
*   [PostgreSQL (v14 or later)](https://www.postgresql.org/)

---

### 1. Database Setup

You can set up the PostgreSQL database in two ways:

#### Option A: Using the SQL Script (Recommended & Fastest)
1. Create a database in PostgreSQL named `School_management_system`.
2. Execute the provided [`database_setup.sql`](file:///e:/Tanvir%20Office/Project/School%20Mnagement%20System/database_setup.sql) script against the database using pgAdmin, `psql`, or any SQL client:
   ```bash
   psql -U postgres -d School_management_system -f database_setup.sql
   ```

#### Option B: EF Core Migrations
1. Update the database connection string in `backend/SchoolManagement.API/appsettings.json` (or `.env`):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=School_management_system;Username=postgres;Password=your_password"
   }
   ```
2. Navigate to the API folder and run:
   ```bash
   dotnet ef database update
   ```

---

### 2. Running the Backend (API)

1. Open your terminal and navigate to the API directory:
   ```bash
   cd backend/SchoolManagement.API
   ```
2. Run the application:
   ```bash
   dotnet run
   ```
3. The API will start and redirect you to the interactive Swagger Documentation at:
   `http://localhost:5000/swagger`

---

### 3. Running the Frontend

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the Next.js development server:
   ```bash
   npm run dev
   ```
4. Access the web interface at: `http://localhost:3000`

---

### 4. Running the Unit Tests

1. Navigate to the backend directory containing tests:
   ```bash
   cd backend
   ```
2. Run the test suite:
   ```bash
   dotnet test tests/SchoolManagement.Tests/SchoolManagement.Tests.csproj
   ```

---

## 🔑 Demo Credentials

All passwords are securely hashed using BCrypt. Use the following credentials to explore different views of the system:

| Role | Email | Password | Primary Features |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@school.com` | `Admin@123` | Control Panel, Class & Subject setup, Assigning Teachers |
| **Teacher** | `teacher@school.com` | `Teacher@123` | View assigned classes, Post assignments, Grade and Feedback |
| **Student** | `student@school.com` | `Student@123` | Enrolled coursework, Submit responses, View grades |

*Other seeded test users include `sarah.johnson@school.com` (Teacher), `bob.davis@school.com` (Student), and `carol.white@school.com` (Student).*

---

## ⚠️ Assumptions and Known Limitations

*   **Local Storage Authentication:** Tokens are cached in `localStorage`. In production, secure `httpOnly` cookies should be used.
*   **Database Default Configuration:** The default developer connection configuration assumes a local PostgreSQL instance running with the password `tanvir@2026` or configured via the environment variables.
*   **File Upload Placeholder:** The assignment DTO accepts a PDF URL or path string. In production, this can be integrated with a cloud storage provider (e.g. AWS S3 or Azure Blob Storage).
