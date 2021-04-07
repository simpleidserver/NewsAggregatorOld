import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, of } from 'rxjs';
import { Article } from '../../articles/models/article.model';
import { SearchArticlesResult } from '../../articles/models/search-article.model';
import { DeleteDatasource } from '../actions/feed.actions';
import { Feed } from '../models/feed.model';
import { SearchFeedsResult } from '../models/search-feed.model';

const feeds: Feed[] = [
  { feedTitle: 'News', datasourceTitle: 'BBC', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'en', datasourceId : 'bbc', feedId: 'news' },
  { feedTitle: 'News', datasourceTitle: 'Sputnick', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId : 'sputnick', feedId: 'news' },
  { feedTitle: 'Gaming', datasourceTitle: 'JDV', nbFollowers: 1000, nbStoriesPerMonth: 1000, language: 'fr', datasourceId: 'JDV', feedId: 'gaming' }
];
const articles: Article[] = [
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

  searchArticles(feedId: string, startIndex: number, count: number, order: string, direction: string): Observable<SearchArticlesResult> {
    const filtered = articles.slice(startIndex, startIndex + count);
    let result: SearchArticlesResult = { content: filtered, count : filtered.length, startIndex: startIndex, totalLength: articles.length };
    return of(result);
  }

  getFeed(feedId: string): Observable<Feed> {
    const feed = feeds.filter((f: Feed) => f.feedId === feedId)[0];
    return of(feed);
  }

  addFeed(feedTitle: string, datasourceId: string): Observable<Feed> {
    const feed: Feed = { datasourceId: datasourceId, datasourceTitle: 'NewDatasource', feedId: 'feedid', feedTitle: feedTitle, language: 'en', nbFollowers: 0, nbStoriesPerMonth: 0 };
    return of(feed);
  }

  getAllFeeds(): Observable<Feed[]> {
    return of(feeds);
  }
}
