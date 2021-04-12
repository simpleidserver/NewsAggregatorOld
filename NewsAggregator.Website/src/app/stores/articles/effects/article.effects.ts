import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeSearchArticlesInDatasource, errorSearchArticlesInDatasource, startSearchArticlesInDatasource
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
        return this.articleService.search(evt.startIndex, evt.count, evt.order, evt.direction, evt.datasourceId)
          .pipe(
            map(articles => completeSearchArticlesInDatasource({ content: articles })),
            catchError(() => of(errorSearchArticlesInDatasource()))
          );
      }
      )
  );
}
