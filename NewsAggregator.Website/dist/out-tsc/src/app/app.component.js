import { __decorate } from "tslib";
import { Component, ViewChild } from '@angular/core';
import * as fromAppState from '@app/stores/appstate';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { select } from '@ngrx/store';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { authConfig } from './auth.config';
let AppComponent = class AppComponent {
    constructor(translateService, oauthService, store, drawerContentService, router) {
        this.translateService = translateService;
        this.oauthService = oauthService;
        this.store = store;
        this.drawerContentService = drawerContentService;
        this.router = router;
        this.isConnected = false;
        this.translateService.setDefaultLang('fr');
        this.translateService.use('fr');
        this.configureAuth();
    }
    ngOnInit() {
        const request = fromFeedActions.startGetAllFeeds();
        this.store.dispatch(request);
        this.listener = this.store.pipe(select(fromAppState.selectAllFeedsResult)).subscribe((r) => {
            if (!r) {
                return;
            }
            this.groupedFeeds = [];
            r.forEach((f) => {
                const record = this.groupedFeeds.filter((r) => r.id === f.feedId);
                if (record.length === 0) {
                    this.groupedFeeds.push({ id: f.feedId, title: f.feedTitle, nbStoriesPerMonth: f.nbStoriesPerMonth, isOpened: false, datasources: [{ description: '', id: f.datasourceId, title: f.datasourceTitle }] });
                }
                else {
                    record[0].datasources.push({ description: '', id: f.datasourceId, title: f.datasourceTitle });
                }
            });
        });
        this.drawerContentService.setDrawerContent(this.matDrawer);
        this.oauthService.events.subscribe((e) => {
            if (e.type === "logout") {
                this.isConnected = false;
            }
            else if (e.type === "token_received") {
                this.init();
            }
        });
        this.init();
    }
    disconnect(evt) {
        evt.preventDefault();
        this.oauthService.logOut();
        this.router.navigate(['/home']);
        return false;
    }
    ngOnDestroy() {
        if (this.listener) {
            this.listener.unsubscribe();
        }
    }
    switchOpen(record) {
        record.isOpened = !record.isOpened;
    }
    chooseLanguage(lng) {
        this.translateService.use(lng);
    }
    login(evt) {
        evt.preventDefault();
        this.oauthService.customQueryParams = {
            'prompt': 'login'
        };
        this.oauthService.initImplicitFlow();
        return false;
    }
    configureAuth() {
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
    init() {
        const claims = this.oauthService.getIdentityClaims();
        if (!claims) {
            this.isConnected = false;
            return;
        }
        this.isConnected = true;
    }
};
__decorate([
    ViewChild('content', { static: true })
], AppComponent.prototype, "matDrawer", void 0);
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.sass']
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map