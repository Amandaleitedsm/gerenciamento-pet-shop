// services/auth.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  // A porta 8080 é a que vimos no seu log do Docker
  private readonly API = 'https://localhost:7103/v1/Auth';

  constructor(private http: HttpClient) {}

  login(dados: any): Observable<any> {
    // O seu back-end espera um objeto com { cpf, senha }
    return this.http.post(this.API, dados);
  }
}
