import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from '../../models/userModels/loginUser.model';
import { AuthUserService } from '../../services/authservices/auth-user.service';
import { PostService } from '../../services/postservices/post.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent  {
  loginForm: FormGroup;
  showPassword: boolean = false;
  showErrorMessage: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthUserService,
    private postService: PostService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const formData: LoginRequest = this.loginForm.value;

      this.authService.loginUser(formData).subscribe({
        next: (value) => {
          console.log('Login successful:', value);
          this.postService.setAccessToken(value.token);
          this.router.navigate(['/blogposts']);
        },
        error: (error) => {
          console.error('Login failed:', error);
          //this.router.navigate(['/register']);
          this.showErrorMessage = true;
        },
      });
    }
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}