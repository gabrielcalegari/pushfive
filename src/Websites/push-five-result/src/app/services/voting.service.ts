import { Injectable } from '@angular/core';
import { HandleError } from './handle-error';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { VotingGetResult } from '../models/voting-get-result';
import { VoterGetResult } from '../models/voter-get-result';

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
        catchError(this.handleError('getResult', null))
      );
  }

  getVoterResult(pageIndex: number, pageSize: number): Observable<VoterGetResult> {

    const params = new HttpParams()
    .set('pageIndex', pageIndex.toString())
    .set('pageSize', pageSize.toString());

    return this.httpClient.get<VoterGetResult>(this.votingUrl + '/result/voters', { params })
    .pipe(
      catchError(this.handleError('getVoterResult', null))
    );
  }

}
