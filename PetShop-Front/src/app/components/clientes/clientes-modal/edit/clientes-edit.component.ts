import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ClientesService } from '../../../../services/Clientes/clientes.service';

@Component({
  selector: 'app-clientes-edit.component',
  imports: [ CommonModule, MatButtonModule, MatDialogModule, FormsModule ],
  templateUrl: './clientes-edit.component.html',
  styleUrl: './clientes-edit.component.css',
})

export class ClientesEditComponent {

  constructor(
    private clientesService: ClientesService,
    private dialogRef: MatDialogRef<ClientesEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ){}

  submit(){
    this.clientesService.atualizarCliente(this.data.id, this.data).subscribe({
      next: (res) => {
        console.log("Cliente atualizado com sucesso!");
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
