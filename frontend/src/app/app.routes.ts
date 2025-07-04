import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { AdminComponent } from './pages/admin/admin.component';
import { ClientComponent } from './pages/client/client.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [authGuard],
    data: { role: 'Administrador' }
  },
  {
    path: 'client',
    component: ClientComponent,
    canActivate: [authGuard],
    data: { role: 'Cliente' }
  },
  { path: '**', redirectTo: 'login' }
];
