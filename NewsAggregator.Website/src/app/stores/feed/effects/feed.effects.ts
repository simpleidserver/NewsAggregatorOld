import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeDeleteDatasources, completeSearchFeeds,




    errorDeleteDatasources, errorSearchFeeds,

    startDeleteDatasources, startSearchFeeds
} from '../actions/feed.actions';
import { FeedService } from '../services/feed.service';

@Injectable()
export class FeedsEffects {
  constructor(
    private actions$: Actions,
    private feedService: FeedService
  ) { }

  @Effect()
  searchFeeds$ = this.actions$
    .pipe(
      ofType(startSearchFeeds),
      mergeMap((evt) => {
        return this.feedService.search(evt.startIndex, evt.count, evt.order, evt.direction, evt.feedTitle)
          .pipe(
            map(feeds => completeSearchFeeds({ content: feeds })),
            catchError(() => of(errorSearchFeeds()))
          );
      }
      )
  );

  @Effect()
  deleteDatasources$ = this.actions$
    .pipe(
      ofType(startDeleteDatasources),
      mergeMap((evt) => {
        return this.feedService.deleteDatasources(evt.parameters)
          .pipe(
            map(() => completeDeleteDatasources({ parameters: evt.parameters })),
            catchError(() => of(errorDeleteDatasources()))
          );
      }
      )
    );
}
