'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { Clock, AlertTriangle } from 'lucide-react';
import { Assignment, Submission } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { EmptyState, TableSkeleton } from '@/components/ui';
import { format, isPast, formatDistanceToNow } from 'date-fns';

export default function StudentPendingPage() {
  const { user } = useAuth();
  const [assignments, setAssignments] = useState<Assignment[]>([]);
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (!user) return;
    Promise.all([
      assignmentsService.getAll({ classId: user.classId }),
      submissionsService.getAll({ studentId: user.id })
    ])
      .then(([a, s]) => {
        setAssignments(a.filter(x => x.status === 'PUBLISHED'));
        setSubmissions(s);
      })
      .finally(() => setIsLoading(false));
  }, [user]);

  const submittedIds = new Set(submissions.map(s => s.assignmentId));
  const pending = assignments
    .filter(a => !submittedIds.has(a.id) && !isPast(new Date(a.deadline)))
    .sort((a, b) => new Date(a.deadline).getTime() - new Date(b.deadline).getTime());

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Pending Assignments</h1><p className="page-subtitle">{pending.length} to complete</p></div>
      </div>

      {!isLoading && pending.length > 0 && (
        <div className="bg-yellow-50 border border-yellow-200 rounded-xl p-4 flex items-center gap-3">
          <AlertTriangle className="w-5 h-5 text-yellow-600 shrink-0" />
          <p className="text-sm text-yellow-800">You have <strong>{pending.length}</strong> assignment{pending.length > 1 ? 's' : ''} pending. Submit before the deadlines!</p>
        </div>
      )}

      <div className="card">
        {isLoading ? <TableSkeleton rows={4} cols={5} /> : pending.length === 0 ? (
          <EmptyState
            icon={<Clock className="w-6 h-6 text-emerald-500" />}
            title="All assignments submitted! 🎉"
            description="You're all caught up. No pending assignments."
          />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Title</th><th>Subject</th><th>Max Marks</th><th>Deadline</th><th>Time Left</th><th>Action</th></tr></thead>
              <tbody>
                {pending.map(a => {
                  const deadline = new Date(a.deadline);
                  const hoursLeft = (deadline.getTime() - Date.now()) / 1000 / 60 / 60;
                  const isUrgent = hoursLeft < 24;
                  return (
                    <tr key={a.id}>
                      <td>
                        <p className="font-medium text-slate-900 max-w-xs truncate">{a.title}</p>
                        <p className="text-xs text-slate-500">{a.className}</p>
                      </td>
                      <td><span className="badge badge-blue">{a.subjectName}</span></td>
                      <td className="text-slate-600">{a.maxMarks}</td>
                      <td className={`text-sm font-medium ${isUrgent ? 'text-red-600' : 'text-slate-700'}`}>
                        {format(deadline, 'MMM d, HH:mm')}
                      </td>
                      <td>
                        <span className={`text-xs font-medium px-2 py-1 rounded-full ${isUrgent ? 'bg-red-100 text-red-700' : 'bg-slate-100 text-slate-700'}`}>
                          {formatDistanceToNow(deadline, { addSuffix: true })}
                        </span>
                      </td>
                      <td>
                        <Link href={`/student/assignments/${a.id}/submit`} className="btn-primary py-1 px-3 text-xs">
                          Submit Now
                        </Link>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
