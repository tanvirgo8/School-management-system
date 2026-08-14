'use client';

import { usePathname, useRouter } from 'next/navigation';
import Link from 'next/link';
import { useAuth } from '@/hooks/useAuth';
import {
  LayoutDashboard, Users, BookOpen, Building2, FlaskConical,
  ClipboardList, FileText, LogOut, GraduationCap, UserCheck, Settings, ChevronRight
} from 'lucide-react';
import { clsx } from 'clsx';

interface SidebarItem {
  label: string;
  href: string;
  icon: React.ReactNode;
}

const adminItems: SidebarItem[] = [
  { label: 'Dashboard', href: '/admin/dashboard', icon: <LayoutDashboard className="w-4 h-4" /> },
  { label: 'Users', href: '/admin/users', icon: <Users className="w-4 h-4" /> },
  { label: 'Classes', href: '/admin/classes', icon: <Building2 className="w-4 h-4" /> },
  { label: 'Subjects', href: '/admin/subjects', icon: <FlaskConical className="w-4 h-4" /> },
  { label: 'Teacher Assignments', href: '/admin/teacher-assignments', icon: <UserCheck className="w-4 h-4" /> },
  { label: 'Assignments', href: '/admin/assignments', icon: <ClipboardList className="w-4 h-4" /> },
  { label: 'Submissions', href: '/admin/submissions', icon: <FileText className="w-4 h-4" /> },
];

const teacherItems: SidebarItem[] = [
  { label: 'Dashboard', href: '/teacher/dashboard', icon: <LayoutDashboard className="w-4 h-4" /> },
  { label: 'Assignments', href: '/teacher/assignments', icon: <ClipboardList className="w-4 h-4" /> },
  { label: 'Submissions', href: '/teacher/submissions', icon: <FileText className="w-4 h-4" /> },
];

const studentItems: SidebarItem[] = [
  { label: 'Dashboard', href: '/student/dashboard', icon: <LayoutDashboard className="w-4 h-4" /> },
  { label: 'My Assignments', href: '/student/assignments', icon: <ClipboardList className="w-4 h-4" /> },
  { label: 'Submitted', href: '/student/submitted', icon: <FileText className="w-4 h-4" /> },
  { label: 'Pending', href: '/student/pending', icon: <BookOpen className="w-4 h-4" /> },
  { label: 'Results', href: '/student/results', icon: <GraduationCap className="w-4 h-4" /> },
];

const roleItems: Record<string, SidebarItem[]> = {
  ADMIN: adminItems,
  TEACHER: teacherItems,
  STUDENT: studentItems,
};

const roleColors: Record<string, string> = {
  ADMIN: 'bg-purple-600',
  TEACHER: 'bg-blue-600',
  STUDENT: 'bg-emerald-600',
};

const roleLabels: Record<string, string> = {
  ADMIN: 'Administrator',
  TEACHER: 'Teacher',
  STUDENT: 'Student',
};

export default function Sidebar() {
  const { user, logout } = useAuth();
  const router = useRouter();
  const pathname = usePathname();

  const handleLogout = () => {
    logout();
    router.push('/login');
  };

  if (!user) return null;

  const items = roleItems[user.role] ?? [];
  const avatarColor = roleColors[user.role] ?? 'bg-slate-600';
  const roleLabel = roleLabels[user.role] ?? user.role;
  const initials = user.fullName
    .split(' ')
    .map((n) => n[0])
    .slice(0, 2)
    .join('')
    .toUpperCase();

  return (
    <aside className="sidebar">
      {/* Logo */}
      <div className="px-4 py-5 border-b border-slate-800">
        <div className="flex items-center gap-3">
          <div className="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center shrink-0">
            <GraduationCap className="w-5 h-5 text-white" />
          </div>
          <div>
            <p className="text-white font-bold text-sm leading-tight">School</p>
            <p className="text-slate-400 text-xs">Management System</p>
          </div>
        </div>
      </div>

      {/* User info */}
      <div className="px-4 py-4 border-b border-slate-800">
        <div className="flex items-center gap-3">
          <div className={`w-9 h-9 ${avatarColor} rounded-full flex items-center justify-center shrink-0 text-white text-sm font-bold`}>
            {initials}
          </div>
          <div className="min-w-0 flex-1">
            <p className="text-white text-sm font-semibold truncate">{user.fullName}</p>
            <p className="text-slate-400 text-xs">{roleLabel}</p>
          </div>
        </div>
      </div>

      {/* Navigation */}
      <nav className="flex-1 py-4 overflow-y-auto">
        <div className="space-y-1">
          {items.map((item) => {
            const isActive = pathname === item.href || pathname.startsWith(item.href + '/');
            return (
              <Link
                key={item.href}
                href={item.href}
                className={clsx('sidebar-item', { active: isActive })}
              >
                {item.icon}
                <span className="flex-1">{item.label}</span>
                {isActive && <ChevronRight className="w-3 h-3 opacity-60" />}
              </Link>
            );
          })}
        </div>
      </nav>

      {/* Logout */}
      <div className="p-4 border-t border-slate-800">
        <button
          onClick={handleLogout}
          className="sidebar-item w-full text-red-400 hover:bg-red-900/30 hover:text-red-300"
        >
          <LogOut className="w-4 h-4" />
          <span>Logout</span>
        </button>
      </div>
    </aside>
  );
}
