'use client';

import { useEffect, useState } from 'react';
import { Plus, ClipboardList } from 'lucide-react';
import Link from 'next/link';
import { Assignment } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { TableSkeleton, EmptyState, AssignmentStatusBadge } from '@/components/ui';
import { format, isPast } from 'date-fns';

export default function TeacherAssignmentsPage() {
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
        <div><h1 className="page-title">Assignments</h1><p className="page-subtitle">{assignments.length} total</p></div>
        <div className="flex gap-3">
          <select value={statusFilter} onChange={e => setStatusFilter(e.target.value)} className="form-select w-36">
            <option value="">All Status</option>
            <option value="DRAFT">Draft</option>
            <option value="PUBLISHED">Published</option>
            <option value="CLOSED">Closed</option>
          </select>
          <Link href="/teacher/assignments/create" className="btn-primary">
            <Plus className="w-4 h-4" /> Create Assignment
          </Link>
        </div>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={6} /> : filtered.length === 0 ? (
          <EmptyState
            icon={<ClipboardList className="w-6 h-6" />}
            title="No assignments"
            description={statusFilter ? `No ${statusFilter.toLowerCase()} assignments.` : "Create your first assignment to get started."}
            action={<Link href="/teacher/assignments/create" className="btn-primary"><Plus className="w-4 h-4" /> Create Assignment</Link>}
          />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Title</th><th>Class</th><th>Subject</th><th>Deadline</th><th>Max Marks</th><th>Submissions</th><th>Status</th><th>Actions</th></tr></thead>
              <tbody>
                {filtered.map(a => (
                  <tr key={a.id}>
                    <td>
                      <Link href={`/teacher/assignments/${a.id}`} className="font-medium text-blue-600 hover:underline truncate block max-w-48">
                        {a.title}
                      </Link>
                    </td>
                    <td className="text-slate-600">{a.className}</td>
                    <td><span className="badge badge-blue">{a.subjectName}</span></td>
                    <td className={`text-sm ${isPast(new Date(a.deadline)) ? 'text-red-600 font-medium' : 'text-slate-600'}`}>
                      {format(new Date(a.deadline), 'MMM d, yyyy')}
                    </td>
                    <td className="text-slate-600">{a.maxMarks}</td>
                    <td><span className="badge badge-slate">{a.submissionCount}</span></td>
                    <td><AssignmentStatusBadge status={a.status} /></td>
                    <td>
                      <Link href={`/teacher/assignments/${a.id}/edit`} className="btn-ghost py-1 px-2 text-xs">Edit</Link>
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
