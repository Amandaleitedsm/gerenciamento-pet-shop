import { Component } from '@angular/core';
import { AuthService } from '../../services/Auth/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [FormsModule, CommonModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css',
})
export class AuthComponent {
  authData = { cpf: '', senha: ''}

  constructor(private authService: AuthService, private router: Router){}

  autenticar() {
    this.authService.login(this.authData).subscribe({
      next: (res) => {
        console.log('Login Sucesso!', res);
        localStorage.setItem('token', res.token); // Salva o JWT que você criou ontem
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('Erro no login', err);
        alert('Usuário ou senha inválidos!');
      }
    });
  }
}

