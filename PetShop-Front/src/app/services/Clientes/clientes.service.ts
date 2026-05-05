import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ClientesService {

  private readonly API = 'https://localhost:7103/api/v1/Clientes';

  constructor(private http: HttpClient) {}

  listarTodos() {
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get<any[]>(this.API, { headers });
    }
    return of([]);
  }

  selecionarCliente(clienteId: number){
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get<any>(this.API + '/' + clienteId, { headers });
    }

    return of([]);
  }

  relatorioPorCliente(clienteId: number){
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get<any>(this.API + '/relatorio/' + clienteId, { headers });
    }

    return of([]);
  }

  atualizarCliente(clienteId: number, data: any){
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.put<any>(this.API + '/' + clienteId, data, { headers });
    }

    return of([]);
  }

  cadastrarCliente(data: any){
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.post<any>(this.API, data, { headers });
    }

    return of([]);
  }
}
