import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromArticleActions from '@app/stores/articles/actions/article.actions';
import { Article } from '@app/stores/articles/models/article.model';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { filter } from 'rxjs/operators';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { SearchArticlesResult } from '../../stores/articles/models/search-article.model';
import { Feed } from '../../stores/feed/models/feed.model';

@Component({
  template: ''
})
export abstract class ViewArticlesComponent implements OnInit, OnDestroy {
  pagination: number = 10;
  articles: Article[] = [];
  feed: Feed = new Feed();
  feedArticlesListener: any;
  feedListener: any;
  refreshInterval: any;
  protected count: number = 10;
  protected isLoadingData: boolean = false;

  constructor(
    protected activatedRoute: ActivatedRoute,
    protected store: Store<fromAppState.AppState>,
    protected drawerContentService: DrawerContentService,
    protected actions$: ScannedActionsSubject) { }

  ngOnInit(): void {
    const self = this;
    this.actions$.pipe(
      filter((action: any) => action.type === fromArticleActions.completeLikeArticle.type))
      .subscribe((evt: any) => {
        const article = self.articles.filter((a: Article) => a.id === evt.articleId)[0];
        article.likeActionDateTime = new Date();
      });
    this.actions$.pipe(
      filter((action: any) => action.type === fromArticleActions.completeUnLikeArticle.type))
      .subscribe((evt: any) => {
        const article = self.articles.filter((a: Article) => a.id === evt.articleId)[0];
        article.likeActionDateTime = null;
      });
    this.feedArticlesListener = this.store.pipe(select(fromAppState.selectSearchArticlesResult)).subscribe((r: SearchArticlesResult | null) => {
      if (!r) {
        return;
      }

      this.isLoadingData = false;
      const copy = JSON.parse(JSON.stringify(r.content));
      this.articles = copy;
    });
    this.refreshInterval = setInterval(this.refresh.bind(this), 1000);
    const drawerContent = this.drawerContentService.getDrawerContent();
    drawerContent.elementScrolled().subscribe((evt) => {
      const offset = drawerContent.measureScrollOffset("bottom");
      const o = Math.floor(offset);
      if (o === 0 && !this.isLoadingData) {
        this.isLoadingData = true;
        this.count += this.pagination;
        this.refresh();
      }
    });
  }

  ngOnDestroy(): void {
    if (this.feedArticlesListener) {
      this.feedArticlesListener.unsubscribe();
    }

    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }

    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
    }
  }

  reset() {
    this.articles = [];
    this.count = this.pagination;
    this.refresh();
  }

  like(article: Article) {
    const request = fromArticleActions.startLikeArticle({ articleId: article.id });
    this.store.dispatch(request);
  }

  unlike(article: Article) {
    const request = fromArticleActions.startUnlikeArticle({ articleId: article.id });
    this.store.dispatch(request);
  }

  navigate(article: Article) {
    const request = fromArticleActions.startViewArticle({ articleId: article.id });
    this.store.dispatch(request);
    window.open(article.externalId, '_blank');
  }

  abstract refresh() : void;
}
