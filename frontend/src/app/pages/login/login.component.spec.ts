import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { LoginComponent } from './login.component';
import { AuthService } from '../../core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    authSpy = jasmine.createSpyObj('AuthService', ['login']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [
        LoginComponent,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatCheckboxModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: AuthService, useValue: authSpy },
        { provide: Router, useValue: routerSpy },
        { provide: MatSnackBar, useValue: { open: jasmine.createSpy() } }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have invalid form when empty', () => {
    expect(component.form.valid).toBeFalse();
  });

  it('should require terms acceptance', () => {
    component.form.patchValue({
      correo: 'test@test.com',
      contrasena: 'pass123',
      aceptarTerminos: false
    });
    expect(component.form.valid).toBeFalse();
  });

  it('should call login on valid submit', () => {
    authSpy.login.and.returnValue(of({ success: true, data: { token: 'x', nombre: 'Test', rol: 'Admin', expiraEn: '' } }));
    component.form.patchValue({
      correo: 'admin@comercio.com',
      contrasena: 'Admin123!',
      aceptarTerminos: true
    });
    component.onSubmit();
    expect(authSpy.login).toHaveBeenCalledWith({ correo: 'admin@comercio.com', contrasena: 'Admin123!' });
  });
});
