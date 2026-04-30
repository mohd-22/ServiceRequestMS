import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { RequestService } from 'src/app/Services/request.service';
import { AttachmentDto } from 'src/app/Models/Request';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-attachments',
  templateUrl: './attachments.component.html',
  styleUrls: ['./attachments.component.css']
})
export class AttachmentsComponent implements OnChanges {
  @Input() requestId: string | null = null;
  @Input() isVisible = false;
  @Output() closeModal = new EventEmitter<void>();

  attachments: AttachmentDto[] = [];
  isLoading = false;
  errorMessage = '';
  deletingAttachmentId: string | null = null;

  constructor(private requestService: RequestService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if ((changes['isVisible'] && this.isVisible) || (changes['requestId'] && this.requestId && this.isVisible)) {
      if (this.requestId) {
        this.loadAttachments(this.requestId);
      }
    }
    if (changes['isVisible'] && !this.isVisible) {
      // reset when closed
      this.attachments = [];
      this.errorMessage = '';
    }
  }

  private loadAttachments(requestId: string): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.requestService.getAttachmentsByRequest(requestId).subscribe({
      next: (data) => {
        this.attachments = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading attachments:', error);
        this.errorMessage = 'Could not load attachments.';
        this.isLoading = false;
      }
    });
  }

  getAttachmentUrl(attachment: AttachmentDto): string {
    const root = environment.apiUrl.replace(/\/api\/?$/i, '');
    return `${root}/${attachment.filePath}`;
  }

  downloadAttachment(attachment: AttachmentDto, event?: MouseEvent): void {
    event?.preventDefault();

    this.requestService.downloadAttachment(attachment.id).subscribe({
      next: (blob) => {
        const objectUrl = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = objectUrl;
        link.download = attachment.fileName || 'attachment';
        link.click();
        window.URL.revokeObjectURL(objectUrl);
      },
      error: (error) => {
        console.error('Error downloading attachment:', error);
        this.errorMessage = 'Could not download attachment.';
      }
    });
  }

  deleteAttachment(attachment: AttachmentDto): void {
    if (!confirm('Are you sure you want to delete this attachment?')) {
      return;
    }

    this.deletingAttachmentId = attachment.id;
    this.errorMessage = '';
    this.requestService.deleteAttachment(attachment.id).subscribe({
      next: () => {
        this.attachments = this.attachments.filter(a => a.id !== attachment.id);
        this.deletingAttachmentId = null;
      },
      error: (error) => {
        console.error('Error deleting attachment:', error);
        this.errorMessage = 'Could not delete attachment.';
        this.deletingAttachmentId = null;
      }
    });
  }

  close(): void {
    this.closeModal.emit();
  }
}
