import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { forkJoin, of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeAddFeed,
    completeDeleteDatasources,
    completeDeleteFeed,
    completeGetAllFeeds,
    completeGetFeed,
    completeSearchFeeds,
    errorAddFeed,
    errorDeleteDatasources,
    errorDeleteFeed,
    errorGetAllFeeds,
    errorGetFeed,
    errorSearchFeeds,
    startAddFeed,
    startDeleteDatasources,
    startDeleteFeed,
    startGetAllFeeds,
    startGetFeed,
    startSearchFeeds
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
        return this.feedService.search(evt.startIndex, evt.count, evt.order, evt.direction, evt.feedTitle, evt.datasourceIds, evt.followersFilter, evt.storiesFilter, evt.isPaginationEnabled)
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
        const calls = evt.parameters.map(p => this.feedService.unsubscribeDatasource(p));
        return forkJoin(calls)
          .pipe(
            map(() => completeDeleteDatasources()),
            catchError(() => of(errorDeleteDatasources()))
          );
      }
      )
  );

  @Effect()
  getFeed$ = this.actions$
    .pipe(
      ofType(startGetFeed),
      mergeMap((evt) => {
        return this.feedService.getFeed(evt.feedId)
          .pipe(
            map((feed) => completeGetFeed({ content: feed })),
            catchError(() => of(errorGetFeed()))
          );
      }
      )
  );

  @Effect()
  addFeed$ = this.actions$
    .pipe(
      ofType(startAddFeed),
      mergeMap((evt) => {
        return this.feedService.addFeed(evt.feedTitle, evt.datasourceIds)
          .pipe(
            map((feedId) => completeAddFeed({ content: feedId })),
            catchError(() => of(errorAddFeed()))
          );
      }
      )
  );

  @Effect()
  getAllFeeds$ = this.actions$
    .pipe(
      ofType(startGetAllFeeds),
      mergeMap((evt) => {
        return this.feedService.getAllFeeds()
          .pipe(
            map((feeds) => completeGetAllFeeds({ content: feeds })),
            catchError(() => of(errorGetAllFeeds()))
          );
      }
      )
  );

  @Effect()
  deleteFeed$ = this.actions$
    .pipe(
      ofType(startDeleteFeed),
      mergeMap((evt) => {
        return this.feedService.deleteFeed(evt.feedId)
          .pipe(
            map(() => completeDeleteFeed),
            catchError(() => of(errorDeleteFeed()))
          );
      }
      )
    );
}
