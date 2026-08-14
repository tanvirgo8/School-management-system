'use client';

import { useEffect, useState } from 'react';
import { BookOpen } from 'lucide-react';
import Link from 'next/link';
import { Assignment, Submission } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { EmptyState, TableSkeleton, AssignmentStatusBadge, SubmissionStatusBadge } from '@/components/ui';
import { format, isPast } from 'date-fns';

export default function StudentAssignmentsPage() {
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
      .then(([a, s]) => { setAssignments(a.filter(x => x.status === 'PUBLISHED')); setSubmissions(s); })
      .finally(() => setIsLoading(false));
  }, [user]);

  const submissionMap = new Map(submissions.map(s => [s.assignmentId, s]));

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Assignments</h1><p className="page-subtitle">{assignments.length} published assignments</p></div>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={5} /> : assignments.length === 0 ? (
          <EmptyState icon={<BookOpen className="w-6 h-6" />} title="No assignments yet" description="Your teacher hasn't published any assignments yet." />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Title</th><th>Class</th><th>Subject</th><th>Deadline</th><th>Max Marks</th><th>My Status</th><th>Action</th></tr></thead>
              <tbody>
                {assignments.map(a => {
                  const sub = submissionMap.get(a.id);
                  const past = isPast(new Date(a.deadline));
                  return (
                    <tr key={a.id}>
                      <td className="font-medium text-slate-900 max-w-xs">
                        <span className="truncate block">{a.title}</span>
                      </td>
                      <td className="text-slate-600">{a.className}</td>
                      <td><span className="badge badge-blue">{a.subjectName}</span></td>
                      <td className={`text-sm ${past ? 'text-red-600 font-medium' : 'text-slate-600'}`}>
                        {format(new Date(a.deadline), 'MMM d, yyyy HH:mm')}
                      </td>
                      <td className="text-slate-600">{a.maxMarks}</td>
                      <td>
                        {sub ? (
                          <SubmissionStatusBadge status={sub.status} />
                        ) : past ? (
                          <span className="badge badge-red">Missed</span>
                        ) : (
                          <span className="badge badge-yellow">Pending</span>
                        )}
                      </td>
                      <td>
                        {!sub && !past ? (
                          <Link href={`/student/assignments/${a.id}/submit`} className="btn-primary py-1 px-3 text-xs">Submit</Link>
                        ) : sub ? (
                          <Link href={`/student/submissions/${sub.id}`} className="btn-secondary py-1 px-3 text-xs">View</Link>
                        ) : (
                          <span className="text-xs text-slate-400">—</span>
                        )}
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
