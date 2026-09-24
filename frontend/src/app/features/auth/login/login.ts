import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
    selector: 'app-login',
    imports: [FormsModule, RouterLink],
    templateUrl: './login.html',
    styleUrl: './login.scss',
})
export class Login {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    protected email = '';
    protected password = '';
    protected readonly isSubmitting = signal(false);
    protected readonly errorMessage = signal<string | null>(null);

    protected onSubmit(): void {
        this.errorMessage.set(null);
        this.isSubmitting.set(true);

        this.authService.login(this.email, this.password).subscribe({
            next: () => {
                this.isSubmitting.set(false);
                this.router.navigateByUrl('/receipts');
            },
            error: () => {
                this.isSubmitting.set(false);
                this.errorMessage.set('Invalid email or password.');
            },
        });
    }
}
