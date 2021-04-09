import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable, of } from "rxjs";
import { Datasource } from "../models/datasource.model";
import { SearchDatasourcesResult } from "../models/SearchDatasourcesResult";

const datasources: Datasource[] = [
  { description: 'BBC', id: '169de9bc-a389-4e21-b1a7-3d9ad25dfe5f', title: 'BBC' }
];

@Injectable()
export class DatasourceService {
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  searchDatasources(startIndex: number, count: number, title: string): Observable<SearchDatasourcesResult> {
    let filtered = datasources;
    if (title && title !== '') {
      filtered = datasources.filter((f: Datasource) => {
        return f.title.includes(title);
      });
    }

    const result: SearchDatasourcesResult = { content: filtered, count: 100, startIndex: 0, totalLength: 100 };
    return of(result);
  }
}
