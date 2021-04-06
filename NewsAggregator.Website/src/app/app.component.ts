import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { authConfig } from './auth.config';
import { Datasource } from './stores/datasource/models/datasource.model';
import { Feed } from './stores/feed/models/feed.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  sessionCheckTimer: any;
  isConnected: boolean = false;
  feeds: Feed[] = [
    { feedTitle: 'News', datasourceTitle: 'BBC', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'en', datasourceId: 'bbc', feedId: 'news' },
    { feedTitle: 'News', datasourceTitle: 'Sputnick', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'sputnick', feedId: 'news' },
    { feedTitle: 'Gaming', datasourceTitle: 'JDV', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'JDV', feedId: 'gaming' }
  ];
  groupedFeeds: { id: string, title: string, isOpened: boolean, nbStoriesPerMonth: number, datasources: Datasource[] }[];

  constructor(
    private translateService: TranslateService,
    private oauthService: OAuthService,
    private route: Router) {
    this.translateService.setDefaultLang('fr');
    this.translateService.use('fr');
    this.configureAuth();
    this.groupedFeeds = [];
    this.feeds.forEach((f: Feed) => {
      const record = this.groupedFeeds.filter((r) => r.id === f.feedId);
      if (record.length === 0) {
        this.groupedFeeds.push({ id: f.feedId, title: f.feedTitle, nbStoriesPerMonth: f.nbStoriesPerMonth, isOpened: false, datasources: [{ description: '', id: f.datasourceId, title: f.datasourceTitle }] });
      } else {
        record[0].datasources.push({ description: '', id : f.datasourceId, title: f.datasourceTitle });
      }
    });
  }

  ngOnInit(): void {
    const claims : any = this.oauthService.getIdentityClaims();
    if (!claims) {
      this.isConnected = false;
      return;
    }

    this.isConnected = true;
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
        self.route.navigate(["/"]);
      }
    }, 3000);
  }
}
