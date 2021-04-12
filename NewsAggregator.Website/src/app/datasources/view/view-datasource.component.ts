import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import { Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';

@Component({
  selector: 'app-datasource-view',
  templateUrl: './view-datasource.component.html'
})
export class DatasourceViewComponent implements OnInit, OnDestroy {
  // datasource: DataSource = new DataSource();
  datasourceListener: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private store: Store<fromAppState.AppState>,
    private drawerContentService: DrawerContentService) { }

  ngOnInit(): void {
    /*
    this.feedListener = this.store.pipe(select(fromAppState.selectFeedResult)).subscribe((r: Feed | null) => {
      if (!r) {
        return;
      }

      this.feed = r;
    });
    */
  }

  ngOnDestroy(): void {
    /*
    if (this.feedListener) {
      this.feedListener.unsubscribe();
    }
    */
  }
}
