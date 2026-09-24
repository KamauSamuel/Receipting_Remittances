import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface AccessTokensResponse {
    accessToken: string;
    refreshToken: string;
}

interface LoginRequest {
    email: string;
    password: string;
}

interface RegisterRequest {
    email: string;
    firstName: string;
    lastName: string;
    password: string;
}

const ACCESS_TOKEN_KEY = 'accessToken';
const REFRESH_TOKEN_KEY = 'refreshToken';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly http = inject(HttpClient);

    private readonly accessToken = signal<string | null>(localStorage.getItem(ACCESS_TOKEN_KEY));

    readonly isAuthenticated = computed(() => this.accessToken() !== null);

    getAccessToken(): string | null {
        return this.accessToken();
    }

    login(email: string, password: string): Observable<AccessTokensResponse> {
        const request: LoginRequest = { email, password };

        return this.http
            .post<AccessTokensResponse>(`${environment.apiUrl}/users/login`, request)
            .pipe(tap((tokens) => this.setTokens(tokens)));
    }

    register(email: string, firstName: string, lastName: string, password: string): Observable<string> {
        const request: RegisterRequest = { email, firstName, lastName, password };

        return this.http.post<string>(`${environment.apiUrl}/users/register`, request);
    }

    logout(): void {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        localStorage.removeItem(REFRESH_TOKEN_KEY);
        this.accessToken.set(null);
    }

    private setTokens(tokens: AccessTokensResponse): void {
        localStorage.setItem(ACCESS_TOKEN_KEY, tokens.accessToken);
        localStorage.setItem(REFRESH_TOKEN_KEY, tokens.refreshToken);
        this.accessToken.set(tokens.accessToken);
    }
}
