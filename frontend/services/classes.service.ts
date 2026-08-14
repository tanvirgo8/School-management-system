import api from '@/lib/api';
import { ApiResponse, Class, CreateClassRequest } from '@/types';

export const classesService = {
  async getAll(): Promise<Class[]> {
    const { data } = await api.get<ApiResponse<Class[]>>('/classes');
    return data.data ?? [];
  },

  async getById(id: string): Promise<Class | null> {
    try {
      const { data } = await api.get<ApiResponse<Class>>(`/classes/${id}`);
      return data.data ?? null;
    } catch {
      return null;
    }
  },

  async create(payload: CreateClassRequest): Promise<Class> {
    const { data } = await api.post<ApiResponse<Class>>('/classes', payload);
    if (!data.data) throw new Error(data.message || 'Failed to create class');
    return data.data;
  },

  async update(id: string, payload: Partial<CreateClassRequest>): Promise<Class> {
    const { data } = await api.put<ApiResponse<Class>>(`/classes/${id}`, payload);
    if (!data.data) throw new Error(data.message || 'Failed to update class');
    return data.data;
  },

  async delete(id: string): Promise<void> {
    await api.delete(`/classes/${id}`);
  },
};
