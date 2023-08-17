import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service'
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationForm!: FormGroup;
  error: string = '';
  submitted = false;
  loading: boolean = false;
  passwordsMatching: boolean = true;

  constructor(
    private fb: FormBuilder,
    private AuthService: AuthService,  
    private route: ActivatedRoute,
    private router: Router) { }

  get f() { return this.registrationForm.controls; }

  passwordMatchingValidator(): boolean  {
    const password = this.f['password'].value;
    const confirmPassword = this.f['confirmPassword'].value;
    return password === confirmPassword;
  };

  
  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?(\d{12})$/)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/
      )]],
      confirmPassword: ['', [Validators.required]],
    });
  }

  onSubmit(): void{
    this.submitted = true;

    this.error = '';

    this.passwordsMatching = this.passwordMatchingValidator();
    if (this.registrationForm.invalid || !this.passwordsMatching) {
        return;
    }
    this.loading = true;

    const userData = {
      firstName: this.f['firstName'].value, 
      lastName: this.f['lastName'].value, 
      email: this.f['email'].value, 
      phoneNumber: this.f['phoneNumber'].value,
      password: this.f['password'].value,
      confirmPassword: this.f['confirmPassword'].value,
    }

    this.AuthService.register(userData)
        .pipe(first())
        .subscribe({
            next: () => {
                const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
                this.router.navigateByUrl(returnUrl);
            },
            error: error => {
                this.error = error;
                this.loading = false;
            }
      });
  }
}
