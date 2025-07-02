import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/Client';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  clients: Client[] = [];
  dato : Client = {
      address : "",
      email :"",
      id : 1,
      middleName: "",
      name : "",
      phone : "",
      surName:""
  };

  constructor(private clientService: ClientService) {}

  ngOnInit(): void {
    this.clientService.getAll().subscribe({
      next: (data) => this.clients = data,
      error: (err) => console.error('Error al obtener clientes:', err)
    });
  }

  deleteClient(id: number): void {
    if (confirm('¿Estás seguro de eliminar este cliente?')) {
      this.clientService.delete(id).subscribe(() => {
        this.clients = this.clients.filter(c => c.id !== id);
      });
    }
  }

  editClient(client: Client): void {
    console.log('Editar cliente:', client);
  }
}
