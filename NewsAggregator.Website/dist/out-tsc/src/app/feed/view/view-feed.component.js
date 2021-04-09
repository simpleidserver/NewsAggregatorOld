import { __decorate } from "tslib";
import { Component } from '@angular/core';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select } from '@ngrx/store';
import { Feed } from '../../stores/feed/models/feed.model';
let FeedViewComponent = class FeedViewComponent {
    constructor(activatedRoute, store, drawerContentService) {
        this.activatedRoute = activatedRoute;
        this.store = store;
        this.drawerContentService = drawerContentService;
        this.articles = [];
        this.feed = new Feed();
        this.startIndex = 0;
        this.count = 10;
        this.isLoadingData = false;
    }
    ngOnInit() {
        this.feedArticlesListener = this.store.pipe(select(fromAppState.selectFeedArticleResult)).subscribe((r) => {
            if (!r) {
                return;
            }
            this.isLoadingData = false;
            this.startIndex = r.startIndex;
            this.articles = this.articles.concat(r.content);
        });
        this.feedListener = this.store.pipe(select(fromAppState.selectFeedResult)).subscribe((r) => {
            if (!r) {
                return;
            }
            this.feed = r;
        });
        const drawerContent = this.drawerContentService.getDrawerContent();
        drawerContent.elementScrolled().subscribe((evt) => {
            const offset = drawerContent.measureScrollOffset("bottom");
            const o = Math.floor(offset);
            if (o === 0 && !this.isLoadingData) {
                this.isLoadingData = true;
                this.refresh(this.startIndex + this.count);
            }
        });
        this.activatedRoute.params.subscribe(() => {
            const feedId = this.activatedRoute.snapshot.params['id'];
            const request = fromFeedActions.startGetFeed({ feedId: feedId });
            this.store.dispatch(request);
            this.refresh(0);
        });
    }
    ngOnDestroy() {
        if (this.feedArticlesListener) {
            this.feedArticlesListener.unsubscribe();
        }
        if (this.feedListener) {
            this.feedListener.unsubscribe();
        }
    }
    refresh(startIndex) {
        const feedId = this.activatedRoute.snapshot.params['id'];
        const request = fromFeedActions.startSearchFeedArticles({ count: this.count, startIndex: startIndex, order: 'createDateTime', direction: 'desc', feedId: feedId });
        this.store.dispatch(request);
    }
};
FeedViewComponent = __decorate([
    Component({
        selector: 'app-feed-view',
        templateUrl: './view-feed.component.html',
        styleUrls: ['./view-feed.component.sass']
    })
], FeedViewComponent);
export { FeedViewComponent };
//# sourceMappingURL=view-feed.component.js.map