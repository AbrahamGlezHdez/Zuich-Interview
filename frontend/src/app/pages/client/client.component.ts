import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Client } from '../../models/Client';
import { Policy } from '../../models/Policy';
import { Store } from '@ngxs/store';
import { ClientService } from '../../services/client.service';
import { PolicyService } from '../../services/policy.service';
import { LoadOwnPolicies,CancelOwnPolicy } from '../../state/policy/policy.state';
import { PolicyState } from "../../state/policy/policy.state"
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {
  client: Client = {} as Client;
  policies: Policy[] = [];

  editForm: Partial<Client> = {};
  editing: boolean = false;

  constructor(
    private clientService: ClientService,
    private policyService: PolicyService,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.clientService.getOwnClient().subscribe(client => {
      this.client = client;
      this.editForm.phone = client.phone;
      this.editForm.address = client.address;

      this.store.dispatch(new LoadOwnPolicies());
      this.store.select(PolicyState.getPolicies).subscribe(policies => {
        this.policies = policies.filter(p => p.clientId === client.id);
      });
    });
  }


  saveChanges(): void {
    const payload = {
      phone: this.editForm.phone!,
      address: this.editForm.address!
    };

    this.clientService.updateSelf(payload).subscribe(updated => {
      this.client = updated;
      this.editing = false;
    });
  }

  cancelEdit(): void {
    this.editing = false;
    this.editForm.phone = this.client.phone;
    this.editForm.address = this.client.address;
  }

  cancelPolicy(policy: Policy): void {
    console.log('Cancelar póliza', policy);
    if (confirm('¿Cancelar esta póliza?')) {
      this.store.dispatch(new CancelOwnPolicy(policy.id));
    }
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
