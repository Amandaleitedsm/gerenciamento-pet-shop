import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-clientes-view.component',
  imports: [ CommonModule, MatButtonModule, MatDialogModule ],
  templateUrl: './clientes-view.component.html',
  styleUrl: './clientes-view.component.css',
})

export class ClientesViewComponent {

  constructor(
    private dialogRef: MatDialogRef<ClientesViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ){}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
