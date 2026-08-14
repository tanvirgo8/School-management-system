import api from '@/lib/api';
import { ApiResponse, Assignment, AssignmentStatus, CreateAssignmentRequest } from '@/types';

export const assignmentsService = {
  async getAll(params?: { teacherId?: string; classId?: string; status?: AssignmentStatus }): Promise<Assignment[]> {
    const { data } = await api.get<ApiResponse<Assignment[]>>('/assignments', { params });
    return data.data ?? [];
  },

  async getById(id: string): Promise<Assignment | null> {
    try {
      const { data } = await api.get<ApiResponse<Assignment>>(`/assignments/${id}`);
      return data.data ?? null;
    } catch {
      return null;
    }
  },

  async create(payload: CreateAssignmentRequest): Promise<Assignment> {
    const { data } = await api.post<ApiResponse<Assignment>>('/assignments', payload);
    if (!data.data) throw new Error(data.message || 'Failed to create assignment');
    return data.data;
  },

  async update(id: string, payload: Partial<CreateAssignmentRequest>): Promise<Assignment> {
    const { data } = await api.put<ApiResponse<Assignment>>(`/assignments/${id}`, payload);
    if (!data.data) throw new Error(data.message || 'Failed to update assignment');
    return data.data;
  },

  async publish(id: string): Promise<Assignment> {
    const { data } = await api.post<ApiResponse<Assignment>>(`/assignments/${id}/publish`);
    if (!data.data) throw new Error(data.message || 'Failed to publish assignment');
    return data.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/assignments/${id}`);
  },
};
