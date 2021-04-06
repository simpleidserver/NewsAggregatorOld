import { createAction, props } from '@ngrx/store';
import { SearchFeedsResult } from '../models/search-feed.model';

export class DeleteDatasource {
  constructor(public feedId: string, public datasourceId: string) { }
}

export const startSearchFeeds = createAction("[Feeds] START_SEARCH_FEEDS", props<{ order: string, direction: string, count: number, startIndex: number, feedTitle: string }>());
export const errorSearchFeeds = createAction("[Feeds] ERROR_SEARCH_FEED");
export const completeSearchFeeds = createAction("[Feeds] COMPLETE_SEARCH_FEEDS", props <{content: SearchFeedsResult}>());
export const startDeleteDatasources = createAction("[Feeds] START_DELETE_DATASOURCES", props<{ parameters: DeleteDatasource[] }>());
export const errorDeleteDatasources = createAction("[Feeds] ERROR_DELETE_DATASOURCES");
export const completeDeleteDatasources = createAction("[Feeds] COMPLETE_DELETE_DATASOURCES", props<{ parameters: DeleteDatasource[] }>());
