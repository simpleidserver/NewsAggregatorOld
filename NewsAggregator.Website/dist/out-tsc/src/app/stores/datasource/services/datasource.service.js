import { __decorate } from "tslib";
import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from '@envs/environment';
let DatasourceService = class DatasourceService {
    constructor(http, oauthService) {
        this.http = http;
        this.oauthService = oauthService;
    }
    searchDatasources(startIndex, count, title) {
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
        const targetUrl = environment.apiUrl + "/datasources/.search";
        const request = { isPaginationEnabled: true, startIndex: startIndex, count: count };
        if (title) {
            request["title"] = title;
        }
        return this.http.post(targetUrl, JSON.stringify(request), { headers: headers });
    }
};
DatasourceService = __decorate([
    Injectable()
], DatasourceService);
export { DatasourceService };
//# sourceMappingURL=datasource.service.js.map