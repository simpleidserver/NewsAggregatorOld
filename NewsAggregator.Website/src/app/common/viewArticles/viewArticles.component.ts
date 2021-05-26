import { Component, OnDestroy, OnInit, ɵclearResolutionOfComponentResourcesQueue } from '@angular/core';
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
    const drawerContent = this.drawerContentService.getDrawerContent();
    drawerContent.elementScrolled().subscribe((evt) => {
      const offset = drawerContent.measureScrollOffset("bottom");
      const o = Math.floor(offset);
      if (o <= 0 && !this.isLoadingData) {
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

  read(article: Article) {
    const request = fromArticleActions.startReadArticle({ articleId: article.id });
    this.store.dispatch(request);
  }

  unread(article: Article) {
    const request = fromArticleActions.startUnreadArticle({ articleId: article.id });
    this.store.dispatch(request);
  }

  readAndHide(article: Article) {
    const request = fromArticleActions.startReadAndHideArticle({ articleId: article.id });
    this.store.dispatch(request);
  }

  navigate(article: Article) {
    window.open(article.externalId, '_blank');
  }

  abstract refresh() : void;
}
