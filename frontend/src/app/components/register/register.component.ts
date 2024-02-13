import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { RegisterUser } from '../../models/userModels/registerUser.model';
import { AuthUserService } from '../../services/authservices/auth-user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ FormsModule, ReactiveFormsModule,CommonModule, RouterModule ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registrationForm: FormGroup;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthUserService
  ) {
    this.registrationForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      dob: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.validatePassword]],
    });
  }

  validatePassword(control: any) {
    const regex = /^(?=.*[a-zA-Z0-9])(?=.*[^a-zA-Z0-9]).{8,}$/;
    return regex.test(control.value) ? null : { invalidPassword: true };
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      const user: RegisterUser = this.registrationForm.value;
      user.dob = new Date("2024-02-06T03:49:15.166Z");

      this.authService.registerUser(user).subscribe({
        next: (value) => {
          console.log('Registration successful: ' + value);
          this.router.navigate(['/login']);
        },
        error: (error) => {
          console.error('Error: ' + error);
        },
      });
    }
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}