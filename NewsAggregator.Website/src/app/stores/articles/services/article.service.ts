import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@envs/environment';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { SearchArticlesResult } from '../models/search-article.model';

@Injectable()
export class ArticleService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  searchInDatasource(startIndex: number, count: number, order: string, direction: string, datasourceId: string): Observable<SearchArticlesResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/searchindatasource/" + datasourceId;
    const request: any = { startIndex: startIndex, count: count, order: order, direction: direction };
    return this.http.post<SearchArticlesResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  searchInFeed(startIndex: number, count: number, order: string, direction: string, feedId: string): Observable<SearchArticlesResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/searchinfeed/" + feedId;
    const request: any = { startIndex: startIndex, count: count, order: order, direction: direction };
    return this.http.post<SearchArticlesResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  searchRecommendations(startIndex: number, count: number): Observable<SearchArticlesResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/recommendations/.search";
    const request: any = { startIndex: startIndex, count: count };
    return this.http.post<SearchArticlesResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  like(articleId: string) : Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/" + articleId + "/like";
    return this.http.get<any>(targetUrl, { headers: headers });
  }

  unlike(articleId: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/" + articleId + "/unlike";
    return this.http.get<any>(targetUrl, { headers: headers });
  }

  read(articleId: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/" + articleId + "/read";
    return this.http.get<any>(targetUrl, { headers: headers });
  }

  unread(articleId: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/" + articleId + "/unread";
    return this.http.get<any>(targetUrl, { headers: headers });
  }

  readAndHide(articleId: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/articles/" + articleId + "/readAndHide";
    return this.http.get<any>(targetUrl, { headers: headers });
  }
}
