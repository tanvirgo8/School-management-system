'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { ArrowLeft, Calendar, Award, User } from 'lucide-react';
import Link from 'next/link';
import { Submission } from '@/types';
import { submissionsService } from '@/services/submissions.service';
import { SubmissionStatusBadge, LoadingButton } from '@/components/ui';
import { format } from 'date-fns';

const gradeSchema = z.object({
  marks: z.coerce.number().min(0, 'Marks cannot be negative'),
  feedback: z.string().optional(),
});
type GradeForm = z.infer<typeof gradeSchema>;

export default function GradeSubmissionPage() {
  const { id } = useParams<{ id: string }>();
  const router = useRouter();
  const [submission, setSubmission] = useState<Submission | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isGrading, setIsGrading] = useState(false);

  const { register, handleSubmit, formState: { errors } } = useForm<any>({
    resolver: zodResolver(gradeSchema),
    defaultValues: { marks: 0, feedback: '' },
  });

  useEffect(() => {
    submissionsService.getById(id).then(s => { setSubmission(s); setIsLoading(false); });
  }, [id]);

  const onSubmit = async (data: GradeForm) => {
    if (!submission) return;
    if (data.marks > submission.assignmentMaxMarks) {
      toast.error(`Marks cannot exceed maximum (${submission.assignmentMaxMarks})`);
      return;
    }
    setIsGrading(true);
    try {
      await submissionsService.grade(id, { 
        marks: data.marks, 
        feedback: data.feedback,
        status: 'GRADED'
      });
      toast.success('Submission graded successfully!');
      router.push('/teacher/submissions');
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to grade submission');
    } finally { setIsGrading(false); }
  };

  if (isLoading) return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;
  if (!submission) return <div className="text-center py-16 text-slate-500">Submission not found.</div>;

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href="/teacher/submissions" className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">Grade Submission</h1>
          <p className="text-sm text-slate-500">{submission.assignmentTitle}</p>
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
            <h2 className="font-semibold text-slate-900">Student Answer</h2>
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

      {/* Existing grade if already graded */}
      {submission.status === 'GRADED' && (
        <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-4">
          <p className="text-sm font-medium text-emerald-800">
            ✅ Already graded: <strong>{submission.marks}/{submission.assignmentMaxMarks}</strong>
          </p>
          {submission.feedback && <p className="text-sm text-emerald-700 mt-1">Feedback: {submission.feedback}</p>}
        </div>
      )}

      {/* Grading Form */}
      <form onSubmit={handleSubmit(onSubmit)} className="card p-6 space-y-5">
        <h2 className="font-semibold text-slate-900">Grade Submission</h2>
        <div>
          <label className="form-label">Marks (out of {submission.assignmentMaxMarks}) *</label>
          <input
            type="number"
            {...register('marks')}
            min={0}
            max={submission.assignmentMaxMarks}
            className={`form-input w-32 ${errors.marks ? 'form-input-error' : ''}`}
          />
          {errors.marks && <p className="form-error">{(errors.marks.message as string)}</p>}
        </div>
        <div>
          <label className="form-label">Feedback (optional)</label>
          <textarea {...register('feedback')} rows={4} className="form-textarea" placeholder="Provide constructive feedback to the student..." />
        </div>
        <div className="flex gap-3 pt-2">
          <Link href="/teacher/submissions" className="btn-secondary flex-1 text-center">Back</Link>
          <LoadingButton type="submit" isLoading={isGrading} variant="success" className="flex-1">
            {submission.status === 'GRADED' ? 'Update Grade' : 'Submit Grade'}
          </LoadingButton>
        </div>
      </form>
    </div>
  );
}
