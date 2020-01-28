import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { SongGetResult } from '../models/song-get-result';
import { HandleError } from './handle-error';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { environment } from '../../environments/environment';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private catalogUrl = environment.catalogServiceUrl;
  private handleError: HandleError;

  constructor(
    private httpClient: HttpClient,
    httpErrorHandler: HttpErrorHandlerService) {
      this.handleError = httpErrorHandler.createHandleError('CatalogService');
    }

    getSongsById(songIds: Guid[]): Observable<SongGetResult> {

      let params = new HttpParams();
      songIds.forEach(id => {
        params = params.append('songIds', id.toString());
      });

      return this.httpClient.get<SongGetResult>(this.catalogUrl + '/ids', { params })
        .pipe(
          catchError(this.handleError('getSongs', null))
        );
    }
}
