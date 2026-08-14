import api from '@/lib/api';
import { ApiResponse, CreateSubmissionRequest, GradeSubmissionRequest, Submission } from '@/types';

export const submissionsService = {
  async getAll(params?: { assignmentId?: string; studentId?: string }): Promise<Submission[]> {
    const { data } = await api.get<ApiResponse<Submission[]>>('/submissions', { params });
    return data.data ?? [];
  },

  async getById(id: string): Promise<Submission | null> {
    try {
      const { data } = await api.get<ApiResponse<Submission>>(`/submissions/${id}`);
      return data.data ?? null;
    } catch {
      return null;
    }
  },

  async create(payload: CreateSubmissionRequest): Promise<Submission> {
    const { data } = await api.post<ApiResponse<Submission>>('/submissions', payload);
    if (!data.data) throw new Error(data.message || 'Failed to submit assignment');
    return data.data;
  },

  async update(id: string, answer: string): Promise<Submission> {
    const { data } = await api.put<ApiResponse<Submission>>(`/submissions/${id}`, { answer });
    if (!data.data) throw new Error(data.message || 'Failed to update submission');
    return data.data;
  },

  async grade(id: string, payload: GradeSubmissionRequest): Promise<Submission> {
    const { data } = await api.post<ApiResponse<Submission>>(`/submissions/${id}/grade`, payload);
    if (!data.data) throw new Error(data.message || 'Failed to grade submission');
    return data.data;
  },
};
