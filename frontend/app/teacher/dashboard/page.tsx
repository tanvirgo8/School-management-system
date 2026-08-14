'use client';

import { useEffect, useState } from 'react';
import { ClipboardList, FileText, Clock, CheckCircle, BookOpen, TrendingUp } from 'lucide-react';
import { StatCard, CardSkeleton, AssignmentStatusBadge, SubmissionStatusBadge } from '@/components/ui';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { teacherAssignmentsService } from '@/services/users.service';
import { useAuth } from '@/hooks/useAuth';
import { Assignment, Submission, TeacherAssignment } from '@/types';
import { format, isPast } from 'date-fns';
import Link from 'next/link';

export default function TeacherDashboardPage() {
  const { user } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const [assignments, setAssignments] = useState<Assignment[]>([]);
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [teacherAssignments, setTeacherAssignments] = useState<TeacherAssignment[]>([]);

  useEffect(() => {
    if (!user) return;
    const load = async () => {
      try {
        const [a, s, ta] = await Promise.all([
          assignmentsService.getAll(),
          submissionsService.getAll(),
          teacherAssignmentsService.getAll(user.id),
        ]);
        setAssignments(a);
        setSubmissions(s);
        setTeacherAssignments(ta);
      } finally { setIsLoading(false); }
    };
    load();
  }, [user]);

  const myClasses = new Set(teacherAssignments.map(ta => ta.classId)).size;
  const mySubjects = new Set(teacherAssignments.map(ta => ta.subjectId)).size;
  const published = assignments.filter(a => a.status === 'PUBLISHED').length;
  const pending = submissions.filter(s => s.status === 'SUBMITTED' || s.status === 'UNDER_REVIEW').length;
  const graded = submissions.filter(s => s.status === 'GRADED').length;

  const upcoming = assignments
    .filter(a => a.status === 'PUBLISHED' && !isPast(new Date(a.deadline)))
    .sort((a, b) => new Date(a.deadline).getTime() - new Date(b.deadline).getTime())
    .slice(0, 3);

  return (
    <div className="space-y-6">
      <div className="grid grid-cols-2 lg:grid-cols-3 xl:grid-cols-6 gap-4">
        {isLoading ? Array.from({ length: 6 }).map((_, i) => <CardSkeleton key={i} />) : (
          <>
            <StatCard title="My Classes" value={myClasses} icon={<BookOpen className="w-5 h-5" />} color="bg-blue-50 text-blue-600" />
            <StatCard title="My Subjects" value={mySubjects} icon={<TrendingUp className="w-5 h-5" />} color="bg-purple-50 text-purple-600" />
            <StatCard title="Total Assignments" value={assignments.length} icon={<ClipboardList className="w-5 h-5" />} color="bg-indigo-50 text-indigo-600" />
            <StatCard title="Published" value={published} icon={<CheckCircle className="w-5 h-5" />} color="bg-emerald-50 text-emerald-600" />
            <StatCard title="Pending Reviews" value={pending} icon={<Clock className="w-5 h-5" />} color="bg-yellow-50 text-yellow-600" />
            <StatCard title="Graded" value={graded} icon={<FileText className="w-5 h-5" />} color="bg-green-50 text-green-600" />
          </>
        )}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Recent Assignments */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center justify-between">
            <div className="flex items-center gap-2">
              <ClipboardList className="w-4 h-4 text-slate-500" />
              <h2 className="font-semibold text-slate-900">Recent Assignments</h2>
            </div>
            <Link href="/teacher/assignments" className="text-xs text-blue-600 hover:underline">View all</Link>
          </div>
          <div className="divide-y divide-slate-100">
            {assignments.slice(0, 5).map(a => (
              <Link key={a.id} href={`/teacher/assignments/${a.id}`} className="px-6 py-3 flex items-center justify-between hover:bg-slate-50 transition-colors block">
                <div className="min-w-0 flex-1">
                  <p className="text-sm font-medium text-slate-900 truncate">{a.title}</p>
                  <p className="text-xs text-slate-500">{a.className} · {a.subjectName}</p>
                </div>
                <AssignmentStatusBadge status={a.status} />
              </Link>
            ))}
            {!isLoading && assignments.length === 0 && (
              <p className="p-6 text-sm text-slate-400 text-center">No assignments yet. <Link href="/teacher/assignments/create" className="text-blue-600 hover:underline">Create one</Link></p>
            )}
          </div>
        </div>

        {/* Upcoming Deadlines */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center gap-2">
            <Clock className="w-4 h-4 text-slate-500" />
            <h2 className="font-semibold text-slate-900">Upcoming Deadlines</h2>
          </div>
          <div className="divide-y divide-slate-100">
            {upcoming.length === 0 ? (
              <p className="p-6 text-sm text-slate-400 text-center">No upcoming deadlines</p>
            ) : upcoming.map(a => (
              <div key={a.id} className="px-6 py-3">
                <div className="flex items-center justify-between">
                  <p className="text-sm font-medium text-slate-900 truncate flex-1">{a.title}</p>
                  <p className="text-xs text-orange-600 font-medium ml-2 shrink-0">{format(new Date(a.deadline), 'MMM d')}</p>
                </div>
                <p className="text-xs text-slate-500">{a.className} · {a.submissionCount} submissions</p>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
