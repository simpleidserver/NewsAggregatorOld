import { __decorate } from "tslib";
import { Component } from '@angular/core';
let AppComponent = class AppComponent {
    constructor(translateService) {
        this.translateService = translateService;
        this.translateService.setDefaultLang('fr');
        this.translateService.use('fr');
    }
    chooseLanguage(lng) {
        this.translateService.use(lng);
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.sass']
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map