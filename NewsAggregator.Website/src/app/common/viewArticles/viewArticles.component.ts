import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import { Article } from '@app/stores/articles/models/article.model';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select, Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { SearchArticlesResult } from '../../stores/articles/models/search-article.model';
import { Feed } from '../../stores/feed/models/feed.model';

@Component({
  template: ''
})
export abstract class ViewArticlesComponent implements OnInit, OnDestroy {
  articles: Article[] = [];
  feed: Feed = new Feed();
  feedArticlesListener: any;
  feedListener: any;
  protected startIndex: number = 0;
  protected count: number = 10;
  protected isLoadingData: boolean = false;

  constructor(
    protected activatedRoute: ActivatedRoute,
    protected store: Store<fromAppState.AppState>,
    protected drawerContentService: DrawerContentService) { }

  ngOnInit(): void {
    this.feedArticlesListener = this.store.pipe(select(fromAppState.selectSearchArticlesResult)).subscribe((r: SearchArticlesResult | null) => {
      if (!r) {
        return;
      }

      this.isLoadingData = false;
      this.startIndex = r.startIndex;
      this.articles = this.articles.concat(r.content);
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

  ngOnDestroy(): void {
    if (this.feedArticlesListener) {
      this.feedArticlesListener.unsubscribe();
    }

    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }
  }

  abstract refresh(startIndex: number) : void;
}
