import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromArticleActions from '@app/stores/articles/actions/article.actions';
import { ScannedActionsSubject, Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { ViewArticlesComponent } from '../../common/viewArticles/viewArticles.component';

@Component({
  selector: 'app-recommendation-view-articles',
  templateUrl: '../../common/viewArticles/viewArticles.component.html',
  styleUrls: ['../../common/viewArticles/viewArticles.component.sass']
})
export class RecommendationViewArticlesComponent extends ViewArticlesComponent {
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected store: Store<fromAppState.AppState>,
    protected drawerContentService: DrawerContentService,
    protected actions$: ScannedActionsSubject) {
    super(activatedRoute, store, drawerContentService, actions$);
  }

  refresh() {
    const request = fromArticleActions.startSearchRecommendations({ count: this.count, startIndex: 0 });
    this.store.dispatch(request);
  }
}
