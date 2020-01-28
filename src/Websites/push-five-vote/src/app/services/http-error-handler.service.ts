import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerService {

  constructor(private toastr: ToastrService) { }

  createHandleError = (serviceName = '') => <T>
    (operation = 'operation', result = {} as T) => this.handleError(serviceName, operation, result);

  handleError<T>(serviceName = '', operation = 'operation', result = {} as T) {

    return (error: HttpErrorResponse): Observable<T> => {

      if (error.error != null) {
        for (const [key, value] of Object.entries(error.error)) {
          if (value instanceof Array) {
            for (const message of value) {
              this.toastr.error(message);
            }
          }
        }
      } else {
        this.toastr.error('Oops! An error ocurred');
      }

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning a safe result.
      throw error;
    };
  }

}
