'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { Star, CheckCircle } from 'lucide-react';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { TableSkeleton, EmptyState } from '@/components/ui';
import { format } from 'date-fns';

export default function StudentResultsPage() {
  const { user } = useAuth();
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (!user) return;
    submissionsService.getAll({ studentId: user.id })
      .then(s => setSubmissions(s.filter(x => x.status === 'GRADED')))
      .finally(() => setIsLoading(false));
  }, [user]);

  const avgMarks = submissions.length > 0
    ? Math.round(submissions.reduce((sum, s) => sum + (s.marks ?? 0) / s.assignmentMaxMarks * 100, 0) / submissions.length)
    : 0;

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div>
          <h1 className="page-title">My Results</h1>
          <p className="page-subtitle">Your graded assignments and academic feedback</p>
        </div>
      </div>

      {!isLoading && submissions.length > 0 && (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-4 flex items-center gap-3">
            <CheckCircle className="w-5 h-5 text-emerald-600 shrink-0" />
            <p className="text-sm text-emerald-800">
              You have completed and received grades for <strong>{submissions.length}</strong> assignments.
            </p>
          </div>
          <div className="bg-purple-50 border border-purple-200 rounded-xl p-4 flex items-center gap-3">
            <Star className="w-5 h-5 text-purple-600 shrink-0" />
            <p className="text-sm text-purple-800">
              Average academic grade: <strong>{avgMarks}%</strong>
            </p>
          </div>
        </div>
      )}

      <div className="card">
        {isLoading ? <TableSkeleton rows={4} cols={5} /> : submissions.length === 0 ? (
          <EmptyState
            icon={<Star className="w-6 h-6 text-yellow-500" />}
            title="No graded assignments yet"
            description="When your teacher grades your submitted work, the scores and feedback will appear here."
            action={<Link href="/student/assignments" className="btn-primary">View Assignments</Link>}
          />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead>
                <tr>
                  <th>Assignment</th>
                  <th>Submitted At</th>
                  <th>Obtained Marks</th>
                  <th>Max Marks</th>
                  <th>Score</th>
                  <th>Feedback</th>
                </tr>
              </thead>
              <tbody>
                {submissions.map(s => {
                  const pct = Math.round((s.marks ?? 0) / s.assignmentMaxMarks * 100);
                  return (
                    <tr key={s.id}>
                      <td className="font-medium text-slate-900">{s.assignmentTitle}</td>
                      <td className="text-slate-500 text-sm">{format(new Date(s.submittedAt), 'MMM d, yyyy')}</td>
                      <td className="font-semibold text-slate-900">{s.marks}</td>
                      <td className="text-slate-600">{s.assignmentMaxMarks}</td>
                      <td>
                        <span className={`text-xs font-semibold px-2.5 py-1 rounded-full ${
                          pct >= 85 ? 'bg-emerald-100 text-emerald-800' :
                          pct >= 70 ? 'bg-blue-100 text-blue-800' :
                          pct >= 50 ? 'bg-yellow-100 text-yellow-800' : 'bg-red-100 text-red-800'
                        }`}>
                          {pct}%
                        </span>
                      </td>
                      <td className="text-slate-600 text-sm max-w-xs whitespace-pre-wrap">{s.feedback || <span className="text-slate-400 italic">No feedback provided.</span>}</td>
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
