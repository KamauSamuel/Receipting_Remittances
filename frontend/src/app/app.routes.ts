import { Routes } from '@angular/router';
import { authGuard } from './core/auth/auth.guard';

export const routes: Routes = [
    { path: 'login', loadComponent: () => import('./features/auth/login/login').then((m) => m.Login) },
    { path: 'signup', loadComponent: () => import('./features/auth/signup/signup').then((m) => m.Signup) },
    {
        path: 'receipts',
        loadComponent: () => import('./features/receipts/receipt-view/receipt-view').then((m) => m.ReceiptView),
        canActivate: [authGuard],
    },
    { path: '', pathMatch: 'full', redirectTo: 'receipts' },
    { path: '**', redirectTo: 'receipts' },
];
