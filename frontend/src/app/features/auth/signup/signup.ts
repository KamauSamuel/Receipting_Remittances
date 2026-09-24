import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
    selector: 'app-signup',
    imports: [FormsModule, RouterLink],
    templateUrl: './signup.html',
    styleUrl: './signup.scss',
})
export class Signup {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    protected email = '';
    protected firstName = '';
    protected lastName = '';
    protected password = '';
    protected readonly isSubmitting = signal(false);
    protected readonly errorMessage = signal<string | null>(null);

    protected onSubmit(): void {
        this.errorMessage.set(null);
        this.isSubmitting.set(true);

        this.authService.register(this.email, this.firstName, this.lastName, this.password).subscribe({
            next: () => this.signInAfterRegister(),
            error: () => {
                this.isSubmitting.set(false);
                this.errorMessage.set('Unable to create an account with those details.');
            },
        });
    }

    // Registration doesn't return tokens, so sign the new user in right after.
    private signInAfterRegister(): void {
        this.authService.login(this.email, this.password).subscribe({
            next: () => {
                this.isSubmitting.set(false);
                this.router.navigateByUrl('/receipts');
            },
            error: () => {
                this.isSubmitting.set(false);
                this.router.navigateByUrl('/login');
            },
        });
    }
}
