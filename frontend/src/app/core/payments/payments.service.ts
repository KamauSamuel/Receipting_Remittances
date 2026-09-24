import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PaymentReceipt } from './payment-receipt.model';

@Injectable({ providedIn: 'root' })
export class PaymentsService {
    private readonly http = inject(HttpClient);

    getReceipt(paymentId: string): Observable<PaymentReceipt> {
        return this.http.get<PaymentReceipt>(`${environment.apiUrl}/payments/${paymentId}/receipt`);
    }

    downloadReceiptPdf(paymentId: string): Observable<Blob> {
        return this.http.get(`${environment.apiUrl}/payments/${paymentId}/receipt/pdf`, {
            responseType: 'blob',
        });
    }
}
