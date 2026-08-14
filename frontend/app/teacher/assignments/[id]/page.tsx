'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import Link from 'next/link';
import toast from 'react-hot-toast';
import { ArrowLeft, Pencil, Trash2, Share2, Users, Calendar, Award } from 'lucide-react';
import { Assignment, Submission } from '@/types';
import { assignmentsService } from '@/services/assignments.service';
import { submissionsService } from '@/services/submissions.service';
import { AssignmentStatusBadge, SubmissionStatusBadge, ConfirmDialog } from '@/components/ui';
import { format, isPast } from 'date-fns';

export default function TeacherAssignmentDetailPage() {
  const { id } = useParams<{ id: string }>();
  const router = useRouter();
  const [assignment, setAssignment] = useState<Assignment | null>(null);
  const [submissions, setSubmissions] = useState<Submission[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showDeleteDialog, setShowDeleteDialog] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const [isPublishing, setIsPublishing] = useState(false);

  useEffect(() => {
    const load = async () => {
      const [a, s] = await Promise.all([
        assignmentsService.getById(id),
        submissionsService.getAll({ assignmentId: id }),
      ]);
      setAssignment(a); setSubmissions(s);
      setIsLoading(false);
    };
    load();
  }, [id]);

  const handlePublish = async () => {
    setIsPublishing(true);
    try {
      const updated = await assignmentsService.publish(id);
      setAssignment(updated);
      toast.success('Assignment published successfully!');
    } catch (e: unknown) {
      const err = e as { response?: { data?: { message?: string } } };
      toast.error(err?.response?.data?.message ?? 'Failed to publish');
    } finally { setIsPublishing(false); }
  };

  const handleDelete = async () => {
    setIsDeleting(true);
    try {
      await assignmentsService.delete(id);
      toast.success('Assignment deleted');
      router.push('/teacher/assignments');
    } catch { toast.error('Failed to delete'); }
    finally { setIsDeleting(false); }
  };

  if (isLoading) {
    return <div className="flex items-center justify-center h-64"><div className="w-8 h-8 spinner" /></div>;
  }
  if (!assignment) return <div className="text-center py-16 text-slate-500">Assignment not found.</div>;

  const isDeadlinePast = isPast(new Date(assignment.deadline));

  return (
    <div className="max-w-4xl mx-auto space-y-6">
      {/* Header */}
      <div className="flex items-start gap-4">
        <Link href="/teacher/assignments" className="btn-ghost p-2 mt-1"><ArrowLeft className="w-4 h-4" /></Link>
        <div className="flex-1 min-w-0">
          <div className="flex items-center gap-3 mb-1">
            <AssignmentStatusBadge status={assignment.status} />
            {isDeadlinePast && <span className="badge badge-red">Deadline Passed</span>}
          </div>
          <h1 className="text-2xl font-bold text-slate-900">{assignment.title}</h1>
          <p className="text-sm text-slate-500 mt-1">{assignment.className} · {assignment.subjectName}</p>
        </div>
        <div className="flex gap-2 shrink-0">
          {assignment.status === 'DRAFT' && (
            <button onClick={handlePublish} disabled={isPublishing} className="btn-success">
              {isPublishing ? <span className="w-4 h-4 spinner" /> : <><Share2 className="w-4 h-4" /> Publish</>}
            </button>
          )}
          <Link href={`/teacher/assignments/${id}/edit`} className="btn-secondary"><Pencil className="w-4 h-4" /> Edit</Link>
          <button onClick={() => setShowDeleteDialog(true)} className="btn-danger"><Trash2 className="w-4 h-4" /></button>
        </div>
      </div>

      <div className="grid grid-cols-3 gap-4">
        <div className="card p-4 flex items-center gap-3">
          <Calendar className="w-5 h-5 text-blue-500" />
          <div>
            <p className="text-xs text-slate-500">Deadline</p>
            <p className={`text-sm font-semibold ${isDeadlinePast ? 'text-red-600' : 'text-slate-900'}`}>{format(new Date(assignment.deadline), 'MMM d, yyyy HH:mm')}</p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <Award className="w-5 h-5 text-purple-500" />
          <div>
            <p className="text-xs text-slate-500">Max Marks</p>
            <p className="text-sm font-semibold text-slate-900">{assignment.maxMarks}</p>
          </div>
        </div>
        <div className="card p-4 flex items-center gap-3">
          <Users className="w-5 h-5 text-emerald-500" />
          <div>
            <p className="text-xs text-slate-500">Submissions</p>
            <p className="text-sm font-semibold text-slate-900">{assignment.submissionCount}</p>
          </div>
        </div>
      </div>

      <div className="card p-6 space-y-4">
        <div>
          <h2 className="font-semibold text-slate-900 mb-3">Description</h2>
          <p className="text-slate-700 whitespace-pre-wrap text-sm leading-relaxed">{assignment.description}</p>
        </div>
        {assignment.pdfUrl && (
          <div className="bg-blue-50 border border-blue-200 rounded-lg p-3 flex items-center justify-between">
            <span className="text-sm text-blue-800 font-medium">📎 Attached Reference PDF</span>
            <a href={assignment.pdfUrl} target="_blank" rel="noreferrer" className="btn-primary py-1 px-3 text-xs">View PDF</a>
          </div>
        )}
      </div>

      {/* Submissions */}
      <div className="card">
        <div className="px-6 py-4 border-b border-slate-200">
          <h2 className="font-semibold text-slate-900">Submissions ({submissions.length})</h2>
        </div>
        {submissions.length === 0 ? (
          <div className="p-8 text-center text-slate-400">
            <Users className="w-8 h-8 mx-auto mb-2 opacity-40" />
            <p className="text-sm">No submissions yet.</p>
          </div>
        ) : (
          <div className="table-container">
            <table className="table">
              <thead><tr><th>Student</th><th>Submitted At</th><th>Marks</th><th>Status</th><th>Action</th></tr></thead>
              <tbody>
                {submissions.map(s => (
                  <tr key={s.id}>
                    <td>
                      <div>
                        <p className="font-medium text-slate-900">{s.studentName}</p>
                        <p className="text-xs text-slate-500">{s.studentEmail}</p>
                      </div>
                    </td>
                    <td className="text-slate-500 text-sm">{format(new Date(s.submittedAt), 'MMM d, HH:mm')}</td>
                    <td>
                      {s.marks != null
                        ? <span className="font-semibold text-slate-900">{s.marks}/{assignment.maxMarks}</span>
                        : <span className="text-slate-400">—</span>
                      }
                    </td>
                    <td><SubmissionStatusBadge status={s.status} /></td>
                    <td>
                      <Link href={`/teacher/submissions/${s.id}`} className="btn-ghost py-1 px-2 text-xs text-blue-600">
                        {s.status === 'GRADED' ? 'View' : 'Grade'}
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      <ConfirmDialog isOpen={showDeleteDialog} onClose={() => setShowDeleteDialog(false)} onConfirm={handleDelete} title="Delete Assignment" message="Are you sure you want to delete this assignment? All submissions will also be removed. This cannot be undone." confirmLabel="Delete Assignment" isDestructive isLoading={isDeleting} />
    </div>
  );
}
