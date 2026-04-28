import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { CommentReadDto, CreateCommentDto } from 'src/app/Models';
import { CommentService } from 'src/app/Services/comment.service';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit, OnChanges {
  @Input() requestId: string = '';
  @Input() isVisible: boolean = false;
  @Output() closeModal = new EventEmitter<void>();

  comments: CommentReadDto[] = [];
  newCommentText: string = '';
  isLoading = false;
  isSubmitting = false;
  errorMessage = '';
  userRole: string | null = null;
  userId: string | null = null;
  deletingCommentId: string | null = null;

  constructor(
    private commentService: CommentService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    this.userId = this.authService.getUserId();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['isVisible'] && this.isVisible && this.requestId) {
      this.loadComments();
    }
  }

  loadComments(): void {
    if (!this.requestId) return;

    this.isLoading = true;
    this.errorMessage = '';

    this.commentService.getAllComments(this.requestId).subscribe({
      next: (comments) => {
        this.comments = comments;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = 'Could not load comments.';
        console.error('Error loading comments:', error);
      }
    });
  }

  canAddComment(): boolean {
    return this.userRole === 'Staff' || this.userRole === 'Employee';
  }

  submitComment(): void {
    if (!this.canAddComment() || !this.newCommentText.trim() || this.isSubmitting) {
      return;
    }

    const comment: CreateCommentDto = {
      text: this.newCommentText.trim(),
      requestId: this.requestId
    };

    this.isSubmitting = true;
    this.errorMessage = '';

    this.commentService.createComment(comment).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.newCommentText = '';
        this.loadComments(); // Reload comments to show the new one
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = 'Could not add comment.';
        console.error('Error adding comment:', error);
      }
    });
  }

  close(): void {
    this.closeModal.emit();
    this.comments = [];
    this.newCommentText = '';
    this.errorMessage = '';
  }

  formatDate(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toLocaleString();
  }

  getRoleBadgeClass(role: string): string {
    switch (role?.toLowerCase()) {
      case 'admin':
        return 'bg-danger';
      case 'manager':
        return 'bg-warning text-dark';
      case 'staff':
        return 'bg-info text-dark';
      case 'employee':
        return 'bg-success';
      default:
        return 'bg-secondary';
    }
  }

  canDeleteComment(comment: CommentReadDto): boolean {
    return this.userId === comment.userId || this.userRole === 'Admin';
  }

  deleteComment(commentId: string): void {
    if (!confirm('Are you sure you want to delete this comment?')) {
      return;
    }

    this.deletingCommentId = commentId;
    this.errorMessage = '';

    this.commentService.deleteComment(commentId).subscribe({
      next: () => {
        this.deletingCommentId = null;
        this.loadComments();
      },
      error: (error) => {
        this.deletingCommentId = null;
        this.errorMessage = 'Could not delete comment.';
        console.error('Error deleting comment:', error);
      }
    });
  }

  isDeleteLoading(commentId: string): boolean {
    return this.deletingCommentId === commentId;
  }
}
