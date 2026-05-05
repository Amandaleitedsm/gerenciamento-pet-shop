import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ClientesService } from '../../services/Clientes/clientes.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon'
import { ClientesViewComponent } from './clientes-modal/view/clientes-view.component';
import { MatDialog } from '@angular/material/dialog';
import { ClientesEditComponent } from './clientes-modal/edit/clientes-edit.component';
import { ClientesCreateComponent } from './clientes-modal/create/clientes-create.component';

@Component({
  selector: 'app-clientes',
  imports: [CommonModule, FormsModule, MatIconModule],
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.css',
})
export class ClientesComponent implements OnInit {
  listaClientes: any[] = []
  dadosCliente: any
  tipoModal: any

  constructor(
    private clientesService: ClientesService,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.ListarClientes();
  }

  ListarClientes() {
    this.clientesService.listarTodos().subscribe({
      next: (dados) => {
        this.listaClientes = dados;
        console.log("Dados que chegaram:", dados);

        // 3. O SEGREDO: Avise o Angular para atualizar a tela agora!
        this.cdr.detectChanges();
      },
      error: (err) => console.log("Erro ao listar pets: ", err)
    });
  }

  relatorio(clienteId: number) {
    this.clientesService.relatorioPorCliente(clienteId).subscribe({
      next: (dado) => {
        console.log("Dados que chegaram:", dado);
        this.dadosCliente = dado[0];
        this.abrirModalComDados(this.dadosCliente);

        this.cdr.detectChanges();
      },
      error: (err) => console.log("Erro ao buscar cliente: ", err)
    });
  }

  listarCliente(clienteId: number){
    this.clientesService.selecionarCliente(clienteId).subscribe({
      next: (dado) => {
        console.log("Dado:", dado);
        this.dadosCliente = dado;
        this.abrirModalComDados(this.dadosCliente);

        this.cdr.detectChanges();
      },
      error: (err) => console.log("Erro ao buscar cliente: ", err)
    });
  }

  abrirModalSemDados(){
    const dialogRef = this.dialog.open(ClientesCreateComponent, {
      width: '450px',
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('O modal foi fechado');
      this.ListarClientes()
    });
  }

  abrirModalComDados(cliente: any) {
    const dialogRef = this.dialog.open(this.tipoModal, {
      width: '450px',
      disableClose: false,
      data: cliente // Passa o 'dado' que acabou de chegar da API
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('O modal foi fechado');
      this.ListarClientes()
    });
  }

  abrirModalCliente(clienteId: number, func: string) {
    if (func == 'view') {
      this.tipoModal = ClientesViewComponent
      this.relatorio(clienteId);
    }
    else if (func == 'edit'){
      this.tipoModal = ClientesEditComponent
      this.listarCliente(clienteId)
    }
  }
}

