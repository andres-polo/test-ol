import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface LoginRequest {
  correo: string;
  contrasena: string;
}

export interface LoginResponse {
  token: string;
  nombre: string;
  rol: string;
  expiraEn: string;
}

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'auth_token';
  private readonly userKey = 'auth_user';

  private token = signal<string | null>(this.getStoredToken());
  private user = signal<{ nombre: string; rol: string } | null>(this.getStoredUser());

  isAuthenticated = computed(() => !!this.token());
  currentUser = computed(() => this.user());
  isAdmin = computed(() => this.user()?.rol === 'Administrador');

  constructor(private http: HttpClient, private router: Router) {}

  login(request: LoginRequest): Observable<ApiResponse<LoginResponse>> {
    return this.http.post<ApiResponse<LoginResponse>>(`${environment.apiUrl}/api/auth/login`, request).pipe(
      tap((res) => {
        if (res.success && res.data) {
          this.setSession(res.data);
        }
      })
    );
  }

  logout(): void {
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem(this.userKey);
    this.token.set(null);
    this.user.set(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return this.token();
  }

  private setSession(data: LoginResponse): void {
    sessionStorage.setItem(this.tokenKey, data.token);
    sessionStorage.setItem(this.userKey, JSON.stringify({ nombre: data.nombre, rol: data.rol }));
    this.token.set(data.token);
    this.user.set({ nombre: data.nombre, rol: data.rol });
  }

  private getStoredToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }

  private getStoredUser(): { nombre: string; rol: string } | null {
    const stored = sessionStorage.getItem(this.userKey);
    return stored ? JSON.parse(stored) : null;
  }
}
