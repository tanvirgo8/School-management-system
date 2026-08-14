'use client';

import RouteGuard from '@/components/RouteGuard';
import Sidebar from '@/components/Sidebar';
import Topbar from '@/components/Topbar';
import { usePathname } from 'next/navigation';

const pageTitles: Record<string, { title: string; subtitle?: string }> = {
  '/admin/dashboard': { title: 'Dashboard', subtitle: 'Overview of your school' },
  '/admin/users': { title: 'User Management', subtitle: 'Manage all users' },
  '/admin/classes': { title: 'Class Management', subtitle: 'Manage classes' },
  '/admin/subjects': { title: 'Subject Management', subtitle: 'Manage subjects' },
  '/admin/teacher-assignments': { title: 'Teacher Assignments', subtitle: 'Assign teachers to classes and subjects' },
  '/admin/assignments': { title: 'All Assignments', subtitle: 'View all assignments' },
  '/admin/submissions': { title: 'All Submissions', subtitle: 'View all submissions' },
};

export default function AdminLayout({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const pageInfo = pageTitles[pathname] ?? { title: 'Admin' };

  return (
    <RouteGuard allowedRole="ADMIN">
      <div className="flex min-h-screen bg-slate-50">
        <Sidebar />
        <div className="flex-1 ml-64 flex flex-col min-h-screen">
          <Topbar title={pageInfo.title} subtitle={pageInfo.subtitle} />
          <main className="flex-1 p-6">{children}</main>
        </div>
      </div>
    </RouteGuard>
  );
}
