'use client';

import { useEffect, useState } from 'react';
import { Users, Building2, FlaskConical, ClipboardList, FileText, TrendingUp, Clock } from 'lucide-react';
import { StatCard, CardSkeleton, AssignmentStatusBadge, SubmissionStatusBadge } from '@/components/ui';
import { usersService } from '@/services/users.service';
import { classesService } from '@/services/classes.service';
import { subjectsService } from '@/services/subjects.service';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { Assignment, Submission } from '@/types';
import { format } from 'date-fns';

export default function AdminDashboardPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [stats, setStats] = useState({
    totalStudents: 0, totalTeachers: 0, totalClasses: 0,
    totalSubjects: 0, totalAssignments: 0, pendingSubmissions: 0,
  });
  const [recentAssignments, setRecentAssignments] = useState<Assignment[]>([]);
  const [recentSubmissions, setRecentSubmissions] = useState<Submission[]>([]);

  useEffect(() => {
    const load = async () => {
      try {
        const [users, classes, subjects, assignments, submissions] = await Promise.all([
          usersService.getAll(),
          classesService.getAll(),
          subjectsService.getAll(),
          assignmentsService.getAll(),
          submissionsService.getAll(),
        ]);

        setStats({
          totalStudents: users.filter(u => u.role === 'STUDENT').length,
          totalTeachers: users.filter(u => u.role === 'TEACHER').length,
          totalClasses: classes.length,
          totalSubjects: subjects.length,
          totalAssignments: assignments.length,
          pendingSubmissions: submissions.filter(s => s.status === 'SUBMITTED' || s.status === 'UNDER_REVIEW').length,
        });
        setRecentAssignments(assignments.slice(0, 5));
        setRecentSubmissions(submissions.slice(0, 5));
      } catch (e) {
        console.error('Dashboard load error:', e);
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, []);

  return (
    <div className="space-y-6">
      {/* Stats Grid */}
      <div className="grid grid-cols-2 lg:grid-cols-3 xl:grid-cols-6 gap-4">
        {isLoading ? (
          Array.from({ length: 6 }).map((_, i) => <CardSkeleton key={i} />)
        ) : (
          <>
            <StatCard title="Total Students" value={stats.totalStudents} icon={<Users className="w-5 h-5" />} color="bg-blue-50 text-blue-600" />
            <StatCard title="Total Teachers" value={stats.totalTeachers} icon={<Users className="w-5 h-5" />} color="bg-purple-50 text-purple-600" />
            <StatCard title="Classes" value={stats.totalClasses} icon={<Building2 className="w-5 h-5" />} color="bg-emerald-50 text-emerald-600" />
            <StatCard title="Subjects" value={stats.totalSubjects} icon={<FlaskConical className="w-5 h-5" />} color="bg-orange-50 text-orange-600" />
            <StatCard title="Assignments" value={stats.totalAssignments} icon={<ClipboardList className="w-5 h-5" />} color="bg-indigo-50 text-indigo-600" />
            <StatCard title="Pending Reviews" value={stats.pendingSubmissions} icon={<Clock className="w-5 h-5" />} color="bg-yellow-50 text-yellow-600" />
          </>
        )}
      </div>

      {/* Recent Activity */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Recent Assignments */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center justify-between">
            <div className="flex items-center gap-2">
              <ClipboardList className="w-4 h-4 text-slate-500" />
              <h2 className="text-base font-semibold text-slate-900">Recent Assignments</h2>
            </div>
          </div>
          <div className="divide-y divide-slate-100">
            {recentAssignments.length === 0 ? (
              <p className="p-6 text-sm text-slate-400 text-center">No assignments yet</p>
            ) : (
              recentAssignments.map((a) => (
                <div key={a.id} className="px-6 py-3 flex items-center justify-between">
                  <div className="min-w-0 flex-1">
                    <p className="text-sm font-medium text-slate-900 truncate">{a.title}</p>
                    <p className="text-xs text-slate-500">{a.className} · {a.subjectName}</p>
                  </div>
                  <div className="ml-3 shrink-0">
                    <AssignmentStatusBadge status={a.status} />
                  </div>
                </div>
              ))
            )}
          </div>
        </div>

        {/* Recent Submissions */}
        <div className="card">
          <div className="px-6 py-4 border-b border-slate-200 flex items-center gap-2">
            <FileText className="w-4 h-4 text-slate-500" />
            <h2 className="text-base font-semibold text-slate-900">Recent Submissions</h2>
          </div>
          <div className="divide-y divide-slate-100">
            {recentSubmissions.length === 0 ? (
              <p className="p-6 text-sm text-slate-400 text-center">No submissions yet</p>
            ) : (
              recentSubmissions.map((s) => (
                <div key={s.id} className="px-6 py-3 flex items-center justify-between">
                  <div className="min-w-0 flex-1">
                    <p className="text-sm font-medium text-slate-900 truncate">{s.studentName}</p>
                    <p className="text-xs text-slate-500 truncate">{s.assignmentTitle}</p>
                  </div>
                  <div className="ml-3 shrink-0">
                    <SubmissionStatusBadge status={s.status} />
                  </div>
                </div>
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
