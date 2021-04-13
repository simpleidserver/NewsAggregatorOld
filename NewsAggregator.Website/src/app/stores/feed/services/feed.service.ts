import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@envs/environment';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { DeleteDatasource } from '../actions/feed.actions';
import { DetailedFeed } from '../models/detailedfeed.model';
import { Feed } from '../models/feed.model';
import { SearchFeedsResult } from '../models/search-feed.model';

@Injectable()
export class FeedService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  search(startIndex: number | null, count: number | null, order: string, direction: string, feedTitle: string, datasourceIds: string[], followersFilter: number | null, storiesFitler: number | null, isPaginationEnabled : boolean): Observable<SearchFeedsResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/me/.search";
    const request: any = { isPaginationEnabled: isPaginationEnabled};
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

    return this.http.post<SearchFeedsResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  unsubscribeDatasource(parameter: DeleteDatasource): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/" + parameter.feedId + "/datasources/" + parameter.datasourceId;
    return this.http.delete<any>(targetUrl, { headers: headers });
  }

  getFeed(feedId: string): Observable<Feed> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/" + feedId;
    return this.http.get<Feed>(targetUrl, { headers: headers });
  }

  addFeed(feedTitle: string, datasourceIds: string[]): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds";
    const request: any = { title: feedTitle, datasourceIds: datasourceIds };
    return this.http.post<string>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  getAllFeeds(): Observable<DetailedFeed[]> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/me";
    return this.http.get<DetailedFeed[]>(targetUrl, { headers: headers });
  }

  deleteFeed(feedId: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/" + feedId;
    return this.http.delete<any>(targetUrl, { headers: headers });
  }
}
