'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { ArrowLeft } from 'lucide-react';
import Link from 'next/link';
import { Assignment, TeacherAssignment } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { teacherAssignmentsService } from '@/services/users.service';
import { useAuth } from '@/hooks/useAuth';
import { LoadingButton } from '@/components/ui';
import { format } from 'date-fns';

const schema = z.object({
  title: z.string().min(1, 'Title is required'),
  description: z.string().min(1, 'Description is required'),
  classId: z.string().min(1, 'Class is required'),
  subjectId: z.string().min(1, 'Subject is required'),
  deadline: z.string().min(1, 'Deadline is required'),
  maxMarks: z.coerce.number().min(1, 'Max marks must be > 0'),
  status: z.enum(['DRAFT', 'PUBLISHED', 'CLOSED'] as const),
});
type Form = z.infer<typeof schema>;

export default function EditAssignmentPage() {
  const { id } = useParams<{ id: string }>();
  const { user } = useAuth();
  const router = useRouter();
  const [assignment, setAssignment] = useState<Assignment | null>(null);
  const [teacherAssignments, setTeacherAssignments] = useState<TeacherAssignment[]>([]);
  const [isSaving, setIsSaving] = useState(false);
  const [pdfBase64, setPdfBase64] = useState<string>('');

  const { register, handleSubmit, reset, watch, formState: { errors } } = useForm<any>({
    resolver: zodResolver(schema),
  });

  const watchClassId = watch('classId');

  useEffect(() => {
    const load = async () => {
      const [a, ta] = await Promise.all([
        assignmentsService.getById(id),
        user ? teacherAssignmentsService.getAll(user.id) : Promise.resolve([]),
      ]);
      if (a) {
        setAssignment(a);
        const deadlineLocal = format(new Date(a.deadline), "yyyy-MM-dd'T'HH:mm");
        reset({ title: a.title, description: a.description, classId: a.classId, subjectId: a.subjectId, deadline: deadlineLocal, maxMarks: a.maxMarks, status: a.status as 'DRAFT' | 'PUBLISHED' | 'CLOSED' });
      }
      setTeacherAssignments(ta);
    };
    load();
  }, [id, user, reset]);

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

  const onSubmit = async (data: Form) => {
    setIsSaving(true);
    try {
      await assignmentsService.update(id, { 
        ...data, 
        pdfUrl: pdfBase64 || assignment?.pdfUrl,
        deadline: new Date(data.deadline).toISOString() 
      });
      toast.success('Assignment updated successfully');
      router.push(`/teacher/assignments/${id}`);
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to update assignment');
    } finally { setIsSaving(false); }
  };

  if (!assignment) return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;

  return (
    <div className="max-w-2xl mx-auto space-y-6">
      <div className="flex items-center gap-3">
        <Link href={`/teacher/assignments/${id}`} className="btn-ghost p-2"><ArrowLeft className="w-4 h-4" /></Link>
        <div>
          <h1 className="text-xl font-bold text-slate-900">Edit Assignment</h1>
          <p className="text-sm text-slate-500 truncate">{assignment.title}</p>
        </div>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="card p-6 space-y-5">
        <div>
          <label className="form-label">Title *</label>
          <input {...register('title')} className={`form-input ${errors.title ? 'form-input-error' : ''}`} />
          {errors.title && <p className="form-error">{(errors.title.message as string)}</p>}
        </div>
        <div>
          <label className="form-label">Description *</label>
          <textarea {...register('description')} rows={5} className={`form-textarea ${errors.description ? 'form-input-error' : ''}`} />
          {errors.description && <p className="form-error">{(errors.description.message as string)}</p>}
        </div>
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="form-label">Class *</label>
            <select {...register('classId')} className="form-select">
              <option value="">Select class...</option>
              {uniqueClasses.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
          </div>
          <div>
            <label className="form-label">Subject *</label>
            <select {...register('subjectId')} className="form-select">
              <option value="">Select subject...</option>
              {subjectsForClass.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
            </select>
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="form-label">Deadline *</label>
            <input type="datetime-local" {...register('deadline')} className="form-input" />
          </div>
          <div>
            <label className="form-label">Maximum Marks *</label>
            <input type="number" {...register('maxMarks')} min={1} className="form-input" />
          </div>
        </div>
        {assignment.pdfUrl && (
          <div className="bg-slate-50 border border-slate-200 rounded-lg p-3 text-sm text-slate-600 flex items-center justify-between">
            <span>📎 Existing PDF Attached</span>
            <a href={assignment.pdfUrl} target="_blank" rel="noreferrer" className="text-blue-600 hover:underline font-semibold">View PDF</a>
          </div>
        )}
        <div>
          <label className="form-label">Replace / Attach PDF (optional)</label>
          <input type="file" accept="application/pdf" onChange={handleFileChange} className="form-input" />
        </div>
        <div>
          <label className="form-label">Status</label>
          <select {...register('status')} className="form-select w-48">
            <option value="DRAFT">Draft</option>
            <option value="PUBLISHED">Published</option>
            <option value="CLOSED">Closed</option>
          </select>
        </div>
        <div className="flex gap-3 pt-2 border-t border-slate-200">
          <Link href={`/teacher/assignments/${id}`} className="btn-secondary flex-1 text-center">Cancel</Link>
          <LoadingButton type="submit" isLoading={isSaving} className="flex-1">Save Changes</LoadingButton>
        </div>
      </form>
    </div>
  );
}
