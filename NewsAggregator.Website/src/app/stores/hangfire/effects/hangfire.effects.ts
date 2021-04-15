import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { completeExtractArticles, completeExtractRecommendations, completeSearchJobs, completeSearchJobStates, errorExtractArticles, errorExtractRecommendations, errorSearchJobs, errorSearchJobStates, startExtractArticles, startExtractRecommendations, startSearchJobs, startSearchJobStates } from '../actions/hangfire.actions';
import { HangfireService } from '../services/hangfire.service';

@Injectable()
export class HangfireEffects {
  constructor(
    private actions$: Actions,
    private hangfireService: HangfireService
  ) { }

  @Effect()
  searchHangfireJobs$ = this.actions$
    .pipe(
      ofType(startSearchJobs),
      mergeMap((evt) => {
        return this.hangfireService.searchHangfireJobs(evt.startIndex, evt.count)
          .pipe(
            map(content => completeSearchJobs({ content: content })),
            catchError(() => of(errorSearchJobs()))
          );
      }
      )
  );

  @Effect()
  searchHangfireJobStates$ = this.actions$
    .pipe(
      ofType(startSearchJobStates),
      mergeMap((evt) => {
        return this.hangfireService.searchHangfireJobStates(evt.startIndex, evt.count, evt.jobId)
          .pipe(
            map(content => completeSearchJobStates({ content: content })),
            catchError(() => of(errorSearchJobStates()))
          );
      }
      )
  );

  @Effect()
  extractArticles$ = this.actions$
    .pipe(
      ofType(startExtractArticles),
      mergeMap((evt) => {
        return this.hangfireService.extractArticles()
          .pipe(
            map(content => completeExtractArticles()),
            catchError(() => of(errorExtractArticles))
          );
      }
      )
  );

  @Effect()
  extractRecommendations$ = this.actions$
    .pipe(
      ofType(startExtractRecommendations),
      mergeMap((evt) => {
        return this.hangfireService.extractRecommendations()
          .pipe(
            map(content => completeExtractRecommendations()),
            catchError(() => of(errorExtractRecommendations))
          );
      }
      )
    );
}
