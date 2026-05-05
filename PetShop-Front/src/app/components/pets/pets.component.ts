import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PetsService } from '../../services/Pets/pets.service';
import { CommonModule} from '@angular/common';
import { ClientesService } from '../../services/Clientes/clientes.service';
import { PetsModalComponent } from './pets-modal/pets-modal.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-pets',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './pets.component.html',
  styleUrl: './pets.component.css',
})
export class PetsComponent implements OnInit {
  listaPets: any[] = []
  dadosCliente: any

  constructor(
    private dialog: MatDialog,
    private petsService: PetsService,
    private clientesService: ClientesService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.ListarPets();
  }

  ListarPets() {
    this.petsService.listarTodos().subscribe({
      next: (dados) => {
        this.listaPets = dados;
        console.log("Dados que chegaram:", dados);

        // 3. O SEGREDO: Avise o Angular para atualizar a tela agora!
        this.cdr.detectChanges();
      },
      error: (err) => console.log("Erro ao listar pets: ", err)
    });
  }

  selecionarCliente(clienteId: number) {
    this.clientesService.selecionarCliente(clienteId).subscribe({
      next: (dado) => {
        console.log("Dados que chegaram:", dado);
        this.dadosCliente = dado;

        // MOVA A ABERTURA PARA CÁ
        this.abrirModalComDados(dado);

        this.cdr.detectChanges();
      },
      error: (err) => console.log("Erro ao buscar cliente: ", err)
    });
  }

  // Crie uma função auxiliar para organizar melhor
  abrirModalComDados(cliente: any) {
    const dialogRef = this.dialog.open(PetsModalComponent, {
      width: '450px',
      disableClose: false,
      data: cliente // Passa o 'dado' que acabou de chegar da API
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('O modal foi fechado');
    });
  }

  // Agora sua função de clique fica simples:
  abrirDonoModal(clienteId: number) {
    this.selecionarCliente(clienteId);
  }
}
