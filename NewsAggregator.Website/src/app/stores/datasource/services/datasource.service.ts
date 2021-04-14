import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from '@envs/environment';
import { OAuthService } from "angular-oauth2-oidc";
import { Observable } from "rxjs";
import { Datasource } from "../models/datasource.model";
import { SearchDatasourcesResult } from "../models/SearchDatasourcesResult";

@Injectable()
export class DatasourceService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  searchDatasources(startIndex: number, count: number, title: string): Observable<SearchDatasourcesResult> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/datasources/.search";
    const request: any = { isPaginationEnabled: true, startIndex: startIndex, count: count };
    if (title) {
      request["title"] = title;
    }

    return this.http.post<SearchDatasourcesResult>(targetUrl, JSON.stringify(request), { headers: headers });
  }

  get(datasourceId: string): Observable<Datasource> {
    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Authorization', 'Bearer ' + this.oauthService.getIdToken());
    const targetUrl = environment.apiUrl + "/datasources/" + datasourceId;
    return this.http.get<Datasource>(targetUrl, { headers: headers });
  }
}
