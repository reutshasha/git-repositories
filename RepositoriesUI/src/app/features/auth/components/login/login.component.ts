import { Component, inject } from '@angular/core';
import { ApiService } from '../../../../core/services/api.service';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AngularMaterialModule } from '../../../../angular-material.module';
import { FormsModule } from '@angular/forms';
import { AuthInterceptor } from '../../../../core/services/auth.interceptor';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { SnackBarUtil } from '../../../../shared/utilities/snack-bar.util';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
  imports: [AngularMaterialModule, FormsModule, RouterModule, CommonModule, HttpClientModule],

})
export class LoginComponent {
  username = '';
  password = '';

  private apiService = inject(ApiService);
  private authService = inject(AuthService);
  private router = inject(Router);
  private snackBar = inject(SnackBarUtil);

  onSubmit(username: string, password: string) {
    this.apiService.login(username, password).subscribe({
      next: (token) => {
        this.authService.setToken(token);
        this.snackBar.show("Login successful!");
        this.router.navigate(['/repository-search']);
      },
      error: (err) => {
        this.snackBar.show("Login Failed ");
      },
    });
  }
}
