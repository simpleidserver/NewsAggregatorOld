import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@envs/environment';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { SearchHangfireJobsResult } from '../models/searchhangfirejobs';
import { SearchHangfireJobStatesResult } from '../models/searchhangfirejobstates';

@Injectable()
export class HangfireService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  searchHangfireJobs(startIndex: number, count: number): Observable<SearchHangfireJobsResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/hangfire/searchjobs";
    const request: any = { startIndex: startIndex, count: count };
    return this.http.post<SearchHangfireJobsResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  searchHangfireJobStates(startIndex: number, count: number, jobId: number): Observable<SearchHangfireJobStatesResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/hangfire/searchjobstates";
    const request: any = { startIndex: startIndex, count: count, jobId: jobId };
    return this.http.post<SearchHangfireJobStatesResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  extractArticles(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/hangfire/extract-articles";
    return this.http.get<any>(targetUrl, { headers: headers });
  }

  extractRecommendations(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/hangfire/extract-recommendations";
    return this.http.get<any>(targetUrl, { headers: headers });
  }
}
