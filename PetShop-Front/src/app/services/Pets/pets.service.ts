import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PetsService {

  private readonly API = 'https://localhost:7103/api/v1/Pets';

  constructor(private http: HttpClient) {}

  listarTodos() {
    if (typeof window !== 'undefined') {
      console.log(this.API)
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get<any[]>(this.API, { headers });
    }

    console.log('aqui1')
    return of([]);
  }

}
