import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { of } from "rxjs";
const datasources = [
    { description: 'BBC', id: 'bbc', title: 'BBC' },
    { description: 'Sputnick', id: 'sputnick', title: 'Sputnick' },
    { description: 'JDV', id: 'JDV', title: 'JDV' }
];
let DatasourceService = class DatasourceService {
    constructor(http, oauthService) {
        this.http = http;
        this.oauthService = oauthService;
    }
    searchDatasources(startIndex, count, title) {
        let filtered = datasources;
        if (title && title !== '') {
            filtered = datasources.filter((f) => {
                return f.title.includes(title);
            });
        }
        const result = { content: filtered, count: 100, startIndex: 0, totalLength: 100 };
        return of(result);
    }
};
DatasourceService = __decorate([
    Injectable()
], DatasourceService);
export { DatasourceService };
//# sourceMappingURL=datasource.service.js.map