import { inject, Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token: string | null = null;
  private readonly tokenKey = 'auth_token';
  private readonly refreshTokenKey = 'refresh_token';

  private http = inject(HttpClient);
  
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, JSON.stringify(token));
    localStorage.setItem('token_expiry', (Date.now() + 10 * 60 * 1000).toString());
  }

  async getTokenAsync(): Promise<Observable<string | null>> {

    const token = localStorage.getItem('authToken');
    if (token) {
      const tokenObject = JSON.parse(token);
    }
    return of(token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  saveTokenLocalStorage() {
    if (this.token) {
      localStorage.setItem(this.tokenKey, this.token);
      localStorage.setItem('token_expiry', (Date.now() + 10 * 60 * 1000).toString());

    }
  }












  private async refreshToken(): Promise<string> {
    return 'new_token_from_server';
  }
  // refreshToken(): Observable<string> {
  //   const refreshToken = localStorage.getItem(this.refreshTokenKey);
  //   if (!refreshToken) {
  //     this.clearToken();
  //     return of(''); 
  //   }

  //   return this.http.post<string>(`${environment.API_AUTH_URL}/refresh`, { refreshToken });
}

