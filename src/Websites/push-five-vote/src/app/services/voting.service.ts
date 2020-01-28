import { Injectable } from '@angular/core';
import { HandleError } from './handle-error';
import { HttpClient } from '@angular/common/http';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { Observable } from 'rxjs';
import { Voting } from '../models/voting';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

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

  addVote(voting: Voting): Observable<any> {

    voting.items.forEach(i => i.songId = i.song.id);

    return this.httpClient.post<Voting>(this.votingUrl, voting, {observe: 'response'})
      .pipe(
        catchError(this.handleError('addVote', voting))
      );
  }

}
