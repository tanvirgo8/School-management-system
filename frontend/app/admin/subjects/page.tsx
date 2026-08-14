'use client';

import { useEffect, useState } from 'react';
import { Plus, Pencil, Trash2, FlaskConical } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { Subject } from '@/types';
import { subjectsService } from '@/services/subjects.service';
import { classesService } from '@/services/classes.service';
import { Modal, ConfirmDialog, EmptyState, TableSkeleton, LoadingButton } from '@/components/ui';

const subjectSchema = z.object({
  name: z.string().min(1, 'Subject name is required'),
  code: z.string().min(1, 'Subject code is required'),
  description: z.string().optional(),
  classId: z.string().min(1, 'Class is required'),
  isActive: z.boolean(),
});
type SubjectForm = z.infer<typeof subjectSchema>;

export default function AdminSubjectsPage() {
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editing, setEditing] = useState<Subject | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  const [classes, setClasses] = useState<any[]>([]);

  const { register, handleSubmit, reset, formState: { errors } } = useForm<SubjectForm>({
    resolver: zodResolver(subjectSchema),
    defaultValues: { isActive: true },
  });

  const load = async () => {
    try {
      const [sList, cList] = await Promise.all([subjectsService.getAll(), classesService.getAll()]);
      setSubjects(sList);
      setClasses(cList);
    } finally {
      setIsLoading(false);
    }
  };
  useEffect(() => { load(); }, []);

  const openCreate = () => { setEditing(null); reset({ classId: '', isActive: true }); setShowModal(true); };
  const openEdit = (s: Subject) => { setEditing(s); reset({ name: s.name, code: s.code, description: s.description ?? '', classId: s.classId, isActive: s.isActive }); setShowModal(true); };

  const onSubmit = async (data: SubjectForm) => {
    setIsSaving(true);
    try {
      if (editing) { await subjectsService.update(editing.id, data); toast.success('Subject updated'); }
      else { await subjectsService.create(data); toast.success('Subject created'); }
      setShowModal(false); load();
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to save subject');
    } finally { setIsSaving(false); }
  };

  const confirmDelete = async () => {
    if (!deleteId) return;
    setIsDeleting(true);
    try { await subjectsService.delete(deleteId); toast.success('Subject deleted'); setDeleteId(null); load(); }
    catch { toast.error('Failed to delete subject'); }
    finally { setIsDeleting(false); }
  };

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Subjects</h1><p className="page-subtitle">{subjects.length} subjects</p></div>
        <button onClick={openCreate} className="btn-primary"><Plus className="w-4 h-4" /> Add Subject</button>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={4} /> : subjects.length === 0 ? (
          <EmptyState icon={<FlaskConical className="w-6 h-6" />} title="No subjects yet" action={<button onClick={openCreate} className="btn-primary"><Plus className="w-4 h-4" /> Add Subject</button>} />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Name</th><th>Code</th><th>Class</th><th>Description</th><th>Status</th><th>Actions</th></tr></thead>
              <tbody>
                {subjects.map(s => (
                  <tr key={s.id}>
                    <td className="font-medium text-slate-900">{s.name}</td>
                    <td><span className="badge badge-blue font-mono">{s.code}</span></td>
                    <td className="text-slate-700 font-medium text-sm">{s.className || '—'}</td>
                    <td className="text-slate-500 text-sm">{s.description ?? '—'}</td>
                    <td><span className={`badge ${s.isActive ? 'badge-green' : 'badge-red'}`}>{s.isActive ? 'Active' : 'Inactive'}</span></td>
                    <td>
                      <div className="flex gap-1">
                        <button onClick={() => openEdit(s)} className="btn-ghost p-1.5"><Pencil className="w-3.5 h-3.5" /></button>
                        <button onClick={() => setDeleteId(s.id)} className="p-1.5 text-red-500 hover:bg-red-50 rounded-lg transition-colors"><Trash2 className="w-3.5 h-3.5" /></button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      <Modal isOpen={showModal} onClose={() => setShowModal(false)} title={editing ? 'Edit Subject' : 'Create Subject'}>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="form-label">Name *</label>
              <input {...register('name')} className={`form-input ${errors.name ? 'form-input-error' : ''}`} placeholder="Mathematics" />
              {errors.name && <p className="form-error">{errors.name.message}</p>}
            </div>
            <div>
              <label className="form-label">Code *</label>
              <input {...register('code')} className={`form-input ${errors.code ? 'form-input-error' : ''}`} placeholder="MATH101" />
              {errors.code && <p className="form-error">{errors.code.message}</p>}
            </div>
          </div>
          <div>
            <label className="form-label">Class *</label>
            <select {...register('classId')} className={`form-select ${errors.classId ? 'form-input-error' : ''}`}>
              <option value="">Select a Class</option>
              {classes.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
            {errors.classId && <p className="form-error">{errors.classId.message}</p>}
          </div>
          <div>
            <label className="form-label">Description</label>
            <textarea {...register('description')} className="form-textarea" rows={2} />
          </div>
          <div className="flex items-center gap-2">
            <input type="checkbox" {...register('isActive')} id="subActive" className="w-4 h-4 accent-blue-600" />
            <label htmlFor="subActive" className="text-sm font-medium text-slate-700">Active</label>
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" onClick={() => setShowModal(false)} className="btn-secondary flex-1">Cancel</button>
            <LoadingButton type="submit" isLoading={isSaving} className="flex-1">{editing ? 'Update' : 'Create'}</LoadingButton>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={confirmDelete} title="Delete Subject" message="Are you sure? This cannot be undone." confirmLabel="Delete" isDestructive isLoading={isDeleting} />
    </div>
  );
}
