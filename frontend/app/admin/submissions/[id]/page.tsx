'use client';

import { useEffect, useState } from 'react';
import { useParams } from 'next/navigation';
import { ArrowLeft, Calendar, Award, User } from 'lucide-react';
import Link from 'next/link';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { SubmissionStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function AdminGradeSubmissionPage() {
  const { id } = useParams<{ id: string }>();
  const [submission, setSubmission] = useState<Submission | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    submissionsService.getById(id).then(s => { 
      setSubmission(s); 
      setIsLoading(false); 
    });
  }, [id]);

  const onSubmit = async () => {}; // No-op, not used in Admin view

  if (isLoading) return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;
  if (!submission) return <div className="text-center py-16 text-slate-500">Submission not found.</div>;

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href="/admin/submissions" className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">Submission Details</h1>
          <p className="text-sm font-semibold text-blue-600">{submission.assignmentTitle}</p>
        </div>
      </div>

      {/* Info Cards */}
      <div className="grid grid-cols-3 gap-4">
        <div className="card p-4 flex items-center gap-3">
          <User className="w-5 h-5 text-blue-500" />
          <div>
            <p className="text-xs text-slate-500">Student</p>
            <p className="text-sm font-semibold text-slate-900">{submission.studentName}</p>
            <p className="text-xs text-slate-500">{submission.studentEmail}</p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <Calendar className="w-5 h-5 text-purple-500" />
          <div>
            <p className="text-xs text-slate-500">Submitted</p>
            <p className="text-sm font-semibold text-slate-900">{format(new Date(submission.submittedAt), 'MMM d, HH:mm')}</p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <Award className="w-5 h-5 text-yellow-500" />
          <div>
            <p className="text-xs text-slate-500">Max Marks</p>
            <p className="text-sm font-semibold text-slate-900">{submission.assignmentMaxMarks}</p>
          </div>
        </div>
      </div>

      {/* Answer */}
      <div className="card p-6 space-y-4">
        <div>
          <div className="flex items-center justify-between mb-3">
            <h2 className="font-semibold text-slate-900">Student Answer / Response</h2>
            <SubmissionStatusBadge status={submission.status} />
          </div>
          <div className="bg-slate-50 border border-slate-200 rounded-lg p-4">
            <p className="text-slate-700 text-sm whitespace-pre-wrap leading-relaxed">{submission.answer || <span className="text-slate-400 italic">No answer provided.</span>}</p>
          </div>
        </div>
        {submission.pdfUrl && (
          <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 flex items-center justify-between">
            <span className="text-sm text-blue-800 font-medium">📎 Attached PDF Answer File</span>
            <a href={submission.pdfUrl} target="_blank" rel="noreferrer" className="btn-primary py-1 px-3 text-xs font-semibold">View PDF</a>
          </div>
        )}
      </div>

      {/* Grading status and info for Admin */}
      {submission.status === 'GRADED' ? (
        <div className="card p-6 space-y-4">
          <h2 className="font-semibold text-slate-900 border-b pb-2">Grading Details</h2>
          <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-4">
            <p className="text-sm font-medium text-emerald-800">
              Grade Assigned: <strong>{submission.marks}/{submission.assignmentMaxMarks}</strong>
            </p>
            {submission.feedback && (
              <p className="text-sm text-emerald-700 mt-2 whitespace-pre-wrap">
                <strong>Feedback:</strong> {submission.feedback}
              </p>
            )}
          </div>
        </div>
      ) : (
        <div className="bg-yellow-50 border border-yellow-200 rounded-xl p-4">
          <p className="text-sm font-medium text-yellow-800">
            ⏳ This submission is pending grading by the assigned teacher.
          </p>
        </div>
      )}

      <div className="flex pt-4">
        <Link href="/admin/submissions" className="btn-secondary w-full text-center">Back to Submissions</Link>
      </div>
    </div>
  );
}
