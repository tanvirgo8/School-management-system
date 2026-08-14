import api from '@/lib/api';
import { ApiResponse, CreateSubjectRequest, Subject } from '@/types';

export const subjectsService = {
  async getAll(): Promise<Subject[]> {
    const { data } = await api.get<ApiResponse<Subject[]>>('/subjects');
    return data.data ?? [];
  },

  async create(payload: CreateSubjectRequest): Promise<Subject> {
    const { data } = await api.post<ApiResponse<Subject>>('/subjects', payload);
    if (!data.data) throw new Error(data.message || 'Failed to create subject');
    return data.data;
  },

  async update(id: string, payload: Partial<CreateSubjectRequest>): Promise<Subject> {
    const { data } = await api.put<ApiResponse<Subject>>(`/subjects/${id}`, payload);
    if (!data.data) throw new Error(data.message || 'Failed to update subject');
    return data.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/subjects/${id}`);
  },
};
