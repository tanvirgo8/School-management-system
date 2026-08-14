'use client';

import { useEffect, useState } from 'react';
import { Plus, Pencil, Trash2, Building2 } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { Class } from '@/types';
import { classesService } from '@/services/classes.service';
import { Modal, ConfirmDialog, EmptyState, TableSkeleton, LoadingButton } from '@/components/ui';
import { format } from 'date-fns';

const classSchema = z.object({
  name: z.string().min(1, 'Class name is required'),
  description: z.string().optional(),
  tuitionFee: z.coerce.number().min(0, 'Tuition fee cannot be negative'),
  isActive: z.boolean(),
});
type ClassForm = z.infer<typeof classSchema>;

export default function AdminClassesPage() {
  const [classes, setClasses] = useState<Class[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editing, setEditing] = useState<Class | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  const { register, handleSubmit, reset, formState: { errors } } = useForm<any>({
    resolver: zodResolver(classSchema),
    defaultValues: { tuitionFee: 0, isActive: true },
  });

  const load = async () => {
    try { setClasses(await classesService.getAll()); } finally { setIsLoading(false); }
  };
  useEffect(() => { load(); }, []);

  const openCreate = () => { setEditing(null); reset({ tuitionFee: 0, isActive: true }); setShowModal(true); };
  const openEdit = (c: Class) => { setEditing(c); reset({ name: c.name, description: c.description ?? '', tuitionFee: c.tuitionFee, isActive: c.isActive }); setShowModal(true); };

  const onSubmit = async (data: ClassForm) => {
    setIsSaving(true);
    try {
      if (editing) { await classesService.update(editing.id, data); toast.success('Class updated'); }
      else { await classesService.create(data); toast.success('Class created'); }
      setShowModal(false); load();
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to save class');
    } finally { setIsSaving(false); }
  };

  const confirmDelete = async () => {
    if (!deleteId) return;
    setIsDeleting(true);
    try { await classesService.delete(deleteId); toast.success('Class deleted'); setDeleteId(null); load(); }
    catch { toast.error('Failed to delete class'); }
    finally { setIsDeleting(false); }
  };

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Classes</h1><p className="page-subtitle">{classes.length} classes</p></div>
        <button onClick={openCreate} className="btn-primary"><Plus className="w-4 h-4" /> Add Class</button>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={4} cols={4} /> : classes.length === 0 ? (
          <EmptyState icon={<Building2 className="w-6 h-6" />} title="No classes yet" description="Create your first class to get started." action={<button onClick={openCreate} className="btn-primary"><Plus className="w-4 h-4" /> Add Class</button>} />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Name</th><th>Description</th><th>Tuition Fee</th><th>Students</th><th>Status</th><th>Created</th><th>Actions</th></tr></thead>
              <tbody>
                {classes.map(c => (
                  <tr key={c.id}>
                    <td className="font-medium text-slate-900">{c.name}</td>
                    <td className="text-slate-500 text-sm">{c.description ?? '—'}</td>
                    <td className="font-semibold text-slate-900">${c.tuitionFee}</td>
                    <td><span className="badge badge-blue">{c.studentCount} students</span></td>
                    <td><span className={`badge ${c.isActive ? 'badge-green' : 'badge-red'}`}>{c.isActive ? 'Active' : 'Inactive'}</span></td>
                    <td className="text-slate-500 text-xs">{format(new Date(c.createdAt), 'MMM d, yyyy')}</td>
                    <td>
                      <div className="flex gap-1">
                        <button onClick={() => openEdit(c)} className="btn-ghost p-1.5"><Pencil className="w-3.5 h-3.5" /></button>
                        <button onClick={() => setDeleteId(c.id)} className="p-1.5 text-red-500 hover:bg-red-50 rounded-lg transition-colors"><Trash2 className="w-3.5 h-3.5" /></button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      <Modal isOpen={showModal} onClose={() => setShowModal(false)} title={editing ? 'Edit Class' : 'Create Class'}>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label className="form-label">Class Name *</label>
            <input {...register('name')} className={`form-input ${errors.name ? 'form-input-error' : ''}`} placeholder="e.g. Class 10" />
            {errors.name && <p className="form-error">{(errors.name.message as string)}</p>}
          </div>
          <div>
            <label className="form-label">Description</label>
            <textarea {...register('description')} className="form-textarea" rows={2} placeholder="Optional description..." />
          </div>
          <div>
            <label className="form-label">Tuition Fee ($) *</label>
            <input type="number" {...register('tuitionFee')} className={`form-input ${errors.tuitionFee ? 'form-input-error' : ''}`} placeholder="e.g. 1500" />
            {errors.tuitionFee && <p className="form-error">{(errors.tuitionFee.message as string)}</p>}
          </div>
          <div className="flex items-center gap-2">
            <input type="checkbox" {...register('isActive')} id="classActive" className="w-4 h-4 accent-blue-600" />
            <label htmlFor="classActive" className="text-sm font-medium text-slate-700">Active</label>
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" onClick={() => setShowModal(false)} className="btn-secondary flex-1">Cancel</button>
            <LoadingButton type="submit" isLoading={isSaving} className="flex-1">{editing ? 'Update' : 'Create'}</LoadingButton>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={confirmDelete} title="Delete Class" message="Are you sure? This cannot be undone." confirmLabel="Delete" isDestructive isLoading={isDeleting} />
    </div>
  );
}
