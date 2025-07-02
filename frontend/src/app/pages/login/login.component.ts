import { Component } from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {Router} from '@angular/router';
import {FormsModule} from '@angular/forms';
import { AuthService } from '../../services/auth.service';

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

  constructor(private authService: AuthService, private router: Router, private http: HttpClient) {

  }

  login() {
    this.http.post<any>('https://localhost:7046/api/Auth/login', {
      email: this.email,
      password: this.password
    }).subscribe({
      next: (res) => {
        this.authService.setToken(res.token);

        const role = this.authService.getUserRole();
        if (role === 'Administrador') {
          this.router.navigate(['/admin']);
        } else if (role === 'Cliente') {
          this.router.navigate(['/client']);
        } else {
          this.error = 'Rol desconocido.';
        }
      },
      error: () => {
        this.error = 'Credenciales inválidas.';
      }
    });
  }

}
