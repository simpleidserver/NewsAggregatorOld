import { __decorate } from "tslib";
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@envs/environment';
let ArticleService = class ArticleService {
    constructor(http, oauthService) {
        this.http = http;
        this.oauthService = oauthService;
    }
    search(startIndex, count, order, direction, datasourceId) {
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
        const targetUrl = environment.apiUrl + "/feeds/me/.search";
        const request = { startIndex: startIndex, count: count, order: order, direction: direction, dataSourceId: datasourceId };
        return this.http.post(targetUrl, JSON.stringify(request), { headers: headers });
    }
};
ArticleService = __decorate([
    Injectable()
], ArticleService);
export { ArticleService };
//# sourceMappingURL=article.service.js.map