import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

export type HandleError =
  <T> (operation?: string, result?: T) => (error: HttpErrorResponse) => Observable<T>;
