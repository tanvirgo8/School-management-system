'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { FileText } from 'lucide-react';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { TableSkeleton, EmptyState, SubmissionStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function StudentSubmittedPage() {
  const { user } = useAuth();
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (!user) return;
    submissionsService.getAll({ studentId: user.id }).then(setSubmissions).finally(() => setIsLoading(false));
  }, [user]);

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Submitted Work</h1><p className="page-subtitle">{submissions.length} total submissions</p></div>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={4} /> : submissions.length === 0 ? (
          <EmptyState icon={<FileText className="w-6 h-6" />} title="No submissions yet" description="Submit your assignments to see them here." action={<Link href="/student/assignments" className="btn-primary">View Assignments</Link>} />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Assignment</th><th>Submitted At</th><th>Marks</th><th>Feedback</th><th>Status</th><th>Action</th></tr></thead>
              <tbody>
                {submissions.map(s => {
                  const pct = s.marks != null ? Math.round(s.marks / s.assignmentMaxMarks * 100) : null;
                  return (
                    <tr key={s.id}>
                      <td>
                        <p className="font-medium text-slate-900 max-w-xs truncate">{s.assignmentTitle}</p>
                      </td>
                      <td className="text-slate-500 text-sm">{format(new Date(s.submittedAt), 'MMM d, yyyy HH:mm')}</td>
                      <td>
                        {s.marks != null ? (
                          <div>
                            <span className="font-semibold text-slate-900">{s.marks}/{s.assignmentMaxMarks}</span>
                            <span className={`ml-2 text-xs font-medium ${pct! >= 80 ? 'text-emerald-600' : pct! >= 60 ? 'text-yellow-600' : 'text-red-600'}`}>({pct}%)</span>
                          </div>
                        ) : <span className="text-slate-400">—</span>}
                      </td>
                      <td className="text-slate-600 text-sm max-w-xs truncate">{s.feedback ?? <span className="text-slate-400">—</span>}</td>
                      <td><SubmissionStatusBadge status={s.status} /></td>
                      <td>
                        <Link href={`/student/submissions/${s.id}`} className="btn-ghost py-1 px-3 text-xs text-blue-600">
                          View
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
