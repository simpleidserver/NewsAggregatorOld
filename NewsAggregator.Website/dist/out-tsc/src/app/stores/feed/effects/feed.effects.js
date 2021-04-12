import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { Effect, ofType } from '@ngrx/effects';
import { forkJoin, of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { completeAddFeed, completeDeleteDatasources, completeGetAllFeeds, completeGetFeed, completeSearchFeedArticles, completeSearchFeeds, errorAddFeed, errorDeleteDatasources, errorGetAllFeeds, errorGetFeed, errorSearchFeedArticles, errorSearchFeeds, startAddFeed, startDeleteDatasources, startGetAllFeeds, startGetFeed, startSearchFeedArticles, startSearchFeeds } from '../actions/feed.actions';
let FeedsEffects = class FeedsEffects {
    constructor(actions$, feedService) {
        this.actions$ = actions$;
        this.feedService = feedService;
        this.searchFeeds$ = this.actions$
            .pipe(ofType(startSearchFeeds), mergeMap((evt) => {
            return this.feedService.search(evt.startIndex, evt.count, evt.order, evt.direction, evt.feedTitle, evt.datasourceIds, evt.followersFilter, evt.storiesFilter, evt.isPaginationEnabled)
                .pipe(map(feeds => completeSearchFeeds({ content: feeds })), catchError(() => of(errorSearchFeeds())));
        }));
        this.deleteDatasources$ = this.actions$
            .pipe(ofType(startDeleteDatasources), mergeMap((evt) => {
            const calls = evt.parameters.map(p => this.feedService.unsubscribeDatasource(p));
            return forkJoin(calls)
                .pipe(map(() => completeDeleteDatasources()), catchError(() => of(errorDeleteDatasources())));
        }));
        this.searchFeedArticles$ = this.actions$
            .pipe(ofType(startSearchFeedArticles), mergeMap((evt) => {
            return this.feedService.searchArticles(evt.feedId, evt.startIndex, evt.count, evt.order, evt.direction)
                .pipe(map((articles) => completeSearchFeedArticles({ content: articles })), catchError(() => of(errorSearchFeedArticles())));
        }));
        this.getFeed$ = this.actions$
            .pipe(ofType(startGetFeed), mergeMap((evt) => {
            return this.feedService.getFeed(evt.feedId)
                .pipe(map((feed) => completeGetFeed({ content: feed })), catchError(() => of(errorGetFeed())));
        }));
        this.addFeed$ = this.actions$
            .pipe(ofType(startAddFeed), mergeMap((evt) => {
            return this.feedService.addFeed(evt.feedTitle, evt.datasourceIds)
                .pipe(map((feedId) => completeAddFeed({ content: feedId })), catchError(() => of(errorAddFeed())));
        }));
        this.getAllFeeds$ = this.actions$
            .pipe(ofType(startGetAllFeeds), mergeMap((evt) => {
            return this.feedService.getAllFeeds()
                .pipe(map((feeds) => completeGetAllFeeds({ content: feeds })), catchError(() => of(errorGetAllFeeds())));
        }));
    }
};
__decorate([
    Effect()
], FeedsEffects.prototype, "searchFeeds$", void 0);
__decorate([
    Effect()
], FeedsEffects.prototype, "deleteDatasources$", void 0);
__decorate([
    Effect()
], FeedsEffects.prototype, "searchFeedArticles$", void 0);
__decorate([
    Effect()
], FeedsEffects.prototype, "getFeed$", void 0);
__decorate([
    Effect()
], FeedsEffects.prototype, "addFeed$", void 0);
__decorate([
    Effect()
], FeedsEffects.prototype, "getAllFeeds$", void 0);
FeedsEffects = __decorate([
    Injectable()
], FeedsEffects);
export { FeedsEffects };
//# sourceMappingURL=feed.effects.js.map