'use client';

import { useEffect, useState } from 'react';
import { Plus, Pencil, Trash2, UserPlus } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import toast from 'react-hot-toast';
import { User, UserRole } from '@/types';
import { usersService } from '@/services/users.service';
import { classesService } from '@/services/classes.service';
import { Class } from '@/types';
import { Modal, ConfirmDialog, EmptyState, TableSkeleton, SearchInput, RoleBadge, LoadingButton } from '@/components/ui';
import { format } from 'date-fns';

const userSchema = z.object({
  fullName: z.string().min(2, 'Full name is required'),
  email: z.string().email('Valid email is required'),
  password: z.string().optional(),
  role: z.enum(['ADMIN', 'TEACHER', 'STUDENT'] as const),
  phone: z.string().optional(),
  isActive: z.boolean(),
  classId: z.string().optional(),
});

type UserForm = z.infer<typeof userSchema>;

export default function AdminUsersPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [classes, setClasses] = useState<Class[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [roleFilter, setRoleFilter] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [editingUser, setEditingUser] = useState<User | null>(null);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  const { register, handleSubmit, reset, watch, formState: { errors } } = useForm<UserForm>({
    resolver: zodResolver(userSchema),
    defaultValues: { role: 'STUDENT', isActive: true },
  });

  const watchRole = watch('role');

  const load = async () => {
    try {
      const [u, c] = await Promise.all([usersService.getAll(), classesService.getAll()]);
      setUsers(u);
      setClasses(c);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => { load(); }, []);

  const filtered = users.filter(u => {
    const matchSearch = !search || u.fullName.toLowerCase().includes(search.toLowerCase()) || u.email.toLowerCase().includes(search.toLowerCase());
    const matchRole = !roleFilter || u.role === roleFilter;
    return matchSearch && matchRole;
  });

  const openCreate = () => { setEditingUser(null); reset({ role: 'STUDENT', isActive: true }); setShowModal(true); };
  const openEdit = (u: User) => { setEditingUser(u); reset({ fullName: u.fullName, email: u.email, role: u.role, phone: u.phone ?? '', isActive: u.isActive, classId: u.classId ?? '' }); setShowModal(true); };

  const onSubmit = async (data: UserForm) => {
    setIsSaving(true);
    try {
      if (editingUser) {
        await usersService.update(editingUser.id, { ...data, classId: data.role === 'STUDENT' ? data.classId : undefined });
        toast.success('User updated successfully');
      } else {
        if (!data.password) { toast.error('Password is required for new users'); return; }
        await usersService.create({ ...data, password: data.password!, classId: data.role === 'STUDENT' ? data.classId : undefined });
        toast.success('User created successfully');
      }
      setShowModal(false);
      load();
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Something went wrong');
    } finally {
      setIsSaving(false);
    }
  };

  const confirmDelete = async () => {
    if (!deleteId) return;
    setIsDeleting(true);
    try {
      await usersService.delete(deleteId);
      toast.success('User deleted');
      setDeleteId(null);
      load();
    } catch {
      toast.error('Failed to delete user');
    } finally {
      setIsDeleting(false);
    }
  };

  return (
    <div className="space-y-5">
      {/* Header */}
      <div className="page-header">
        <div>
          <h1 className="page-title">Users</h1>
          <p className="page-subtitle">{users.length} total users</p>
        </div>
        <button onClick={openCreate} className="btn-primary">
          <Plus className="w-4 h-4" /> Add User
        </button>
      </div>

      {/* Filters */}
      <div className="card p-4 flex flex-wrap gap-3 items-center">
        <SearchInput value={search} onChange={setSearch} placeholder="Search users..." />
        <select value={roleFilter} onChange={e => setRoleFilter(e.target.value)} className="form-select w-36">
          <option value="">All Roles</option>
          <option value="ADMIN">Admin</option>
          <option value="TEACHER">Teacher</option>
          <option value="STUDENT">Student</option>
        </select>
        <span className="text-sm text-slate-500 ml-auto">{filtered.length} result(s)</span>
      </div>

      {/* Table */}
      <div className="card">
        {isLoading ? (
          <TableSkeleton rows={6} cols={5} />
        ) : filtered.length === 0 ? (
          <EmptyState
            icon={<UserPlus className="w-6 h-6" />}
            title="No users found"
            description="Try adjusting your search or create a new user."
            action={<button onClick={openCreate} className="btn-primary"><Plus className="w-4 h-4" /> Add User</button>}
          />
        ) : (
          <div className="table-container">
            <table className="table">
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Email</th>
                  <th>Role</th>
                  <th>Status</th>
                  <th>Created</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filtered.map(u => (
                  <tr key={u.id}>
                    <td className="font-medium text-slate-900">{u.fullName}</td>
                    <td className="text-slate-600">{u.email}</td>
                    <td><RoleBadge role={u.role} /></td>
                    <td>
                      <span className={`badge ${u.isActive ? 'badge-green' : 'badge-red'}`}>
                        {u.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                    <td className="text-slate-500 text-xs">{format(new Date(u.createdAt), 'MMM d, yyyy')}</td>
                    <td>
                      <div className="flex gap-1">
                        <button onClick={() => openEdit(u)} className="btn-ghost p-1.5" aria-label="Edit user">
                          <Pencil className="w-3.5 h-3.5" />
                        </button>
                        <button onClick={() => setDeleteId(u.id)} className="p-1.5 text-red-500 hover:bg-red-50 rounded-lg transition-colors" aria-label="Delete user">
                          <Trash2 className="w-3.5 h-3.5" />
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {/* Modal */}
      <Modal isOpen={showModal} onClose={() => setShowModal(false)} title={editingUser ? 'Edit User' : 'Create User'}>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label className="form-label">Full Name *</label>
            <input {...register('fullName')} className={`form-input ${errors.fullName ? 'form-input-error' : ''}`} placeholder="John Smith" />
            {errors.fullName && <p className="form-error">{errors.fullName.message}</p>}
          </div>
          <div>
            <label className="form-label">Email *</label>
            <input {...register('email')} type="email" className={`form-input ${errors.email ? 'form-input-error' : ''}`} placeholder="john@school.com" />
            {errors.email && <p className="form-error">{errors.email.message}</p>}
          </div>
          {!editingUser && (
            <div>
              <label className="form-label">Password *</label>
              <input {...register('password')} type="password" className="form-input" placeholder="••••••••" />
            </div>
          )}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="form-label">Role *</label>
              <select {...register('role')} className="form-select">
                <option value="ADMIN">Admin</option>
                <option value="TEACHER">Teacher</option>
                <option value="STUDENT">Student</option>
              </select>
            </div>
            <div>
              <label className="form-label">Phone</label>
              <input {...register('phone')} className="form-input" placeholder="+1-555-0100" />
            </div>
          </div>
          {watchRole === 'STUDENT' && (
            <div>
              <label className="form-label">Class</label>
              <select {...register('classId')} className="form-select">
                <option value="">Select class...</option>
                {classes.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
              </select>
            </div>
          )}
          <div className="flex items-center gap-2">
            <input type="checkbox" {...register('isActive')} id="isActive" className="w-4 h-4 accent-blue-600" />
            <label htmlFor="isActive" className="text-sm font-medium text-slate-700">Active account</label>
          </div>
          <div className="flex gap-3 pt-2">
            <button type="button" onClick={() => setShowModal(false)} className="btn-secondary flex-1">Cancel</button>
            <LoadingButton type="submit" isLoading={isSaving} className="flex-1">
              {editingUser ? 'Update User' : 'Create User'}
            </LoadingButton>
          </div>
        </form>
      </Modal>

      <ConfirmDialog
        isOpen={!!deleteId}
        onClose={() => setDeleteId(null)}
        onConfirm={confirmDelete}
        title="Delete User"
        message="Are you sure you want to delete this user? This action cannot be undone."
        confirmLabel="Delete"
        isDestructive
        isLoading={isDeleting}
      />
    </div>
  );
}
