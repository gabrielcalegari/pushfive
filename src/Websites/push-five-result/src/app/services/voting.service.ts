import { Injectable } from '@angular/core';
import { HandleError } from './handle-error';
import { HttpClient } from '@angular/common/http';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { VotingGetResult } from '../models/voting-get-result';

@Injectable({
  providedIn: 'root'
})
export class VotingService {

  private votingUrl = environment.votingServiceUrl;
  private handleError: HandleError;

  constructor(
    private httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandlerService) {
    this.handleError = httpErrorHandler.createHandleError('VotingService');
  }

  getResult(): Observable<VotingGetResult> {

    return this.httpClient.get<VotingGetResult>(this.votingUrl + '/result')
      .pipe(
        catchError(this.handleError('addVote', null))
      );
  }

}
