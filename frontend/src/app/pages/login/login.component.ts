import { Component } from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {Router} from '@angular/router';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule,HttpClientModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    this.http.post<any>('https://localhost:7046/api/Auth/login', {
      email: this.email,
      password: this.password
    }).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);

        // Decodificar JWT (simplificado) para obtener el rol
        const payload = JSON.parse(atob(res.token.split('.')[1]));
        const role = payload.role;

        if (role === 'Administrador') {
          this.router.navigate(['/admin']);
        } else if (role === 'Cliente') {
          this.router.navigate(['/client']);
        } else {
          this.error = 'Rol desconocido.';
        }
      },
      error: (err) => {
        this.error = 'Credenciales inválidas';
        console.error(err);
      }
    });
  }

}
