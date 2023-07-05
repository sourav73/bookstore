import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css'],
})
export class AdminLoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {}
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onLogin(loginForm: FormGroup) {
    this.authService.login(loginForm['value']).subscribe({
      next: (res: any) => {
        this.errorMessage = '';
        localStorage.setItem('token', res.data.refreshToken);
      },
      error: (err: any) => {
        this.errorMessage = err.error.message;
        // console.log(err.error.message);
      },
    });
  }
}
