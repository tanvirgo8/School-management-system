import api from '@/lib/api';
import { ApiResponse, CreateTeacherAssignmentRequest, CreateUserRequest, User, TeacherAssignment } from '@/types';

export const usersService = {
  async getAll(params?: { role?: string; search?: string }): Promise<User[]> {
    const { data } = await api.get<ApiResponse<User[]>>('/users', { params });
    return data.data ?? [];
  },

  async getById(id: string): Promise<User | null> {
    try {
      const { data } = await api.get<ApiResponse<User>>(`/users/${id}`);
      return data.data ?? null;
    } catch {
      return null;
    }
  },

  async create(payload: CreateUserRequest): Promise<User> {
    const { data } = await api.post<ApiResponse<User>>('/users', payload);
    if (!data.data) throw new Error(data.message || 'Failed to create user');
    return data.data;
  },

  async update(id: string, payload: Partial<CreateUserRequest>): Promise<User> {
    const { data } = await api.put<ApiResponse<User>>(`/users/${id}`, payload);
    if (!data.data) throw new Error(data.message || 'Failed to update user');
    return data.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/users/${id}`);
  },
};

export const teacherAssignmentsService = {
  async getAll(teacherId?: string): Promise<TeacherAssignment[]> {
    const params = teacherId ? { teacherId } : undefined;
    const { data } = await api.get<ApiResponse<TeacherAssignment[]>>('/teacher-assignments', { params });
    return data.data ?? [];
  },

  async create(payload: CreateTeacherAssignmentRequest): Promise<TeacherAssignment> {
    const { data } = await api.post<ApiResponse<TeacherAssignment>>('/teacher-assignments', payload);
    if (!data.data) throw new Error(data.message || 'Failed to create teacher assignment');
    return data.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/teacher-assignments/${id}`);
  },
};
