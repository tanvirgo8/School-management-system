'use client';

import { useEffect, useState } from 'react';
import { Plus, Trash2, UserCheck } from 'lucide-react';
import { useForm } from 'react-hook-form';
import toast from 'react-hot-toast';
import { TeacherAssignment, User, Class, Subject } from '@/types';
import { teacherAssignmentsService, usersService } from '@/services/users.service';
import { classesService } from '@/services/classes.service';
import { subjectsService } from '@/services/subjects.service';
import { Modal, ConfirmDialog, EmptyState, TableSkeleton, LoadingButton } from '@/components/ui';

type TAForm = { teacherId: string; classId: string; subjectId: string };

export default function AdminTeacherAssignmentsPage() {
  const [assignments, setAssignments] = useState<TeacherAssignment[]>([]);
  const [teachers, setTeachers] = useState<User[]>([]);
  const [classes, setClasses] = useState<Class[]>([]);
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  const { register, handleSubmit, reset } = useForm<TAForm>();

  const load = async () => {
    try {
      const [ta, users, cls, subs] = await Promise.all([
        teacherAssignmentsService.getAll(),
        usersService.getAll({ role: 'TEACHER' }),
        classesService.getAll(),
        subjectsService.getAll(),
      ]);
      setAssignments(ta); setTeachers(users); setClasses(cls); setSubjects(subs);
    } finally { setIsLoading(false); }
  };
  useEffect(() => { load(); }, []);

  const onSubmit = async (data: TAForm) => {
    setIsSaving(true);
    try {
      await teacherAssignmentsService.create(data);
      toast.success('Teacher assigned successfully');
      setShowModal(false); reset(); load();
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to create assignment');
    } finally { setIsSaving(false); }
  };

  const confirmDelete = async () => {
    if (!deleteId) return;
    setIsDeleting(true);
    try { await teacherAssignmentsService.delete(deleteId); toast.success('Assignment removed'); setDeleteId(null); load(); }
    catch { toast.error('Failed to remove assignment'); }
    finally { setIsDeleting(false); }
  };

  return (
    <div className="space-y-5">
      <div className="page-header">
        <div><h1 className="page-title">Teacher Assignments</h1><p className="page-subtitle">Manage who teaches what</p></div>
        <button onClick={() => { reset(); setShowModal(true); }} className="btn-primary"><Plus className="w-4 h-4" /> Assign Teacher</button>
      </div>

      <div className="card">
        {isLoading ? <TableSkeleton rows={5} cols={4} /> : assignments.length === 0 ? (
          <EmptyState icon={<UserCheck className="w-6 h-6" />} title="No assignments yet" description="Assign teachers to classes and subjects." action={<button onClick={() => setShowModal(true)} className="btn-primary"><Plus className="w-4 h-4" /> Assign Teacher</button>} />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Teacher</th><th>Class</th><th>Subject</th><th>Actions</th></tr></thead>
              <tbody>
                {assignments.map(ta => (
                  <tr key={ta.id}>
                    <td>
                      <div>
                        <p className="font-medium text-slate-900">{ta.teacherName}</p>
                        <p className="text-xs text-slate-500">{ta.teacherEmail}</p>
                      </div>
                    </td>
                    <td className="text-slate-700">{ta.className}</td>
                    <td>
                      <div>
                        <span className="text-slate-900">{ta.subjectName}</span>
                        <span className="badge badge-blue ml-2 font-mono">{ta.subjectCode}</span>
                      </div>
                    </td>
                    <td>
                      <button onClick={() => setDeleteId(ta.id)} className="p-1.5 text-red-500 hover:bg-red-50 rounded-lg transition-colors"><Trash2 className="w-3.5 h-3.5" /></button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      <Modal isOpen={showModal} onClose={() => setShowModal(false)} title="Assign Teacher">
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label className="form-label">Teacher *</label>
            <select {...register('teacherId', { required: true })} className="form-select">
              <option value="">Select teacher...</option>
              {teachers.map(t => <option key={t.id} value={t.id}>{t.fullName} ({t.email})</option>)}
            </select>
          </div>
          <div>
            <label className="form-label">Class *</label>
            <select {...register('classId', { required: true })} className="form-select">
              <option value="">Select class...</option>
              {classes.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
          </div>
          <div>
            <label className="form-label">Subject *</label>
            <select {...register('subjectId', { required: true })} className="form-select">
              <option value="">Select subject...</option>
              {subjects.map(s => <option key={s.id} value={s.id}>{s.name} ({s.code})</option>)}
            </select>
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" onClick={() => setShowModal(false)} className="btn-secondary flex-1">Cancel</button>
            <LoadingButton type="submit" isLoading={isSaving} className="flex-1">Assign</LoadingButton>
          </div>
        </form>
      </Modal>

      <ConfirmDialog isOpen={!!deleteId} onClose={() => setDeleteId(null)} onConfirm={confirmDelete} title="Remove Assignment" message="Are you sure you want to remove this teacher assignment?" confirmLabel="Remove" isDestructive isLoading={isDeleting} />
    </div>
  );
}
