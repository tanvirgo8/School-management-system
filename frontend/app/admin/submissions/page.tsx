'use client';

import { useEffect, useState } from 'react';
import { FileText } from 'lucide-react';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { TableSkeleton, EmptyState, SubmissionStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function AdminSubmissionsPage() {
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    submissionsService.getAll().then(setSubmissions).finally(() => setIsLoading(false));
  }, []);

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">All Submissions</h1><p className="page-subtitle">{submissions.length} total submissions</p></div>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={5} /> : submissions.length === 0 ? (
          <EmptyState icon={<FileText className="w-6 h-6" />} title="No submissions yet" description="Student submissions will appear here." />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Student</th><th>Assignment</th><th>Submitted At</th><th>Marks</th><th>Status</th></tr></thead>
              <tbody>
                {submissions.map(s => (
                  <tr key={s.id}>
                    <td>
                      <div>
                        <p className="font-medium text-slate-900">{s.studentName}</p>
                        <p className="text-xs text-slate-500">{s.studentEmail}</p>
                      </div>
                    </td>
                    <td className="text-slate-700 max-w-xs truncate text-sm">{s.assignmentTitle}</td>
                    <td className="text-slate-500 text-sm">{format(new Date(s.submittedAt), 'MMM d, yyyy HH:mm')}</td>
                    <td>
                      {s.marks != null
                        ? <span className="font-semibold text-slate-900">{s.marks}/{s.assignmentMaxMarks}</span>
                        : <span className="text-slate-400">—</span>
                      }
                    </td>
                    <td><SubmissionStatusBadge status={s.status} /></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
