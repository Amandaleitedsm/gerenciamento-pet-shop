import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ClientesService } from '../../../../services/Clientes/clientes.service';

@Component({
  selector: 'app-clientes-create.component',
  imports: [ CommonModule, MatButtonModule, MatDialogModule, FormsModule ],
  templateUrl: './clientes-create.component.html',
  styleUrl: './clientes-create.component.css',
})
export class ClientesCreateComponent {
  data: any = {
    nome: '',
    cpf: '',
    dataNascimento: ''
  }
  constructor(
    private clientesService: ClientesService,
    private dialogRef: MatDialogRef<ClientesCreateComponent>
  ){}

  submit(){
    if (!this.data.nome || this.data.nome.trim() === '') {
      alert("O nome é obrigatório!");
      return;
    }
    if (!this.data.cpf || this.data.cpf.trim() === '') {
      alert("O cpf é obrigatório!");
      return;
    }
    this.clientesService.cadastrarCliente(this.data).subscribe({
      next: (res) => {
        console.log("Cliente criado com sucesso!");
        this.dialogRef.close(true); // Fecha o modal e avisa que deu certo
      },
      error: (err) => {
        console.error("Erro ao atualizar:", err);
      }
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
