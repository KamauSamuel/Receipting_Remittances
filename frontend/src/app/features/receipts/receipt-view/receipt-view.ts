import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PaymentReceipt } from '../../../core/payments/payment-receipt.model';
import { PaymentsService } from '../../../core/payments/payments.service';

@Component({
    selector: 'app-receipt-view',
    imports: [FormsModule, DatePipe],
    templateUrl: './receipt-view.html',
    styleUrl: './receipt-view.scss',
})
export class ReceiptView {
    private readonly paymentsService = inject(PaymentsService);

    protected paymentId = '';
    protected readonly receipt = signal<PaymentReceipt | null>(null);
    protected readonly errorMessage = signal<string | null>(null);
    protected readonly isLoading = signal(false);
    protected readonly isDownloading = signal(false);

    protected onViewReceipt(): void {
        if (!this.paymentId) {
            return;
        }

        this.errorMessage.set(null);
        this.receipt.set(null);
        this.isLoading.set(true);

        this.paymentsService.getReceipt(this.paymentId).subscribe({
            next: (receipt) => {
                this.isLoading.set(false);
                this.receipt.set(receipt);
            },
            error: () => {
                this.isLoading.set(false);
                this.errorMessage.set('No receipt was found for that payment id.');
            },
        });
    }

    protected onDownloadPdf(): void {
        if (!this.paymentId) {
            return;
        }

        this.errorMessage.set(null);
        this.isDownloading.set(true);

        this.paymentsService.downloadReceiptPdf(this.paymentId).subscribe({
            next: (blob) => {
                this.isDownloading.set(false);
                this.triggerDownload(blob, `receipt-${this.paymentId}.pdf`);
            },
            error: () => {
                this.isDownloading.set(false);
                this.errorMessage.set('Unable to generate the receipt PDF for that payment id.');
            },
        });
    }

    // Downloads a blob response by simulating a click on a temporary anchor element.
    private triggerDownload(blob: Blob, fileName: string): void {
        const url = URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = fileName;
        anchor.click();
        URL.revokeObjectURL(url);
    }
}
