import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./pages/login/login.component').then((m) => m.LoginComponent) },
  {
    path: '',
    loadComponent: () => import('./layout/main-layout/main-layout.component').then((m) => m.MainLayoutComponent),
    canActivate: [authGuard],
    children: [
      { path: 'home', loadComponent: () => import('./pages/home/home.component').then((m) => m.HomeComponent) },
      { path: 'comerciantes/nuevo', loadComponent: () => import('./pages/comerciante-form/comerciante-form.component').then((m) => m.ComercianteFormComponent) },
      { path: 'comerciantes/:id/editar', loadComponent: () => import('./pages/comerciante-form/comerciante-form.component').then((m) => m.ComercianteFormComponent) },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'home' }
];
