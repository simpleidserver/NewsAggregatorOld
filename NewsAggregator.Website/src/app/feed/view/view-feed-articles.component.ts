import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import { Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { ViewArticlesComponent } from '../../common/viewArticles/viewArticles.component';

@Component({
  selector: 'app-feed-view-articles',
  templateUrl: '../../common/viewArticles/viewArticles.component.html',
  styleUrls: ['../../common/viewArticles/viewArticles.component.sass']
})
export class FeedViewArticlesComponent extends ViewArticlesComponent {
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected store: Store<fromAppState.AppState>,
    protected drawerContentService: DrawerContentService) {
    super(activatedRoute, store, drawerContentService);
  }

  refresh(startIndex : number) {
  }
}
