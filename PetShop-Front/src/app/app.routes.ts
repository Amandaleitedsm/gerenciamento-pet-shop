import { DashboardComponent } from './components/dashboard/dashboard.component';
import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { PetsComponent } from './components/pets/pets.component';

export const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'clientes', component: ClientesComponent },
  { path: 'pets', component: PetsComponent }
];
