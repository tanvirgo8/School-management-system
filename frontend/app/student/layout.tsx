'use client';

import RouteGuard from '@/components/RouteGuard';
import Sidebar from '@/components/Sidebar';
import Topbar from '@/components/Topbar';
import { usePathname } from 'next/navigation';

const pageTitles: Record<string, { title: string; subtitle?: string }> = {
  '/student/dashboard': { title: 'Dashboard', subtitle: 'Your learning overview' },
  '/student/assignments': { title: 'Assignments', subtitle: 'All your assignments' },
  '/student/submitted': { title: 'Submitted', subtitle: 'Your submitted work' },
  '/student/pending': { title: 'Pending', subtitle: 'Assignments to complete' },
  '/student/results': { title: 'Results', subtitle: 'Your grades and scores' },
};

export default function StudentLayout({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const pageInfo = pageTitles[pathname] ?? { title: 'Student' };

  return (
    <RouteGuard allowedRole="STUDENT">
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
