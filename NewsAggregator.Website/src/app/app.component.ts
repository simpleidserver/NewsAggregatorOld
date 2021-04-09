import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDrawerContent } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { authConfig } from './auth.config';
import { DrawerContentService } from './common/matDrawerContent.service';
import { Datasource } from './stores/datasource/models/datasource.model';
import { Feed } from './stores/feed/models/feed.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnDestroy {
  sessionCheckTimer: any;
  listener: any;
  isConnected: boolean = false;
  groupedFeeds: { id: string, title: string, isOpened: boolean, nbStoriesPerMonth: number, datasources: Datasource[] }[];
  @ViewChild('content', { static: true}) public matDrawer!: MatDrawerContent;

  constructor(
    private translateService: TranslateService,
    private oauthService: OAuthService,
    private store: Store<fromAppState.AppState>,
    private drawerContentService: DrawerContentService,
    private router: Router) {
    this.translateService.setDefaultLang('fr');
    this.translateService.use('fr');
    this.configureAuth();
  }

  ngOnInit(): void {
    const request = fromFeedActions.startGetAllFeeds();
    this.store.dispatch(request);
    this.listener = this.store.pipe(select(fromAppState.selectAllFeedsResult)).subscribe((r: Feed[] | null) => {
      if (!r) {
        return;
      }

      this.groupedFeeds = [];
      r.forEach((f: Feed) => {
        const record = this.groupedFeeds.filter((r) => r.id === f.feedId);
        if (record.length === 0) {
          this.groupedFeeds.push({ id: f.feedId, title: f.feedTitle, nbStoriesPerMonth: f.nbStoriesPerMonth, isOpened: false, datasources: [{ description: '', id: f.datasourceId, title: f.datasourceTitle }] });
        } else {
          record[0].datasources.push({ description: '', id: f.datasourceId, title: f.datasourceTitle });
        }
      });
    });
    this.drawerContentService.setDrawerContent(this.matDrawer);
    this.oauthService.events.subscribe((e: any) => {
      if (e.type === "logout") {
        this.isConnected = false;
      } else if (e.type === "token_received") {
        this.init();
      }
    });

    this.init();
  }

  disconnect(evt: any) {
    evt.preventDefault();
    this.oauthService.logOut();
    this.router.navigate(['/home']);
    return false;
  }

  ngOnDestroy(): void {
    if (this.listener) {
      this.listener.unsubscribe();
    }
  }

  switchOpen(record: { id: string, title: string, isOpened: boolean, datasources: Datasource[] }) {
    record.isOpened = !record.isOpened;
  }

  chooseLanguage(lng: string) {
    this.translateService.use(lng);
  }

  login(evt: any) {
    evt.preventDefault();
    this.oauthService.customQueryParams = {
      'prompt': 'login'
    };
    this.oauthService.initImplicitFlow();
    return false;
  }

  private configureAuth() {
    this.oauthService.configure(authConfig);
    this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    let self = this;
    this.oauthService.loadDiscoveryDocumentAndTryLogin({
      disableOAuth2StateCheck: true
    });
    this.sessionCheckTimer = setInterval(function () {
      if (!self.oauthService.hasValidIdToken()) {
        self.oauthService.logOut();
        // self.route.navigate(["/"]);
      }
    }, 3000);
  }

  private init() {
    const claims: any = this.oauthService.getIdentityClaims();
    if (!claims) {
      this.isConnected = false;
      return;
    }

    this.isConnected = true;
  }
}
