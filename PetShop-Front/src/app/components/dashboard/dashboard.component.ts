import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Console } from 'console';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  constructor(private router: Router){}

  redirect(rota: string){
    console.log('/' + rota)
    if (localStorage.getItem('token')){
      this.router.navigate(['/' + rota]);
    }
  }
}
