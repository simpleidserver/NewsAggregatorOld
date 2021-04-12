import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { completeSearchArticlesInDatasource, errorSearchArticlesInDatasource, startSearchArticlesInDatasource } from '../actions/article.actions';
let ArticlesEffects = class ArticlesEffects {
    constructor(actions$, articleService) {
        this.actions$ = actions$;
        this.articleService = articleService;
        this.searchArticlesInDatasource$ = this.actions$
            .pipe(ofType(startSearchArticlesInDatasource), mergeMap((evt) => {
            return this.articleService.search(evt.startIndex, evt.count, evt.order, evt.direction, evt.datasourceId)
                .pipe(map(articles => completeSearchArticlesInDatasource({ content: articles })), catchError(() => of(errorSearchArticlesInDatasource())));
        }));
    }
};
__decorate([
    Effect()
], ArticlesEffects.prototype, "searchArticlesInDatasource$", void 0);
ArticlesEffects = __decorate([
    Injectable()
], ArticlesEffects);
export { ArticlesEffects };
//# sourceMappingURL=article.effects.js.map