import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient , withInterceptors} from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { PolicyState } from './state/policy/policy.state';
import { ClientState } from './state/client/client.state';
import { routes } from './app.routes';
import {provideStore} from '@ngxs/store';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([AuthInterceptor])),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideStore([PolicyState, ClientState])
  ]
};
