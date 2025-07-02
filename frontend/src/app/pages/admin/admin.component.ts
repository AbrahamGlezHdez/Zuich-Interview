import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/Client';
import { Policy } from '../../models/Policy';
import { Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { DeletePolicy, CreatePolicy, UpdatePolicy, LoadPolicies, PolicyState } from '../../state/policy/policy.state';
import { CreateClient, UpdateClient, DeleteClient, LoadClients, ClientState } from '../../state/client/client.state';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  clients: Client[] = [];

  form: Partial<Client> = {};
  editingClient: boolean = false;

  policyForm: Partial<Policy> = {};
  editingPolicy: boolean = false;



  get policies$(): Observable<Policy[]> {
    return this.store.select(PolicyState.getPolicies);
  }

  get clients$(): Observable<Client[]> {
    return this.store.select(ClientState.getClients);
  }

  constructor(private clientService: ClientService, private store: Store) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const payload = token ? JSON.parse(atob(token.split('.')[1])) : null;

    if (payload?.role !== 'Administrador') {
      return;
    }

    // ✅ Solo admins cargan clientes y pólizas
    this.store.dispatch(new LoadClients());
    this.store.dispatch(new LoadPolicies());
    this.store.select(ClientState.getClients).subscribe(clients => {
      this.clients = clients;
    });

  }

  deleteClient(id: number): void {
    if (confirm('¿Estás seguro de eliminar este cliente?')) {
      this.store.dispatch(new DeleteClient(id));
    }
  }

  submitClient(): void {
    if (this.editingClient && this.form.id) {
      this.store.dispatch(new UpdateClient(this.form as Client));
    } else {
      this.store.dispatch(new CreateClient(this.form as Client));
    }

    this.resetForm();
  }

  editClient(client: Client): void {
    this.form = { ...client };
    this.editingClient = true;
  }

  cancelEdit(): void {
    this.resetForm();
  }

  resetForm(): void {
    this.form = {};
    this.editingClient = false;
  }

  submitPolicy(): void {
    if (this.editingPolicy && this.policyForm.id) {
      this.store.dispatch(new UpdatePolicy(this.policyForm as Policy));
    } else {
      this.store.dispatch(new CreatePolicy(this.policyForm as Policy));
    }

    this.resetPolicyForm();
  }

  editPolicy(policy: Policy): void {
    this.policyForm = { ...policy };
    this.editingPolicy = true;
  }

  cancelPolicyEdit(): void {
    this.resetPolicyForm();
  }

  resetPolicyForm(): void {
    this.policyForm = {};
    this.editingPolicy = false;
  }

  deletePolicy(id: number): void {
    if (confirm('¿Eliminar esta póliza?')) {
      this.store.dispatch(new DeletePolicy(id));
    }
  }

  //---------Helpers-------
  getClientNameById(id: number): string {
    const client = this.clients.find(c => c.id === id);
    return client ? `${client.name} ${client.surName}` : 'Desconocido';
  }

  getPolicyTypeLabel(type: any): string {
    const value = Number(type);
    switch (value) {
      case 0: return 'Vida';
      case 1: return 'Auto';
      case 2: return 'Salud';
      case 3: return 'Hogar';
      default: return 'Desconocido';
    }
  }

  getPolicyStatusLabel(status: any): string {
    const value = Number(status);
    switch (value) {
      case 0: return 'Activa';
      case 1: return 'Cancelada';
      default: return 'Desconocido';
    }
  }




}
