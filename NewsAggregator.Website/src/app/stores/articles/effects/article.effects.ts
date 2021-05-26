import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeLikeArticle,
    completeReadAndHideArticle, completeReadArticle, completeSearchArticlesInDatasource,
    completeSearchArticlesInFeed, completeSearchRecommendations, completeUnLikeArticle, completeUnreadArticle, errorLikeArticle,
    errorReadAndHideArticle, errorReadArticle, errorSearchArticlesInDatasource,
    errorSearchArticlesInFeed, errorSearchRecommendations, errorUnLikeArticle, errorUnreadArticle, startLikeArticle,
    startReadAndHideArticle, startReadArticle, startSearchArticlesInDatasource,
    startSearchArticlesInFeed,
    startSearchRecommendations,
    startUnlikeArticle,
    startUnreadArticle
} from '../actions/article.actions';
import { ArticleService } from '../services/article.service';

@Injectable()
export class ArticlesEffects {
  constructor(
    private actions$: Actions,
    private articleService: ArticleService
  ) { }

  @Effect()
  searchArticlesInDatasource$ = this.actions$
    .pipe(
      ofType(startSearchArticlesInDatasource),
      mergeMap((evt) => {
        return this.articleService.searchInDatasource(evt.startIndex, evt.count, evt.order, evt.direction, evt.datasourceId)
          .pipe(
            map(articles => completeSearchArticlesInDatasource({ content: articles })),
            catchError(() => of(errorSearchArticlesInDatasource()))
          );
      }
      )
  );

  @Effect()
  searchArticlesInFeed$ = this.actions$
    .pipe(
      ofType(startSearchArticlesInFeed),
      mergeMap((evt) => {
        return this.articleService.searchInFeed(evt.startIndex, evt.count, evt.order, evt.direction, evt.feedId)
          .pipe(
            map(articles => completeSearchArticlesInFeed({ content: articles })),
            catchError(() => of(errorSearchArticlesInFeed()))
          );
      }
      )
  );

  @Effect()
  likeArticle$ = this.actions$
    .pipe(
      ofType(startLikeArticle),
      mergeMap((evt) => {
        return this.articleService.like(evt.articleId)
          .pipe(
            map(articles => completeLikeArticle({ articleId: evt.articleId })),
            catchError(() => of(errorLikeArticle()))
          );
      }
      )
  );

  @Effect()
  unlikeArticle$ = this.actions$
    .pipe(
      ofType(startUnlikeArticle),
      mergeMap((evt) => {
        return this.articleService.unlike(evt.articleId)
          .pipe(
            map(articles => completeUnLikeArticle({ articleId: evt.articleId })),
            catchError(() => of(errorUnLikeArticle()))
          );
      }
      )
  );

  @Effect()
  readArticle$ = this.actions$
    .pipe(
      ofType(startReadArticle),
      mergeMap((evt) => {
        return this.articleService.read(evt.articleId)
          .pipe(
            map(articles => completeReadArticle({ articleId: evt.articleId })),
            catchError(() => of(errorReadArticle()))
          );
      }
      )
  );

  @Effect()
  unreadArticle$ = this.actions$
    .pipe(
      ofType(startUnreadArticle),
      mergeMap((evt) => {
        return this.articleService.unread(evt.articleId)
          .pipe(
            map(articles => completeUnreadArticle({ articleId: evt.articleId })),
            catchError(() => of(errorUnreadArticle()))
          );
      }
      )
    );

  @Effect()
  readAndHideArticle$ = this.actions$
    .pipe(
      ofType(startReadAndHideArticle),
      mergeMap((evt) => {
        return this.articleService.readAndHide(evt.articleId)
          .pipe(
            map(articles => completeReadAndHideArticle({ articleId: evt.articleId })),
            catchError(() => of(errorReadAndHideArticle()))
          );
      }
      )
    );

  @Effect()
  searchRecommendations$ = this.actions$
    .pipe(
      ofType(startSearchRecommendations),
      mergeMap((evt) => {
        return this.articleService.searchRecommendations(evt.startIndex, evt.count)
          .pipe(
            map(content => completeSearchRecommendations({ content: content })),
            catchError(() => of(errorSearchRecommendations()))
          );
      }
      )
    );
}
