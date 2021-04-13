import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as fromAppState from '@app/stores/appstate';
import { Datasource } from '@app/stores/datasource/models/datasource.model';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { DetailedFeed } from '@app/stores/feed/models/detailedfeed.model';
import { SearchFeedsResult } from '@app/stores/feed/models/search-feed.model';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { filter } from 'rxjs/operators';
import { AddFeedDialog } from './add-feed.component';

@Component({
  selector: 'app-feed-list',
  templateUrl: './feed-list.component.html',
  styleUrls: ['./feed-list.component.sass']
})
export class FeedListComponent implements OnInit, OnDestroy {
  listener: any;
  displayedColumns: string[] = ['checkbox', 'feedTitle', 'datasourceTitle', 'nbFollowers', 'nbStoriesPerMonth'];
  feeds: DetailedFeed[] = [];
  feedsToBeRemoved: DetailedFeed[] = [];
  feedTitle: string;
  selectedDatasourceIds: string[];
  selectedFollower: number | null = null;
  selectedStory: number |  null = null;
  datasources: Datasource[];
  isLoading: boolean = false;

  constructor(
    private dialog: MatDialog,
    private store: Store<fromAppState.AppState>,
    private actions$: ScannedActionsSubject,
    private snackBar: MatSnackBar,
    private translateService: TranslateService) { }

  ngOnInit(): void {
    const self = this;
    this.refresh();
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeAddFeed.type))
      .subscribe(() => {
        self.snackBar.open(self.translateService.instant('feed.feedAdded'), self.translateService.instant('undo'), {
          duration: 2000
        });
        self.refresh();
      });
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeDeleteDatasources.type))
      .subscribe(() => {
        self.snackBar.open(self.translateService.instant('feed.feedSourcesRemoved'), self.translateService.instant('undo'), {
          duration: 2000
        });
        self.refresh();
      });
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeDeleteFeed.type))
      .subscribe(() => {
        self.refresh();
      });
    this.listener = this.store.pipe(select(fromAppState.selectFeedSearchResult)).subscribe((r: SearchFeedsResult | null) => {
      if (!r) {
        return;
      }

      this.isLoading = false;
      this.feeds = r.content;
      this.feedsToBeRemoved = [];
    });
  }

  ngOnDestroy(): void {
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

  onDatasourceSelected(evt: string[]) {
    this.selectedDatasourceIds = evt;
    this.refresh();
  }

  deleteFeed(feed: DetailedFeed) {
    const filtered = this.feedsToBeRemoved.filter((f: DetailedFeed) => f.datasourceId === feed.datasourceId && f.feedId === feed.feedId);
    if (filtered.length === 1) {
      this.feedsToBeRemoved = this.feedsToBeRemoved.filter((f: DetailedFeed) => f.datasourceId !== feed.datasourceId || f.feedId !== feed.feedId);
      return;
    }

    this.feedsToBeRemoved.push(feed);
  }

  remove() {
    if (this.feedsToBeRemoved.length === 0) {
      return;
    }

    const request = fromFeedActions.startDeleteDatasources({ parameters: this.feedsToBeRemoved.map((v: DetailedFeed) => new fromFeedActions.DeleteDatasource(v.feedId, v.datasourceId)) });
    this.store.dispatch(request);
  }

  refresh() {
    this.isLoading = true;
    const request = fromFeedActions.startSearchFeeds({ order: 'createDateTime', direction: 'desc', count: null, startIndex: null, feedTitle: this.feedTitle, datasourceIds: this.selectedDatasourceIds, followersFilter: this.selectedFollower, storiesFilter: this.selectedStory, isPaginationEnabled: false });
    this.store.dispatch(request);
  }
}
