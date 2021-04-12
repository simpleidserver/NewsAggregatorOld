import { __decorate } from "tslib";
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@envs/environment';
import { of } from 'rxjs';
const feeds = [
    { feedTitle: 'News', datasourceTitle: 'BBC', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'en', datasourceId: 'bbc', feedId: 'news' },
    { feedTitle: 'News', datasourceTitle: 'Sputnick', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'sputnick', feedId: 'news' },
    { feedTitle: 'Gaming', datasourceTitle: 'JDV', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'JDV', feedId: 'gaming' }
];
const articles = [
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' },
    { externalId: 'https://www.bbc.co.uk/news/uk-56607669', id: '0651d9f1-6ddf-4241-8b51-5c3f6f5ae380', language: 'en', publishDate: new Date(), summary: 'Campaigners say banning residents aged 65 and over from taking trips outside homes is unlawful.', title: 'Ban on care home residents taking trips faces legal challenge', feedTitle: 'BBC' }
];
let FeedService = class FeedService {
    constructor(http, oauthService) {
        this.http = http;
        this.oauthService = oauthService;
    }
    search(startIndex, count, order, direction, feedTitle, datasourceIds, followersFilter, storiesFitler, isPaginationEnabled) {
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
        const targetUrl = environment.apiUrl + "/feeds/me/.search";
        const request = { isPaginationEnabled: isPaginationEnabled };
        if (isPaginationEnabled) {
            request["startIndex"] = startIndex;
            request["count"] = count;
        }
        if (order) {
            request["orderBy"] = order;
        }
        if (direction) {
            request["order"] = direction;
        }
        if (feedTitle) {
            request['feedTitle'] = feedTitle;
        }
        if (datasourceIds && datasourceIds.length > 0) {
            request["datasourceIds"] = datasourceIds;
        }
        if (followersFilter) {
            request["followersFilter"] = followersFilter;
        }
        if (storiesFitler) {
            request["storiesFitler"] = storiesFitler;
        }
        return this.http.post(targetUrl, JSON.stringify(request), { headers: headers });
    }
    unsubscribeDatasource(parameter) {
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
        const targetUrl = environment.apiUrl + "/feeds/" + parameter.feedId + "/datasources/" + parameter.datasourceId;
        return this.http.delete(targetUrl, { headers: headers });
    }
    searchArticles(feedId, startIndex, count, order, direction) {
        const filtered = articles.slice(startIndex, startIndex + count);
        let result = { content: filtered, count: filtered.length, startIndex: startIndex, totalLength: articles.length };
        return of(result);
    }
    getFeed(feedId) {
        const feed = feeds.filter((f) => f.feedId === feedId)[0];
        return of(feed);
    }
    addFeed(feedTitle, datasourceIds) {
        let headers = new HttpHeaders();
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
        const targetUrl = environment.apiUrl + "/feeds";
        const request = { title: feedTitle, datasourceIds: datasourceIds };
        return this.http.post(targetUrl, JSON.stringify(request), { headers: headers });
    }
    getAllFeeds() {
        return of(feeds);
    }
};
FeedService = __decorate([
    Injectable()
], FeedService);
export { FeedService };
//# sourceMappingURL=feed.service.js.map