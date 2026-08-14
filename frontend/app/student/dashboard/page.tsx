'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { ClipboardList, FileText, CheckCircle, Clock, Star, BookOpen } from 'lucide-react';
import { StatCard, CardSkeleton, AssignmentStatusBadge } from '@/components/ui';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { Assignment, Submission } from '@/types';
import { format, isPast } from 'date-fns';

export default function StudentDashboardPage() {
  const { user } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const [assignments, setAssignments] = useState<Assignment[]>([]);
  const [submissions, setSubmissions] = useState<Submission[]>([]);

  useEffect(() => {
    if (!user) return;
    Promise.all([
      assignmentsService.getAll({ classId: user.classId }),
      submissionsService.getAll({ studentId: user.id })
    ])
      .then(([a, s]) => { setAssignments(a); setSubmissions(s); })
      .finally(() => setIsLoading(false));
  }, [user]);

  const published = assignments.filter(a => a.status === 'PUBLISHED');
  const submittedIds = new Set(submissions.map(s => s.assignmentId));
  const pending = published.filter(a => !submittedIds.has(a.id) && !isPast(new Date(a.deadline)));
  const graded = submissions.filter(s => s.status === 'GRADED');
  const avgMarks = graded.length > 0
    ? Math.round(graded.reduce((sum, s) => sum + (s.marks ?? 0) / s.assignmentMaxMarks * 100, 0) / graded.length)
    : 0;

  const upcoming = pending.sort((a, b) => new Date(a.deadline).getTime() - new Date(b.deadline).getTime()).slice(0, 5);

  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-lg font-semibold text-slate-900">Welcome back, {user?.fullName?.split(' ')[0]}! 👋</h2>
        <p className="text-sm text-slate-500">Here's your learning overview.</p>
      </div>

      <div className="grid grid-cols-2 lg:grid-cols-3 xl:grid-cols-6 gap-4">
        {isLoading ? Array.from({ length: 6 }).map((_, i) => <CardSkeleton key={i} />) : (
          <>
            <StatCard title="Total Assignments" value={published.length} icon={<BookOpen className="w-5 h-5" />} color="bg-blue-50 text-blue-600" />
            <StatCard title="Pending" value={pending.length} icon={<Clock className="w-5 h-5" />} color="bg-yellow-50 text-yellow-600" />
            <StatCard title="Submitted" value={submissions.length} icon={<FileText className="w-5 h-5" />} color="bg-indigo-50 text-indigo-600" />
            <StatCard title="Graded" value={graded.length} icon={<CheckCircle className="w-5 h-5" />} color="bg-emerald-50 text-emerald-600" />
            <StatCard title="Avg Score" value={`${avgMarks}%`} icon={<Star className="w-5 h-5" />} color="bg-purple-50 text-purple-600" />
            <StatCard title="Late Penalty" value={submissions.filter(s => s.status === 'LATE').length} icon={<ClipboardList className="w-5 h-5" />} color="bg-red-50 text-red-600" />
          </>
        )}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Upcoming / Pending */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center justify-between">
            <div className="flex items-center gap-2">
              <Clock className="w-4 h-4 text-yellow-500" />
              <h2 className="font-semibold text-slate-900">Pending Assignments</h2>
            </div>
            <Link href="/student/pending" className="text-xs text-blue-600 hover:underline">View all</Link>
          </div>
          <div className="divide-y divide-slate-100">
            {upcoming.length === 0 ? (
              <div className="p-8 text-center">
                <CheckCircle className="w-8 h-8 text-emerald-400 mx-auto mb-2" />
                <p className="text-sm text-slate-500">All caught up! 🎉</p>
              </div>
            ) : upcoming.map(a => (
              <div key={a.id} className="px-6 py-3">
                <div className="flex items-center justify-between">
                  <p className="text-sm font-medium text-slate-900 truncate flex-1">{a.title}</p>
                  <p className={`text-xs font-medium ml-2 shrink-0 ${isPast(new Date(a.deadline)) ? 'text-red-600' : 'text-orange-600'}`}>
                    {format(new Date(a.deadline), 'MMM d')}
                  </p>
                </div>
                <p className="text-xs text-slate-500">{a.className} · {a.subjectName}</p>
              </div>
            ))}
          </div>
        </div>

        {/* Recent Results */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center justify-between">
            <div className="flex items-center gap-2">
              <Star className="w-4 h-4 text-yellow-500" />
              <h2 className="font-semibold text-slate-900">Recent Grades</h2>
            </div>
            <Link href="/student/results" className="text-xs text-blue-600 hover:underline">View all</Link>
          </div>
          <div className="divide-y divide-slate-100">
            {graded.length === 0 ? (
              <p className="p-8 text-center text-sm text-slate-400">No grades yet.</p>
            ) : graded.slice(0, 5).map(s => {
              const pct = Math.round((s.marks ?? 0) / s.assignmentMaxMarks * 100);
              return (
                <div key={s.id} className="px-6 py-3 flex items-center gap-4">
                  <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium text-slate-900 truncate">{s.assignmentTitle}</p>
                    <p className="text-xs text-slate-500">{format(new Date(s.submittedAt), 'MMM d')}</p>
                  </div>
                  <div className="text-right shrink-0">
                    <p className="font-bold text-slate-900">{s.marks}/{s.assignmentMaxMarks}</p>
                    <p className={`text-xs font-medium ${pct >= 80 ? 'text-emerald-600' : pct >= 60 ? 'text-yellow-600' : 'text-red-600'}`}>{pct}%</p>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </div>
  );
}
