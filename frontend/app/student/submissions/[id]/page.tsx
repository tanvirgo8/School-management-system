'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import toast from 'react-hot-toast';
import { ArrowLeft, Calendar, Award, User, FileText, CheckCircle } from 'lucide-react';
import Link from 'next/link';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { SubmissionStatusBadge } from '@/components/ui';
import { format } from 'date-fns';

export default function StudentViewSubmissionPage() {
  const { id } = useParams<{ id: string }>();
  const router = useRouter();
  const [submission, setSubmission] = useState<Submission | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    submissionsService.getById(id)
      .then(s => {
        setSubmission(s);
        setIsLoading(false);
      })
      .catch(() => {
        toast.error('Failed to load submission detail.');
        router.push('/student/assignments');
      });
  }, [id, router]);

  if (isLoading) return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;
  if (!submission) return <div className="text-center py-16 text-slate-500">Submission not found.</div>;

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href="/student/assignments" className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">View Submission</h1>
          <p className="text-sm text-slate-500">{submission.assignmentTitle}</p>
        </div>
      </div>

      {/* Info Cards */}
      <div className="grid grid-cols-3 gap-4">
        <div className="card p-4 flex items-center gap-3">
          <Calendar className="w-5 h-5 text-blue-500" />
          <div>
            <p className="text-xs text-slate-500">Submitted</p>
            <p className="text-sm font-semibold text-slate-900">
              {format(new Date(submission.submittedAt), 'MMM d, HH:mm')}
            </p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <Award className="w-5 h-5 text-purple-500" />
          <div>
            <p className="text-xs text-slate-500">Marks Obtained</p>
            <p className="text-sm font-semibold text-slate-900">
              {submission.marks != null ? `${submission.marks} / ${submission.assignmentMaxMarks}` : `Pending`}
            </p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <User className="w-5 h-5 text-emerald-500" />
          <div>
            <p className="text-xs text-slate-500">Status</p>
            <div className="mt-0.5">
              <SubmissionStatusBadge status={submission.status} />
            </div>
          </div>
        </div>
      </div>

      {/* Student Submission Text Answer */}
      <div className="card p-6">
        <h2 className="font-semibold text-slate-900 mb-3">Your Answer</h2>
        <div className="bg-slate-50 border border-slate-200 rounded-lg p-4">
          <p className="text-slate-700 text-sm whitespace-pre-wrap leading-relaxed">
            {submission.answer || <span className="text-slate-400 italic">No text answer provided.</span>}
          </p>
        </div>
      </div>

      {/* PDF Attachment (if exists) */}
      {submission.pdfUrl && (
        <div className="card p-6">
          <h2 className="font-semibold text-slate-900 mb-3">Your Attached PDF Answer</h2>
          <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 flex items-center justify-between">
            <div className="flex items-center gap-3">
              <FileText className="w-6 h-6 text-blue-600" />
              <div>
                <p className="text-sm font-semibold text-blue-900">submission_answer.pdf</p>
                <p className="text-xs text-blue-600">Attached successfully</p>
              </div>
            </div>
            <a href={submission.pdfUrl} target="_blank" rel="noreferrer" className="btn-primary py-1.5 px-4 text-xs font-semibold">
              View Attached PDF
            </a>
          </div>
        </div>
      )}

      {/* Teacher Feedback / Grading Details */}
      {submission.status === 'GRADED' && (
        <div className="card p-6 border-l-4 border-emerald-500 bg-emerald-50/50 space-y-4">
          <div className="flex items-center gap-2">
            <CheckCircle className="w-5 h-5 text-emerald-600" />
            <h2 className="font-bold text-emerald-950">Academic Evaluation & Feedback</h2>
          </div>
          <div className="space-y-3">
            <p className="text-sm text-slate-700">
              Grade Score: <strong className="text-emerald-900 text-base">{submission.marks}</strong> out of {submission.assignmentMaxMarks} ({Math.round((submission.marks ?? 0) / submission.assignmentMaxMarks * 100)}%)
            </p>
            {submission.feedback && (
              <div>
                <p className="text-xs font-bold text-slate-500 uppercase tracking-wider mb-1">Teacher's Feedback</p>
                <p className="bg-white border border-slate-200 rounded-lg p-3 text-slate-700 text-sm leading-relaxed whitespace-pre-wrap">
                  {submission.feedback}
                </p>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  );
}
