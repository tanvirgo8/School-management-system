'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { ArrowLeft, Calendar, Award, BookOpen } from 'lucide-react';
import Link from 'next/link';
import { Assignment } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { useAuth } from '@/hooks/useAuth';
import { LoadingButton } from '@/components/ui';
import { format, isPast } from 'date-fns';

const schema = z.object({ answer: z.string().min(10, 'Answer must be at least 10 characters') });
type Form = z.infer<typeof schema>;

export default function SubmitAssignmentPage() {
  const { id } = useParams<{ id: string }>();
  const { user } = useAuth();
  const router = useRouter();
  const [assignment, setAssignment] = useState<Assignment | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [pdfBase64, setPdfBase64] = useState<string>('');

  const { register, handleSubmit, watch, formState: { errors } } = useForm<Form>({
    resolver: zodResolver(schema),
  });
  const answer = watch('answer', '');

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;
    if (file.type !== 'application/pdf') {
      toast.error('Only PDF files are allowed');
      return;
    }
    const reader = new FileReader();
    reader.onload = () => {
      setPdfBase64(reader.result as string);
    };
    reader.readAsDataURL(file);
  };

  useEffect(() => {
    assignmentsService.getById(id).then(a => { setAssignment(a); setIsLoading(false); });
  }, [id]);

  const onSubmit = async (data: Form) => {
    if (!user) return;
    setIsSubmitting(true);
    try {
      await submissionsService.create({ 
        assignmentId: id, 
        answer: data.answer,
        pdfUrl: pdfBase64 || undefined
      });
      toast.success('Assignment submitted successfully! 🎉');
      router.push('/student/submitted');
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to submit assignment');
    } finally { setIsSubmitting(false); }
  };

  if (isLoading) return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;
  if (!assignment) return <div className="text-center py-16 text-slate-500">Assignment not found.</div>;
  if (isPast(new Date(assignment.deadline))) {
    return (
      <div className="max-w-2xl mx-auto text-center py-16">
        <p className="text-6xl mb-4">⏰</p>
        <h2 className="text-xl font-bold text-red-600 mb-2">Deadline Passed</h2>
        <p className="text-slate-500 mb-6">The submission deadline for this assignment has passed.</p>
        <Link href="/student/assignments" className="btn-secondary">Back to Assignments</Link>
      </div>
    );
  }

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href="/student/assignments" className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">Submit Assignment</h1>
          <p className="text-sm text-slate-500 truncate">{assignment.title}</p>
        </div>
      </div>

      {/* Assignment Details */}
      <div className="card p-6">
        <div className="flex items-center gap-2 mb-3">
          <BookOpen className="w-4 h-4 text-blue-500" />
          <h2 className="font-semibold text-slate-900">Assignment Details</h2>
        </div>
        <p className="text-slate-700 text-sm whitespace-pre-wrap leading-relaxed mb-4">{assignment.description}</p>
        {assignment.pdfUrl && (
          <div className="bg-blue-50 border border-blue-200 rounded-lg p-3 mb-4 flex items-center justify-between">
            <span className="text-sm text-blue-800 font-medium font-mono">📎 Reference PDF Attached</span>
            <a href={assignment.pdfUrl} target="_blank" rel="noreferrer" className="text-xs btn-primary py-1 px-3">View PDF</a>
          </div>
        )}
        <div className="flex gap-6 pt-3 border-t border-slate-100">
          <div className="flex items-center gap-2 text-sm text-slate-500">
            <Calendar className="w-4 h-4" />
            <span>Due: <strong className="text-slate-900">{format(new Date(assignment.deadline), 'MMM d, yyyy HH:mm')}</strong></span>
          </div>
          <div className="flex items-center gap-2 text-sm text-slate-500">
            <Award className="w-4 h-4" />
            <span>Max marks: <strong className="text-slate-900">{assignment.maxMarks}</strong></span>
          </div>
        </div>
      </div>

      {/* Submit Form */}
      <form onSubmit={handleSubmit(onSubmit)} className="card p-6 space-y-4">
        <h2 className="font-semibold text-slate-900">Your Answer</h2>
        <div>
          <textarea
            {...register('answer')}
            rows={10}
            className={`form-textarea ${errors.answer ? 'form-input-error' : ''}`}
            placeholder="Write your answer here. Be detailed and thorough..."
          />
          <div className="flex items-center justify-between mt-1">
            {errors.answer ? (
              <p className="form-error">{errors.answer.message}</p>
            ) : (
              <span />
            )}
            <p className="text-xs text-slate-400">{answer?.length ?? 0} characters</p>
          </div>
        </div>
        <div>
          <label className="form-label">Upload PDF Answer (optional)</label>
          <input type="file" accept="application/pdf" onChange={handleFileChange} className="form-input" />
        </div>
        <div className="flex gap-3 pt-2">
          <Link href="/student/assignments" className="btn-secondary flex-1 text-center">Cancel</Link>
          <LoadingButton type="submit" isLoading={isSubmitting} className="flex-1">Submit Assignment</LoadingButton>
        </div>
      </form>
    </div>
  );
}
