import { State, Action, StateContext, Selector } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/Client';

// ------------------
// Acciones
// ------------------
export class LoadClients {
  static readonly type = '[Client] Load Clients';
}

export class CreateClient {
  static readonly type = '[Client] Create Client';
  constructor(public payload: Client) {}
}

export class UpdateClient {
  static readonly type = '[Client] Update Client';
  constructor(public payload: Client) {}
}

export class DeleteClient {
  static readonly type = '[Client] Delete Client';
  constructor(public id: number) {}
}

// ------------------
// Estado y Modelo
// ------------------
export interface ClientStateModel {
  clients: Client[];
}

@State<ClientStateModel>({
  name: 'client',
  defaults: {
    clients: []
  }
})
@Injectable()
export class ClientState {
  constructor(private clientService: ClientService) {}

  @Selector()
  static getClients(state: ClientStateModel) {
    return state.clients;
  }

  @Action(LoadClients)
  loadClients(ctx: StateContext<ClientStateModel>) {
    return this.clientService.getAll().pipe(
      tap(clients => ctx.patchState({ clients }))
    );
  }

  @Action(CreateClient)
  createClient(ctx: StateContext<ClientStateModel>, action: CreateClient) {
    return this.clientService.create(action.payload).pipe(
      tap((newClient) => {
        const state = ctx.getState();
        ctx.patchState({
          clients: [...state.clients, newClient]
        });
      })
    );
  }

  @Action(UpdateClient)
  updateClient(ctx: StateContext<ClientStateModel>, action: UpdateClient) {
    return this.clientService.update(action.payload.id, action.payload).pipe(
      tap((updatedClient) => {
        const state = ctx.getState();
        const updatedList = state.clients.map(client =>
          client.id === updatedClient.id ? updatedClient : client
        );
        ctx.patchState({
          clients: updatedList
        });
      })
    );
  }

  @Action(DeleteClient)
  deleteClient(ctx: StateContext<ClientStateModel>, action: DeleteClient) {
    return this.clientService.delete(action.id).pipe(
      tap(() => {
        const state = ctx.getState();
        ctx.patchState({
          clients: state.clients.filter(c => c.id !== action.id)
        });
      })
    );
  }
}
