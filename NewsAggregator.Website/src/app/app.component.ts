import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDrawerContent } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OAuthService, OAuthStorage } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { filter } from 'rxjs/operators';
import { authConfig } from './auth.config';
import { DrawerContentService } from './common/matDrawerContent.service';
import { Datasource } from './stores/datasource/models/datasource.model';
import { DetailedFeed } from './stores/feed/models/detailedfeed.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnDestroy {
  listener: any;
  isConnected: boolean = false;
  groupedFeeds: { id: string, title: string, nbStoriesPerMonth: number, datasources: Datasource[] }[];
  @ViewChild('content', { static: true}) public matDrawer!: MatDrawerContent;

  constructor(
    public router: Router,
    private translateService: TranslateService,
    private oauthService: OAuthService,
    private store: Store<fromAppState.AppState>,
    private drawerContentService: DrawerContentService,
    private actions$: ScannedActionsSubject,
    private snackBar: MatSnackBar) {
    this.translateService.setDefaultLang('fr');
    this.translateService.use('fr');
    this.configureAuth();
  }

  ngOnInit(): void {
    const self = this;
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeAddFeed.type))
      .subscribe(() => {
        self.refresh();
      });
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeDeleteDatasources.type))
      .subscribe(() => {
        self.refresh();
      });
    this.actions$.pipe(
      filter((action: any) => action.type === fromFeedActions.completeDeleteFeed.type))
      .subscribe(() => {
        self.snackBar.open(self.translateService.instant('feed.feedRemoved'), self.translateService.instant('undo'), {
          duration: 2000
        });
        self.refresh();
      });
    this.listener = this.store.pipe(select(fromAppState.selectAllFeedsResult)).subscribe((r: DetailedFeed[] | null) => {
      if (!r) {
        return;
      }

      this.groupedFeeds = [];
      r.forEach((f: DetailedFeed) => {
        const record = this.groupedFeeds.filter((r) => r.id === f.feedId);
        if (record.length === 0) {
          this.groupedFeeds.push({ id: f.feedId, title: f.feedTitle, nbStoriesPerMonth: f.nbStoriesPerMonth, datasources: [{ description: '', id: f.datasourceId, title: f.datasourceTitle, nbFollowers: f.nbFollowers, nbStoriesPerMonth: f.nbStoriesPerMonth }] });
        } else {
          record[0].datasources.push({ description: '', id: f.datasourceId, title: f.datasourceTitle, nbFollowers: f.nbFollowers, nbStoriesPerMonth: f.nbStoriesPerMonth });
        }
      });
    });
    this.drawerContentService.setDrawerContent(this.matDrawer);
    this.oauthService.events.subscribe((e: any) => {
      if (e.type === "logout") {
        this.isConnected = false;
        this.router.navigate(['/home']);
        this.groupedFeeds = [];
      } else if (e.type === "token_received") {
        this.init();
        this.refresh();
      }
    });
    this.init();
    this.refresh();
  }

  disconnect(evt: any) {
    evt.preventDefault();
    this.oauthService.logOut();
    return false;
  }

  ngOnDestroy(): void {
    if (this.listener) {
      this.listener.unsubscribe();
    }
  }

  chooseLanguage(lng: string) {
    this.translateService.use(lng);
  }

  login(evt: any) {
    evt.preventDefault();
    // this.oauthService.customQueryParams = {
    //   'response_mode': 'fragment'
    // };
    this.oauthService.initCodeFlow();
    return false;
  }

  deleteFeed(feedId: string) {
    const request = fromFeedActions.startDeleteFeed({ feedId: feedId });
    this.store.dispatch(request);
  }

  private configureAuth() {
    this.oauthService.configure(authConfig);
    // this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    let self = this;
    this.oauthService.loadDiscoveryDocumentAndTryLogin({
      disableOAuth2StateCheck: true
    });
  }

  private init() {
    const claims: any = this.oauthService.getIdentityClaims();
    if (!claims) {
      this.isConnected = false;
      return;
    }

    this.isConnected = true;
  }

  private refresh() {
    const request = fromFeedActions.startGetAllFeeds();
    this.store.dispatch(request);
  }
}
