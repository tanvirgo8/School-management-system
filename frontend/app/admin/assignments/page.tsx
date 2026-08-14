'use client';

import { useEffect, useState } from 'react';
import { ClipboardList } from 'lucide-react';
import { Assignment } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { TableSkeleton, EmptyState, AssignmentStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function AdminAssignmentsPage() {
  const [assignments, setAssignments] = useState<Assignment[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [statusFilter, setStatusFilter] = useState('');

  useEffect(() => {
    assignmentsService.getAll().then(setAssignments).finally(() => setIsLoading(false));
  }, []);

  const filtered = statusFilter ? assignments.filter(a => a.status === statusFilter) : assignments;

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">All Assignments</h1><p className="page-subtitle">{assignments.length} total</p></div>
        <select value={statusFilter} onChange={e => setStatusFilter(e.target.value)} className="form-select w-36">
          <option value="">All Status</option>
          <option value="DRAFT">Draft</option>
          <option value="PUBLISHED">Published</option>
          <option value="CLOSED">Closed</option>
        </select>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={6} /> : filtered.length === 0 ? (
          <EmptyState icon={<ClipboardList className="w-6 h-6" />} title="No assignments found" description="Assignments created by teachers will appear here." />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Title</th><th>Teacher</th><th>Class</th><th>Subject</th><th>Deadline</th><th>Submissions</th><th>Status</th></tr></thead>
              <tbody>
                {filtered.map(a => (
                  <tr key={a.id}>
                    <td className="font-medium text-slate-900 max-w-xs truncate">{a.title}</td>
                    <td className="text-slate-600 text-sm">{a.teacherName}</td>
                    <td className="text-slate-600">{a.className}</td>
                    <td><span className="badge badge-blue">{a.subjectName}</span></td>
                    <td className={`text-sm ${new Date(a.deadline) < new Date() ? 'text-red-600 font-medium' : 'text-slate-600'}`}>
                      {format(new Date(a.deadline), 'MMM d, yyyy')}
                    </td>
                    <td><span className="badge badge-slate">{a.submissionCount}</span></td>
                    <td><AssignmentStatusBadge status={a.status} /></td>
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
