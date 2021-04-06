import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, of } from 'rxjs';
import { DeleteDatasource } from '../actions/feed.actions';
import { Feed } from '../models/feed.model';
import { SearchFeedsResult } from '../models/search-feed.model';

const feeds: Feed[] = [
  { feedTitle: 'News', datasourceTitle: 'BBC', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'en', datasourceId : 'bbc', feedId: 'news' },
  { feedTitle: 'News', datasourceTitle: 'Sputnick', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId : 'sputnick', feedId: 'news' },
  { feedTitle: 'Gaming', datasourceTitle: 'JDV', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'JDV', feedId: 'gaming' }
];

@Injectable()
export class FeedService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  search(startIndex: number, count: number, order: string, direction: string, feedTitle: string): Observable<SearchFeedsResult> {
    /*
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/feeds/me/.search";
    const request: any = { startIndex: startIndex, count: count };
    if (order) {
      request["orderBy"] = order;
    }

    if (direction) {
      request["order"] = direction;
    }

    if (feedTitle) {
      request['feedTitle'] = feedTitle;
    }

    return this.http.post<SearchFeedsResult>(targetUrl, JSON.stringify(request), { headers: headers });
    */

    let filtered = feeds;
    if (feedTitle && feedTitle !== '') {
      filtered = feeds.filter((f: Feed) => {
        return f.feedTitle.includes(feedTitle);
      });
    }

    const result: SearchFeedsResult = { content: filtered, count: 100, startIndex: 0, totalLength: 100 };
    return of(result);
  }

  deleteDatasources(parameters: DeleteDatasource[]) : Observable<boolean> {
    return of(true);
  }
}
