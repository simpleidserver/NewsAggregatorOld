import { createAction, props } from '@ngrx/store';
import { SearchArticlesResult } from '../../articles/models/search-article.model';

export const startSearchArticlesInDatasource = createAction("[Feeds] START_SEARCH_ARTICLES_IN_DATASOURCE", props<{ order: string, direction: string, count: number, startIndex: number, datasourceId: string }>());
export const errorSearchArticlesInDatasource = createAction("[Feeds] ERROR_SEARCH_ARTICLES_IN_DATASOURCE");
export const completeSearchArticlesInDatasource = createAction("[Feeds] COMPLETE_SEARCH_ARTICLES_IN_DATASOURCE", props<{ content: SearchArticlesResult}>());
export const startSearchArticlesInFeed = createAction("[Feeds] START_SEARCH_ARTICLES_IN_FEED", props<{ order: string, direction: string, count: number, startIndex: number, feedId: string }>());
export const errorSearchArticlesInFeed = createAction("[Feeds] ERROR_SEARCH_ARTICLES_IN_FEED");
export const completeSearchArticlesInFeed = createAction("[Feeds] COMPLETE_SEARCH_ARTICLES_IN_FEED", props<{ content: SearchArticlesResult }>());
