import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeLikeArticle,
    completeSearchArticlesInDatasource,
    completeSearchArticlesInFeed, completeUnLikeArticle, errorLikeArticle, errorSearchArticlesInDatasource,
    errorSearchArticlesInFeed, errorUnLikeArticle, startLikeArticle, startSearchArticlesInDatasource,
    startSearchArticlesInFeed,
    startUnlikeArticle
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
}
