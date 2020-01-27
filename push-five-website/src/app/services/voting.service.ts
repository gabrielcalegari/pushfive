import { Injectable } from '@angular/core';
import { HandleError } from './handle-error';
import { HttpClient } from '@angular/common/http';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { Observable } from 'rxjs';
import { Voting } from '../models/voting';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VotingService {

  private votingUrl = 'https://localhost:44353/votings';
  private handleError: HandleError;

  constructor(
    private httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandlerService) {
    this.handleError = httpErrorHandler.createHandleError('VotingService');
  }

  addVote(voting: Voting): Observable<Voting> {

    voting.items.forEach(i => i.songId = i.song.id);

    return this.httpClient.post<Voting>(this.votingUrl, voting)
      .pipe(
        catchError(this.handleError('addVote', voting))
      );
  }

}
