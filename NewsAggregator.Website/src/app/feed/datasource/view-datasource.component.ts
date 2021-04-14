import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromDatasourceActions from '@app/stores/datasource/actions/datasource.actions';
import { Datasource } from '@app/stores/datasource/models/datasource.model';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { Feed } from '@app/stores/feed/models/feed.model';
import { select, Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { DatasourceViewArticlesComponent } from './view-datasource-articles.component';

@Component({
  selector: 'app-datasource-view',
  templateUrl: './view-datasource.component.html'
})
export class DatasourceViewComponent implements OnInit, OnDestroy {
  feedListener: any;
  datasourceListener: any;
  feed: Feed;
  datasource: Datasource;
  @ViewChild('viewArticles', { static: true }) viewArticles: DatasourceViewArticlesComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private store: Store<fromAppState.AppState>,
    private drawerContentService: DrawerContentService) { }

  ngOnInit(): void {
    this.feedListener = this.store.pipe(select(fromAppState.selectFeedResult)).subscribe((r: Feed | null) => {
      if (!r) {
        return;
      }

      this.feed = r;
    });
    this.datasourceListener = this.store.pipe(select(fromAppState.selectDatasourceResult)).subscribe((r: Datasource | null) => {
      if (!r) {
        return;
      }

      this.datasource = r;
    });
    this.activatedRoute.params.subscribe(() => {
      this.viewArticles.reset();
      const feedId = this.activatedRoute.snapshot.params['id'];
      const datasourceId = this.activatedRoute.snapshot.params['datasourceid'];
      const getFeedRequest = fromFeedActions.startGetFeed({ feedId: feedId });
      const getDatasourceRequest = fromDatasourceActions.startGetDatasource({ datasourceId: datasourceId });
      this.store.dispatch(getFeedRequest);
      this.store.dispatch(getDatasourceRequest);
    });
  }

  ngOnDestroy(): void {
    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }

    if (this.datasourceListener) {
      this.datasourceListener.unsubscribe();
    }
  }
}
