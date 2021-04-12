import { Component, OnDestroy, OnInit } from '@angular/core';
import * as fromAppState from '@app/stores/appstate';
import { select, Store } from '@ngrx/store';
import { Feed } from '../../stores/feed/models/feed.model';

@Component({
  selector: 'app-feed-view',
  templateUrl: './view-feed.component.html'
})
export class FeedViewComponent implements OnInit, OnDestroy {
  feed: Feed = new Feed();
  feedListener: any;

  constructor(
    private store: Store<fromAppState.AppState>) { }

  ngOnInit(): void {
    this.feedListener = this.store.pipe(select(fromAppState.selectFeedResult)).subscribe((r: Feed | null) => {
      if (!r) {
        return;
      }

      this.feed = r;
    });
  }

  ngOnDestroy(): void {
    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }
  }
}
