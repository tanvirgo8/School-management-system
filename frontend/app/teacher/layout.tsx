'use client';

import RouteGuard from '@/components/RouteGuard';
import Sidebar from '@/components/Sidebar';
import Topbar from '@/components/Topbar';
import { usePathname } from 'next/navigation';

const pageTitles: Record<string, { title: string; subtitle?: string }> = {
  '/teacher/dashboard': { title: 'Dashboard', subtitle: 'Your teaching overview' },
  '/teacher/assignments': { title: 'Assignments', subtitle: 'Manage your assignments' },
  '/teacher/assignments/create': { title: 'Create Assignment', subtitle: 'Create a new assignment' },
  '/teacher/submissions': { title: 'Submissions', subtitle: 'Review student submissions' },
};

export default function TeacherLayout({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const pageInfo = pageTitles[pathname] ?? { title: 'Teacher' };

  return (
    <RouteGuard allowedRole="TEACHER">
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
