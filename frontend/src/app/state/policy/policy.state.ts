import { Injectable } from '@angular/core';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { PolicyService } from '../../services/policy.service';
import { Policy } from '../../models/Policy';
import { tap } from 'rxjs/operators';

// Actions
export class LoadPolicies {
  static readonly type = '[Policy] Load Policies';
}

export interface PolicyStateModel {
  policies: Policy[];
}

export class CreatePolicy {
  static readonly type = '[Policy] Create Policy';
  constructor(public payload: Policy) {}
}

export class UpdatePolicy {
  static readonly type = '[Policy] Update Policy';
  constructor(public payload: Policy) {}
}

export class DeletePolicy {
  static readonly type = '[Policy] Delete Policy';
  constructor(public id: number) {}
}

export class LoadOwnPolicies {
  static readonly type = '[Policy] Load Own Policies';
}

export class CancelOwnPolicy {
  static readonly type = '[Policy] Cancel Own Policy';
  constructor(public policyId: number) {}
}

@State<PolicyStateModel>({
  name: 'policy',
  defaults: {
    policies: []
  }
})
@Injectable()
export class PolicyState {
  constructor(private policyService: PolicyService) {}

  @Selector()
  static getPolicies(state: PolicyStateModel) {
    return state.policies;
  }

  @Action(LoadPolicies)
  loadPolicies(ctx: StateContext<PolicyStateModel>) {
    return this.policyService.getPolicies().pipe(
      tap(policies => {
        console.log('Polizas cargadas en el store:', policies);
        ctx.patchState({ policies });
      })
    );
  }

  @Action(CreatePolicy)
  createPolicy(ctx: StateContext<PolicyStateModel>, action: CreatePolicy) {
    return this.policyService.create(action.payload).pipe(
      tap((newPolicy) => {
        const state = ctx.getState();
        ctx.patchState({
          policies: [...state.policies, newPolicy]
        });
      })
    );
  }

  @Action(UpdatePolicy)
  updatePolicy(ctx: StateContext<PolicyStateModel>, action: UpdatePolicy) {
    return this.policyService.update(action.payload.id, action.payload).pipe(
      tap((updatedPolicy) => {
        const state = ctx.getState();
        const updatedList = state.policies.map(policy =>
          policy.id === updatedPolicy.id ? updatedPolicy : policy
        );
        ctx.patchState({
          policies: updatedList
        });
      })
    );
  }

  @Action(DeletePolicy)
  deletePolicy(ctx: StateContext<PolicyStateModel>, action: DeletePolicy) {
    return this.policyService.delete(action.id).pipe(
      tap(() => {
        const state = ctx.getState();
        ctx.patchState({
          policies: state.policies.filter(p => p.id !== action.id)
        });
      })
    );
  }

  @Action(LoadOwnPolicies)
  loadOwnPolicies(ctx: StateContext<PolicyStateModel>) {
    return this.policyService.getOwnPolicies().pipe(
      tap(policies => {
        ctx.patchState({ policies });
      })
    );
  }


  @Action(CancelOwnPolicy)
  cancelOwnPolicy(ctx: StateContext<PolicyStateModel>, action: CancelOwnPolicy) {
    console.log('[CancelOwnPolicy] Ejecutado con ID:', action.policyId);

    const current = ctx.getState().policies.find(p => p.id === action.policyId);
    if (!current) {
      console.error('Póliza no encontrada en el estado');
      return;
    }

    return this.policyService.CancelPolicy(action.policyId).pipe(
      tap(() => {
        const updatedPolicies = ctx.getState().policies.map(p =>
          p.id === action.policyId ? { ...p, status: 1 } : p
        );
        ctx.patchState({ policies: updatedPolicies });
      })
    );
  }

}
