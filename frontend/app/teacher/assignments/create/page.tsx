'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { ArrowLeft } from 'lucide-react';
import Link from 'next/link';
import { Class, Subject, TeacherAssignment } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { teacherAssignmentsService } from '@/services/users.service';
import { useAuth } from '@/hooks/useAuth';
import { LoadingButton } from '@/components/ui';

const assignmentSchema = z.object({
  title: z.string().min(1, 'Title is required'),
  description: z.string().min(1, 'Description is required'),
  classId: z.string().min(1, 'Class is required'),
  subjectId: z.string().min(1, 'Subject is required'),
  deadline: z.string().min(1, 'Deadline is required'),
  maxMarks: z.coerce.number().min(1, 'Max marks must be greater than 0'),
  status: z.enum(['DRAFT', 'PUBLISHED'] as const),
});

type AssignmentForm = z.infer<typeof assignmentSchema>;

export default function CreateAssignmentPage() {
  const { user } = useAuth();
  const router = useRouter();
  const [teacherAssignments, setTeacherAssignments] = useState<TeacherAssignment[]>([]);
  const [isSaving, setIsSaving] = useState(false);
  const [pdfBase64, setPdfBase64] = useState<string>('');

  const { register, handleSubmit, watch, formState: { errors } } = useForm<any>({
    resolver: zodResolver(assignmentSchema),
    defaultValues: { status: 'DRAFT', maxMarks: 100 },
  });

  const watchClassId = watch('classId');

  useEffect(() => {
    if (user) teacherAssignmentsService.getAll(user.id).then(setTeacherAssignments);
  }, [user]);

  const uniqueClasses = Array.from(new Map(teacherAssignments.map(ta => [ta.classId, { id: ta.classId, name: ta.className }])).values());
  const subjectsForClass = teacherAssignments.filter(ta => ta.classId === watchClassId).map(ta => ({ id: ta.subjectId, name: ta.subjectName }));

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

  const onSubmit = async (data: AssignmentForm) => {
    setIsSaving(true);
    try {
      const assignment = await assignmentsService.create({
        ...data,
        pdfUrl: pdfBase64 || undefined,
        deadline: new Date(data.deadline).toISOString(),
      });
      toast.success(data.status === 'PUBLISHED' ? 'Assignment published successfully!' : 'Assignment saved as draft');
      router.push(`/teacher/assignments/${assignment.id}`);
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to create assignment');
    } finally { setIsSaving(false); }
  };

  // Min date: tomorrow
  const minDate = new Date();
  minDate.setDate(minDate.getDate() + 1);
  const minDateStr = minDate.toISOString().slice(0, 16);

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href="/teacher/assignments" className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">Create Assignment</h1>
          <p className="text-sm text-slate-500">Fill in the details below</p>
        </div>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="card p-6 space-y-5">
        <div>
          <label className="form-label">Title *</label>
          <input {...register('title')} className={`form-input ${errors.title ? 'form-input-error' : ''}`} placeholder="e.g. Algebra Problem Set" />
          {errors.title && <p className="form-error">{(errors.title.message as string)}</p>}
        </div>

        <div>
          <label className="form-label">Description *</label>
          <textarea {...register('description')} rows={4} className={`form-textarea ${errors.description ? 'form-input-error' : ''}`} placeholder="Describe the assignment, instructions, and any resources..." />
          {errors.description && <p className="form-error">{(errors.description.message as string)}</p>}
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="form-label">Class *</label>
            <select {...register('classId')} className={`form-select ${errors.classId ? 'form-input-error' : ''}`}>
              <option value="">Select class...</option>
              {uniqueClasses.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
            {errors.classId && <p className="form-error">{(errors.classId.message as string)}</p>}
          </div>
          <div>
            <label className="form-label">Subject *</label>
            <select {...register('subjectId')} className={`form-select ${errors.subjectId ? 'form-input-error' : ''}`} disabled={!watchClassId}>
              <option value="">Select subject...</option>
              {subjectsForClass.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
            </select>
            {errors.subjectId && <p className="form-error">{(errors.subjectId.message as string)}</p>}
          </div>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="form-label">Deadline *</label>
            <input type="datetime-local" {...register('deadline')} min={minDateStr} className={`form-input ${errors.deadline ? 'form-input-error' : ''}`} />
            {errors.deadline && <p className="form-error">{(errors.deadline.message as string)}</p>}
          </div>
          <div>
            <label className="form-label">Maximum Marks *</label>
            <input type="number" {...register('maxMarks')} min={1} className={`form-input ${errors.maxMarks ? 'form-input-error' : ''}`} />
            {errors.maxMarks && <p className="form-error">{(errors.maxMarks.message as string)}</p>}
          </div>
        </div>

        <div>
          <label className="form-label">Attach Reference PDF (optional)</label>
          <input type="file" accept="application/pdf" onChange={handleFileChange} className="form-input" />
        </div>

        <div>
          <label className="form-label">Status</label>
          <div className="flex gap-4 mt-1">
            {(['DRAFT', 'PUBLISHED'] as const).map(s => (
              <label key={s} className="flex items-center gap-2 cursor-pointer">
                <input type="radio" {...register('status')} value={s} className="accent-blue-600" />
                <span className="text-sm font-medium text-slate-700">{s === 'DRAFT' ? '📝 Save as Draft' : '🚀 Publish Now'}</span>
              </label>
            ))}
          </div>
        </div>

        <div className="flex gap-3 pt-2 border-t border-slate-200">
          <Link href="/teacher/assignments" className="btn-secondary flex-1 text-center">Cancel</Link>
          <LoadingButton type="submit" isLoading={isSaving} className="flex-1">Create Assignment</LoadingButton>
        </div>
      </form>
    </div>
  );
}
