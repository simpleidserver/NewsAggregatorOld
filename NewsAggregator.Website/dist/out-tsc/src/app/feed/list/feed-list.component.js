import { __decorate } from "tslib";
import { Component } from '@angular/core';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select } from '@ngrx/store';
import { filter } from 'rxjs/operators';
import { AddFeedDialog } from './add-feed.component';
let FeedListComponent = class FeedListComponent {
    constructor(dialog, store, actions$, snackBar, translateService) {
        this.dialog = dialog;
        this.store = store;
        this.actions$ = actions$;
        this.snackBar = snackBar;
        this.translateService = translateService;
        this.displayedColumns = ['checkbox', 'feedTitle', 'datasourceTitle', 'nbFollowers', 'nbStoriesPerMonth'];
        this.feeds = [];
        this.feedsToBeRemoved = [];
        this.selectedFollower = null;
        this.selectedStory = null;
        this.isLoading = false;
    }
    ngOnInit() {
        const self = this;
        this.refresh();
        this.actions$.pipe(filter((action) => action.type === fromFeedActions.completeAddFeed.type))
            .subscribe(() => {
            self.snackBar.open(self.translateService.instant('feed.feedAdded'), self.translateService.instant('undo'), {
                duration: 2000
            });
            self.refresh();
        });
        this.actions$.pipe(filter((action) => action.type === fromFeedActions.completeDeleteDatasources.type))
            .subscribe(() => {
            self.snackBar.open(self.translateService.instant('feed.feedSourcesRemoved'), self.translateService.instant('undo'), {
                duration: 2000
            });
            self.refresh();
        });
        this.listener = this.store.pipe(select(fromAppState.selectFeedSearchResult)).subscribe((r) => {
            if (!r) {
                return;
            }
            this.isLoading = false;
            this.feeds = r.content;
            this.feedsToBeRemoved = [];
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
            if (!result) {
                return;
            }
            const request = fromFeedActions.startAddFeed({ feedTitle: result.feedTitle, datasourceIds: result.datasourceIds });
            this.store.dispatch(request);
        });
    }
    onDatasourceSelected(evt) {
        this.selectedDatasourceIds = evt;
        this.refresh();
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
        this.isLoading = true;
        const request = fromFeedActions.startSearchFeeds({ order: 'createDateTime', direction: 'desc', count: null, startIndex: null, feedTitle: this.feedTitle, datasourceIds: this.selectedDatasourceIds, followersFilter: this.selectedFollower, storiesFilter: this.selectedStory, isPaginationEnabled: false });
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