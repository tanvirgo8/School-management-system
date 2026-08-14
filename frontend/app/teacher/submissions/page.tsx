'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { FileText } from 'lucide-react';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { TableSkeleton, EmptyState, SubmissionStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function TeacherSubmissionsPage() {
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [statusFilter, setStatusFilter] = useState('');

  useEffect(() => {
    submissionsService.getAll().then(setSubmissions).finally(() => setIsLoading(false));
  }, []);

  const filtered = statusFilter ? submissions.filter(s => s.status === statusFilter) : submissions;

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Submissions</h1><p className="page-subtitle">{submissions.length} total</p></div>
        <select value={statusFilter} onChange={e => setStatusFilter(e.target.value)} className="form-select w-40">
          <option value="">All Status</option>
          <option value="SUBMITTED">Submitted</option>
          <option value="UNDER_REVIEW">Under Review</option>
          <option value="GRADED">Graded</option>
          <option value="LATE">Late</option>
        </select>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={5} /> : filtered.length === 0 ? (
          <EmptyState icon={<FileText className="w-6 h-6" />} title="No submissions" description="Submissions from your students will appear here." />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Student</th><th>Assignment</th><th>Submitted At</th><th>Marks</th><th>Status</th><th>Action</th></tr></thead>
              <tbody>
                {filtered.map(s => (
                  <tr key={s.id}>
                    <td>
                      <div>
                        <p className="font-medium text-slate-900">{s.studentName}</p>
                        <p className="text-xs text-slate-500">{s.studentEmail}</p>
                      </div>
                    </td>
                    <td className="text-slate-700 text-sm max-w-xs truncate">{s.assignmentTitle}</td>
                    <td className="text-slate-500 text-sm">{format(new Date(s.submittedAt), 'MMM d, HH:mm')}</td>
                    <td>
                      {s.marks != null
                        ? <span className="font-semibold text-slate-900">{s.marks}/{s.assignmentMaxMarks}</span>
                        : <span className="text-slate-400">—</span>
                      }
                    </td>
                    <td><SubmissionStatusBadge status={s.status} /></td>
                    <td>
                      <Link href={`/teacher/submissions/${s.id}`} className="btn-ghost py-1 px-2 text-xs text-blue-600">
                        {s.status === 'GRADED' ? 'View' : 'Grade'}
                      </Link>
                    </td>
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
