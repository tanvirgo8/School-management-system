import api, { getStoredUser, getToken, removeToken, setStoredUser, setToken } from '@/lib/api';
import { AuthUser, LoginRequest, LoginResponse } from '@/types';

export const authService = {
  async login(credentials: LoginRequest): Promise<LoginResponse> {
    const { data } = await api.post<LoginResponse>('/auth/login', credentials);
    if (data.success && data.token) {
      setToken(data.token);
      setStoredUser(data.user);
    }
    return data;
  },

  async getCurrentUser(): Promise<AuthUser | null> {
    try {
      const { data } = await api.get<{ success: boolean; data: AuthUser }>('/auth/me');
      if (data.success && data.data) {
        setStoredUser(data.data);
        return data.data;
      }
      return null;
    } catch {
      return null;
    }
  },

  logout(): void {
    removeToken();
  },

  getStoredUser(): AuthUser | null {
    return getStoredUser<AuthUser>();
  },

  isAuthenticated(): boolean {
    return !!getToken();
  },

  getRedirectPath(role: string): string {
    switch (role) {
      case 'ADMIN':
        return '/admin/dashboard';
      case 'TEACHER':
        return '/teacher/dashboard';
      case 'STUDENT':
        return '/student/dashboard';
      default:
        return '/login';
    }
  },
};
