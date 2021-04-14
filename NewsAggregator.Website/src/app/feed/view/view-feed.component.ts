import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select, Store } from '@ngrx/store';
import { Feed } from '../../stores/feed/models/feed.model';
import { FeedViewArticlesComponent } from './view-feed-articles.component';

@Component({
  selector: 'app-feed-view',
  templateUrl: './view-feed.component.html'
})
export class FeedViewComponent implements OnInit, OnDestroy {
  feed: Feed = new Feed();
  feedListener: any;
  datasourceListener: any;
  @ViewChild('viewArticles', { static: true}) viewArticles: FeedViewArticlesComponent;

  constructor(
    private store: Store<fromAppState.AppState>,
    protected activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.feedListener = this.store.pipe(select(fromAppState.selectFeedResult)).subscribe((r: Feed | null) => {
      if (!r) {
        return;
      }

      this.feed = r;
    });

    this.activatedRoute.params.subscribe(() => {
      this.viewArticles.reset();
      const feedId = this.activatedRoute.snapshot.params['id'];
      const request = fromFeedActions.startGetFeed({ feedId: feedId });
      this.store.dispatch(request);
    });
  }

  ngOnDestroy(): void {
    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }
  }
}
