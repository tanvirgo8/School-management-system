// ─── Enums ──────────────────────────────────────────────
export type UserRole = 'ADMIN' | 'TEACHER' | 'STUDENT';
export type AssignmentStatus = 'DRAFT' | 'PUBLISHED' | 'CLOSED';
export type SubmissionStatus =
  | 'NOT_SUBMITTED'
  | 'SUBMITTED'
  | 'UNDER_REVIEW'
  | 'GRADED'
  | 'LATE';

// ─── Entities ────────────────────────────────────────────
export interface User {
  id: string;
  fullName: string;
  email: string;
  role: UserRole;
  phone?: string;
  isActive: boolean;
  createdAt: string;
  classId?: string;
}

export interface Class {
  id: string;
  name: string;
  description?: string;
  tuitionFee: number;
  isActive: boolean;
  createdAt: string;
  studentCount: number;
}

export interface Subject {
  id: string;
  name: string;
  code: string;
  description?: string;
  classId: string;
  className: string;
  isActive: boolean;
  createdAt: string;
}

export interface TeacherAssignment {
  id: string;
  teacherId: string;
  teacherName: string;
  teacherEmail: string;
  classId: string;
  className: string;
  subjectId: string;
  subjectName: string;
  subjectCode: string;
  createdAt: string;
}

export interface Assignment {
  id: string;
  title: string;
  description: string;
  pdfUrl?: string;
  teacherId: string;
  teacherName: string;
  classId: string;
  className: string;
  subjectId: string;
  subjectName: string;
  subjectCode: string;
  deadline: string;
  maxMarks: number;
  status: AssignmentStatus;
  createdAt: string;
  updatedAt: string;
  submissionCount: number;
}

export interface Submission {
  id: string;
  assignmentId: string;
  assignmentTitle: string;
  assignmentMaxMarks: number;
  assignmentDeadline: string;
  studentId: string;
  studentName: string;
  studentEmail: string;
  answer: string;
  pdfUrl?: string;
  submittedAt: string;
  updatedAt: string;
  marks?: number;
  feedback?: string;
  status: SubmissionStatus;
}

// ─── Auth ────────────────────────────────────────────────
export interface AuthUser {
  id: string;
  fullName: string;
  email: string;
  role: UserRole;
  phone?: string;
  isActive: boolean;
  createdAt: string;
  classId?: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  token: string;
  message: string;
  user: AuthUser;
}

// ─── API Responses ───────────────────────────────────────
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
}

// ─── Form DTOs ───────────────────────────────────────────
export interface CreateUserRequest {
  fullName: string;
  email: string;
  password: string;
  role: UserRole;
  phone?: string;
  isActive: boolean;
  classId?: string;
}

export interface CreateClassRequest {
  name: string;
  description?: string;
  tuitionFee: number;
  isActive: boolean;
}

export interface CreateSubjectRequest {
  name: string;
  code: string;
  description?: string;
  classId: string;
  isActive: boolean;
}

export interface CreateTeacherAssignmentRequest {
  teacherId: string;
  classId: string;
  subjectId: string;
}

export interface CreateAssignmentRequest {
  title: string;
  description: string;
  pdfUrl?: string;
  classId: string;
  subjectId: string;
  deadline: string;
  maxMarks: number;
  status: AssignmentStatus;
}

export interface CreateSubmissionRequest {
  assignmentId: string;
  answer: string;
  pdfUrl?: string;
}

export interface GradeSubmissionRequest {
  marks: number;
  feedback?: string;
  status: SubmissionStatus;
}

// ─── Dashboard Stats ─────────────────────────────────────
export interface AdminStats {
  totalStudents: number;
  totalTeachers: number;
  totalClasses: number;
  totalSubjects: number;
  totalAssignments: number;
  pendingSubmissions: number;
}

export interface TeacherStats {
  myClasses: number;
  mySubjects: number;
  totalAssignments: number;
  publishedAssignments: number;
  pendingSubmissions: number;
  gradedSubmissions: number;
}

export interface StudentStats {
  totalAssignments: number;
  pendingAssignments: number;
  submittedAssignments: number;
  gradedAssignments: number;
  averageMarks: number;
}
