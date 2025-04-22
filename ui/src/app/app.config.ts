import {NG_EVENT_PLUGINS} from "@taiga-ui/event-plugins";
import {provideAnimations} from "@angular/platform-browser/animations";
import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {provideHttpClient} from '@angular/common/http';
import {API_BASE_URL} from './bff-client/bff-client';

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: API_BASE_URL, useValue: "https://scheduleai-bff.nachert.art" },
    provideAnimations(),
    provideZoneChangeDetection({eventCoalescing: true}),
    provideRouter(routes),
    provideHttpClient(),
    NG_EVENT_PLUGINS]
};
