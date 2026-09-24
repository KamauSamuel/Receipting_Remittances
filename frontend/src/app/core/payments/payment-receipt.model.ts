export type PaymentDirection = 'Incoming' | 'Outgoing';

export interface PaymentReceipt {
    paymentId: string;
    payee: string;
    amount: number;
    currency: string;
    paymentDate: string;
    bankAccount: string;
    remittanceClassification: string;
    paymentPartner: string;
    direction: PaymentDirection;
    receiptNumber: string;
    generatedAtUtc: string;
}
