import { __decorate } from "tslib";
import { Component } from '@angular/core';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select } from '@ngrx/store';
import { AddFeedDialog } from './add-feed.component';
let FeedListComponent = class FeedListComponent {
    constructor(dialog, store) {
        this.dialog = dialog;
        this.store = store;
        this.displayedColumns = ['checkbox', 'feedTitle', 'datasourceTitle', 'nbFollowers', 'nbStoriesPerMonth'];
        this.feeds = [];
        this.feedsToBeRemoved = [];
    }
    ngOnInit() {
        this.refresh();
        this.listener = this.store.pipe(select(fromAppState.selectFeedSearchResult)).subscribe((r) => {
            if (!r) {
                return;
            }
            this.feeds = r.content;
        });
    }
    ngOnDestroy() {
        if (this.listener) {
            this.listener.unsubscribe();
        }
    }
    addFeed() {
        const dialogRef = this.dialog.open(AddFeedDialog);
        dialogRef.afterClosed().subscribe(result => {
            const request = fromFeedActions.startAddFeed({ feedTitle: result.feedTitle, datasource: result.datasource });
            this.store.dispatch(request);
        });
    }
    deleteFeed(feed) {
        const filtered = this.feedsToBeRemoved.filter((f) => f.datasourceId === feed.datasourceId && f.feedId === feed.feedId);
        if (filtered.length === 1) {
            this.feedsToBeRemoved = this.feedsToBeRemoved.filter((f) => f.datasourceId !== feed.datasourceId || f.feedId !== feed.feedId);
            return;
        }
        this.feedsToBeRemoved.push(feed);
    }
    remove() {
        if (this.feedsToBeRemoved.length === 0) {
            return;
        }
        const request = fromFeedActions.startDeleteDatasources({ parameters: this.feedsToBeRemoved.map((v) => new fromFeedActions.DeleteDatasource(v.feedId, v.datasourceId)) });
        this.store.dispatch(request);
    }
    refresh() {
        const startIndex = 0;
        const count = 100;
        const request = fromFeedActions.startSearchFeeds({ order: 'createDateTime', direction: 'desc', count: count, startIndex: startIndex, feedTitle: this.feedTitle });
        this.store.dispatch(request);
    }
};
FeedListComponent = __decorate([
    Component({
        selector: 'app-feed-list',
        templateUrl: './feed-list.component.html',
        styleUrls: ['./feed-list.component.sass']
    })
], FeedListComponent);
export { FeedListComponent };
//# sourceMappingURL=feed-list.component.js.map